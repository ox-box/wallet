//using Microsoft.AspNetCore.Http;
using OX.IO.Data.LevelDB;
using OX.IO.Json;
using OX.Ledger;
using OX.Network.RPC;
using OX.VM;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Snapshot = OX.Persistence.Snapshot;
using OX.Plugins;
using OX.Network.P2P.Payloads;
using OX.Network.P2P;
using OX.IO;
using OX.SmartContract;
using Akka.Util.Internal;
using OX.Bapps;
using Akka.Actor.Dsl;
using System.ComponentModel.Design;
using OX.Wallets.Base.Events;
using OX.Wallets.Base.Wallets;
using Akka.IO;
using Nethereum.Model;
using OX.Cryptography.ECC;
using OX.Wallets.Base;
using OX.Persistence;
using OX.Cryptography;
using OX.IO.Wrappers;
using OX.Wallets.Base.Flash;
using Org.BouncyCastle.Bcpg.OpenPgp;
using OX.Wallets.Base.NFT;

namespace OX.Wallets.Flash
{
    public class FlashMessageProvider : BaseFlashMessageProvider
    {
        public static FlashMessageProvider Instance { get; private set; }
        Wallet _wallet;
        public override Wallet Wallet { get { return _wallet; } set { _wallet = value; initHashAccounts(); } }
        public IFlashStateFilter LogFilter { get; private set; }
        public IFlashUnicastFilter UniFilter { get; private set; }
        public IFlashMulticastFilter MultiFilter { get; private set; }
        public Dictionary<UInt256, AccountPack> HashAccounts = new Dictionary<UInt256, AccountPack>();
        public Dictionary<UInt256, UnicastTalkLineValue> UnicastTalkLines = new Dictionary<UInt256, UnicastTalkLineValue>();
        public Dictionary<UInt256, MulticastTalkLineValue> MulticastTalkLines = new Dictionary<UInt256, MulticastTalkLineValue>();
        public Dictionary<UInt256, Uint32Wrapper> TalkCount = new Dictionary<UInt256, Uint32Wrapper>();
        public Dictionary<UInt160, Uint32Wrapper> FlashStateCount = new Dictionary<UInt160, Uint32Wrapper>();
        public Dictionary<FlashStateTag, Uint32Wrapper> FlashStateTagCount = new Dictionary<FlashStateTag, Uint32Wrapper>();
        public Dictionary<UInt256, CommentCountValue> FlashStateCommentCount = new Dictionary<UInt256, CommentCountValue>();
        public Dictionary<StringWrapper, NFTPendingRecord> NFTPendings = new Dictionary<StringWrapper, NFTPendingRecord>();
        public FlashMessageProvider(Bapp bapp, IFlashStateFilter logFilter, IFlashUnicastFilter uniFilter, IFlashMulticastFilter multiFilter) : base(bapp)
        {
            this.LogFilter = logFilter;
            this.UniFilter = uniFilter;
            this.MultiFilter = multiFilter;
            Db = DB.Open(Path.GetFullPath($"{WalletIndexDirectory}\\fm_{Message.Magic.ToString("X8")}"), new Options { CreateIfMissing = true });
            Instance = this;
            UnicastTalkLines = new Dictionary<UInt256, UnicastTalkLineValue>(this.GetAll<UInt256, UnicastTalkLineValue>(FlashStatePersistencePrefixes.FlashUnicast_TalkLine));
            MulticastTalkLines = new Dictionary<UInt256, MulticastTalkLineValue>(this.GetAll<UInt256, MulticastTalkLineValue>(FlashStatePersistencePrefixes.FlashMulticast_TalkLine));
            TalkCount = new Dictionary<UInt256, Uint32Wrapper>(this.GetAll<UInt256, Uint32Wrapper>(FlashStatePersistencePrefixes.FlashTalk_Count));
            FlashStateCount = new Dictionary<UInt160, Uint32Wrapper>(this.GetAll<UInt160, Uint32Wrapper>(FlashStatePersistencePrefixes.FlashState_LatestCount));
            FlashStateTagCount = new Dictionary<FlashStateTag, Uint32Wrapper>(this.GetAll<FlashStateTag, Uint32Wrapper>(FlashStatePersistencePrefixes.FlashState_TagCount));
            NFTPendings = new Dictionary<StringWrapper, NFTPendingRecord>(this.GetAll<StringWrapper, NFTPendingRecord>(FlashStatePersistencePrefixes.Flash_NFTPendingRecord));
            if (OXRunTime.RunMode == RunMode.Server)
            {
            }
        }
        void initHashAccounts()
        {
            if (_wallet.IsNotNull())
            {
                this.HashAccounts.Clear();
                foreach (var act in this._wallet.GetHeldAccounts())
                {
                    var key = act.GetKey();
                    this.HashAccounts[act.ScriptHash.Hash] = new AccountPack { Key = key, Address = act.ScriptHash, PublicKey = key.PublicKey };
                }
            }
        }

        public override void OnFlashMessage(FlashMessage flashMessage)
        {
            if (!FlashMemoryHelper.FlashHashs.Contains(flashMessage.Hash))
            {
                FlashMemoryHelper.FlashHashs.Enqueue(flashMessage.Hash);
                switch (flashMessage.Type)
                {
                    case FlashMessageType.FlashState:
                        FlashState fs = flashMessage as FlashState;
                        if (fs.IsNotNull() && (this.LogFilter.IsNull() || this.LogFilter.StateInputFilter(fs)))
                            Do(wb =>
                            {
                                wb.Save_FlashState(this, fs);
                            });
                        break;
                    case FlashMessageType.FlashStateComment:
                        FlashStateComment fsc = flashMessage as FlashStateComment;
                        if (fsc.IsNotNull() && (this.LogFilter.IsNull() || this.LogFilter.CommentInputFilter(fsc)))
                            Do(wb =>
                            {
                                wb.Save_FlashStateComment(this, fsc);
                            });
                        break;
                    case FlashMessageType.FlashUnicast:
                        FlashUnicast fu = flashMessage as FlashUnicast;
                        if (fu.IsNotNull() && (this.UniFilter.IsNull() || this.UniFilter.Filter(fu)))
                            Do(wb =>
                            {
                                if (this.HashAccounts.TryGetValue(Contract.CreateSignatureRedeemScript(fu.Sender).ToScriptHash().Hash, out AccountPack myap))
                                {
                                    wb.Save_FlashUnicast(this, myap, fu, TalkKind.OutBox);
                                }
                                else if (this.HashAccounts.TryGetValue(fu.RecipientHash, out AccountPack uniap))
                                {
                                    wb.Save_FlashUnicast(this, uniap, fu, TalkKind.Inbox);
                                }
                            });
                        break;
                    case FlashMessageType.FlashMulticast:
                        FlashMulticast fm = flashMessage as FlashMulticast;
                        if (fm.IsNotNull() && (this.MultiFilter.IsNull() || this.MultiFilter.Filter(fm)))
                            Do(wb =>
                            {
                                bool findFM = false;
                                TalkKind tk = default;
                                foreach (var m in this.MulticastTalkLines)
                                {
                                    if (m.Key == fm.TalkLine)
                                    {
                                        tk = Contract.CreateSignatureRedeemScript(fm.Sender).ToScriptHash() == m.Value.Local ? TalkKind.OutBox : TalkKind.Inbox;
                                        findFM = true;
                                        if (tk == TalkKind.OutBox) break;
                                    }
                                }
                                if (findFM)
                                    wb.Save_FlashMulticast(this, fm, tk);
                            });
                        break;
                    case FlashMessageType.FlashMulticastNotice:
                        FlashMulticastNotice fmn = flashMessage as FlashMulticastNotice;
                        AccountPack ap = default;
                        byte[] shareKey = default;
                        string msg = string.Empty;
                        foreach (var dest in fmn.Destinations)
                        {
                            if (this.HashAccounts.TryGetValue(dest.RecipientHash, out ap))
                            {
                                try
                                {
                                    var suffix = BitConverter.GetBytes(fmn.MinIndex);
                                    shareKey = dest.Data.Decrypt(ap.Key, fmn.Sender, suffix);
                                    msg = System.Text.Encoding.UTF8.GetString(fmn.Msg.Decrypt(shareKey, suffix));
                                    break;
                                }
                                catch
                                {
                                    continue;
                                }
                            }
                        }
                        if (ap != default && shareKey != default)
                        {
                            var talkLine = new UInt256(Crypto.Default.Hash256(Crypto.Default.Hash256(shareKey)));
                            if (!this.MulticastTalkLines.ContainsKey(talkLine))
                            {
                                Do(wb =>
                                {
                                    wb.Save_MulticastTalkLine(this, ap.Address, shareKey, msg);
                                });
                            }
                        }
                        break;
                    case FlashMessageType.FlashNFTPending:
                        FlashNFTPending fnp = flashMessage as FlashNFTPending;
                        foreach (var pending in fnp.Pendings)
                        {
                            this.AppendNFTPending(pending);
                        }
                        break;
                }
            }
        }
        public void Do(Action<WriteBatch> action)
        {
            WriteBatch batch = new WriteBatch();
            if (action != default)
                action(batch);
            this.Db.Write(WriteOptions.Default, batch);
        }

        #region
        public void SaveMulticastTalkLine(UInt160 localAddress, byte[] shareKey, string msg)
        {
            Do(wb =>
            {
                wb.Save_MulticastTalkLine(this, localAddress, shareKey, msg);
            });
        }
        public void SaveUnicastTalkLine(WalletAccount local, ECPoint remote, string msg)
        {
            Do(wb =>
            {
                var talkLine = ECDiffieHellmanHelper.ECDHDeriveKeyHash(local.GetKey(), remote);
                if (!this.UnicastTalkLines.TryGetValue(talkLine, out UnicastTalkLineValue utlv))
                {
                    utlv = new UnicastTalkLineValue { Local = local.ScriptHash, Remote = remote, Label = msg };
                    wb.Put(SliceBuilder.Begin(FlashStatePersistencePrefixes.FlashUnicast_TalkLine).Add(talkLine), SliceBuilder.Begin().Add(utlv));
                    this.UnicastTalkLines[talkLine] = utlv;
                }
            });
        }
        public IEnumerable<KeyValuePair<TalkLineKey, FlashUnicastRecord>> GetLastUnicastRecords(UInt256 talkLine, out uint range)
        {
            range = 0;
            if (this.TalkCount.TryGetValue(talkLine, out Uint32Wrapper w))
            {
                range = w.Value / FlashMemoryHelper.RangeSize;
                return GetUnicastRecords(talkLine, range);
            }
            return default;
        }
        public IEnumerable<KeyValuePair<TalkLineKey, FlashUnicastRecord>> GetUnicastRecords(UInt256 talkLine, uint range)
        {
            var builder = SliceBuilder.Begin().Add(talkLine).Add(range);
            return this.GetAll<TalkLineKey, FlashUnicastRecord>(FlashStatePersistencePrefixes.FlashUnicast_Record, builder.ToArray());
        }
        public IEnumerable<KeyValuePair<TalkLineKey, FlashMulticastRecord>> GetLastMulticastRecords(UInt256 talkLine, out uint range)
        {
            range = 0;
            if (this.TalkCount.TryGetValue(talkLine, out Uint32Wrapper w))
            {
                range = w.Value / FlashMemoryHelper.RangeSize;
                return GetMulticastRecords(talkLine, range);
            }
            return default;
        }
        public IEnumerable<KeyValuePair<TalkLineKey, FlashMulticastRecord>> GetMulticastRecords(UInt256 talkLine, uint range)
        {
            var builder = SliceBuilder.Begin().Add(talkLine).Add(range);
            return this.GetAll<TalkLineKey, FlashMulticastRecord>(FlashStatePersistencePrefixes.FlashMulticast_Record, builder.ToArray());
        }
        public FlashStateRecord GetFlashStatRecord(UInt256 flashStateHash)
        {
            return this.Get<FlashStateRecord>(FlashStatePersistencePrefixes.FlashState_Record, flashStateHash);
        }

        public IEnumerable<KeyValuePair<FlashStateCommentKey, FlashStateCommentValue>> GetFlashStateComments(UInt256 flashStateHash)
        {
            var builder = SliceBuilder.Begin().Add(flashStateHash);
            return this.GetAll<FlashStateCommentKey, FlashStateCommentValue>(FlashStatePersistencePrefixes.FlashStateComment_Record, builder.ToArray());
        }
        public void CropCommentCount()
        {
            if (this.FlashStateCommentCount.Count > 10000)
            {
                Dictionary<UInt256, CommentCountValue> dic = new Dictionary<UInt256, CommentCountValue>();
                foreach (var k in this.FlashStateCommentCount.OrderByDescending(m => m.Value.Timestamp).Take(10000))
                {
                    dic[k.Key] = k.Value;
                }
                this.FlashStateCommentCount = dic;
            }
        }

        public IOrderedEnumerable<FlashStateRecord> GetCachedHotFlashStateRecords(uint pageIndex = 0)
        {
            var list = RangeCacheHelper<IEnumerable<FlashStateRecord>>.GetCachePool("mostpop_flashstaterecord").Get(pageIndex, r =>
               {
                   var records = this.FlashStateCommentCount.OrderByDescending(m => m.Value.Count.Value).Skip((int)pageIndex * 100).Take(100);
                   if (records.IsNullOrEmpty()) return default;
                   List<FlashStateRecord> list = new List<FlashStateRecord>();
                   foreach (var rhash in records)
                   {
                       var record = GetFlashStatRecord(rhash.Key);
                       if (record.IsNotNull()) list.Add(record);
                   }
                   return list;
               });
            if (list.IsNullOrEmpty()) return default;
            return list.OrderByDescending(m => m.CommentCount);
        }
        public uint GetLastRangeForTag(FlashStateTag tag)
        {
            uint range = uint.MaxValue;
            if (this.FlashStateTagCount.TryGetValue(tag, out Uint32Wrapper w))
            {
                range = w.Value / FlashMemoryHelper.RangeSize;
            }
            return range;
        }
        public uint GetLastRangeForSender(UInt160 sender)
        {
            uint range = uint.MaxValue;
            if (this.FlashStateCount.TryGetValue(sender, out Uint32Wrapper w))
            {
                range = w.Value / FlashMemoryHelper.RangeSize;
            }
            return range;
        }
        public IOrderedEnumerable<FlashStateRecord> GetCachedFlashStateRecordsForTag(FlashStateTag tag, uint pageIndex)
        {
            var list = RangeCacheHelper<IEnumerable<FlashStateRecord>>.GetCachePool($"tag_flashstaterecord_{tag.ToString()}").Get(pageIndex, r =>
              {
                  var rs = GetFlashStateRecordsForTag(tag, r);
                  if (rs.IsNullOrEmpty()) return default;
                  List<FlashStateRecord> list = new List<FlashStateRecord>();
                  foreach (var rhash in rs)
                  {
                      var record = GetFlashStatRecord(rhash.Value);
                      if (record.IsNotNull()) list.Add(record);
                  }
                  return list;
              });
            if (list.IsNullOrEmpty()) return default;
            return list.OrderByDescending(m => m.Timestamp);
        }
        public IEnumerable<KeyValuePair<TagFlashStateKey, UInt256>> GetFlashStateRecordsForTag(FlashStateTag tag, uint range)
        {
            var builder = SliceBuilder.Begin().Add(tag).Add(range);
            return this.GetAll<TagFlashStateKey, UInt256>(FlashStatePersistencePrefixes.FlashState_TagRecord, builder.ToArray());
        }
        public IOrderedEnumerable<FlashStateRecord> GetCachedFlashStateRecordsForLatest(uint pageIndex)
        {
            var list = RangeCacheHelper<IEnumerable<FlashStateRecord>>.GetCachePool("latest_flashstaterecord").Get(pageIndex, r =>
            {
                var rs = GetFlashStateRecordsForLatest(r);
                if (rs.IsNullOrEmpty()) return default;
                List<FlashStateRecord> list = new List<FlashStateRecord>();
                foreach (var rhash in rs)
                {
                    var record = GetFlashStatRecord(rhash.Value);
                    if (record.IsNotNull()) list.Add(record);
                }
                return list;
            });
            if (list.IsNullOrEmpty()) return default;
            return list.OrderByDescending(m => m.Timestamp);
        }
        public IEnumerable<KeyValuePair<GlobalFlashStateKey, UInt256>> GetFlashStateRecordsForLatest(uint range)
        {
            var builder = SliceBuilder.Begin().Add(range);
            return this.GetAll<GlobalFlashStateKey, UInt256>(FlashStatePersistencePrefixes.FlashState_GlobalRecord, builder.ToArray());
        }
        public IOrderedEnumerable<FlashStateRecord> GetCachedFlashStateRecordsForSender(UInt160 sender, uint pageIndex)
        {
            var list = RangeCacheHelper<IEnumerable<FlashStateRecord>>.GetCachePool($"sender_flashstaterecord_{sender.ToAddress()}").Get(pageIndex, r =>
            {
                var rs = GetFlashStateRecordsForSender(sender, r);
                if (rs.IsNullOrEmpty()) return default;
                List<FlashStateRecord> list = new List<FlashStateRecord>();
                foreach (var rhash in rs)
                {
                    var record = GetFlashStatRecord(rhash.Value);
                    if (record.IsNotNull()) list.Add(record);
                }
                return list;
            });
            if (list.IsNullOrEmpty()) return default;
            return list.OrderByDescending(m => m.Timestamp);
        }
        public IEnumerable<KeyValuePair<GlobalFlashStateKey, UInt256>> GetFlashStateRecordsForSender(UInt160 sender, uint range)
        {
            var builder = SliceBuilder.Begin().Add(sender).Add(range);
            return this.GetAll<GlobalFlashStateKey, UInt256>(FlashStatePersistencePrefixes.FlashState_SenderRecord, builder.ToArray());
        }
        public void CheckNFTPending()
        {
            if (this.NFTPendings.IsNotNullAndEmpty())
            {
                Do(wb =>
                {
                    var ks = this.NFTPendings.Keys.ToArray();
                    foreach (var k in ks)
                    {
                        var ndv = this.NFTPendings[k].Pending;
                        var snapshot = Blockchain.Singleton.CurrentSnapshot;
                        var nfsState = snapshot.GetNftTransfer(ndv.Key);
                        bool needRemove = false;
                        if (nfsState.IsNull())
                        {
                            needRemove = true;
                        }
                        else
                        {
                            if (!nfsState.LastNFS.Hash.Equals(ndv.Validator.Target.PreHash))
                            {
                                needRemove = true;
                            }
                        }
                        if ((ndv.Validator.Target.MaxIndex >0&&ndv.Validator.Target.MaxIndex < Blockchain.Singleton.Height) || ndv.Validator.Target.Amount <= Fixed8.Zero)
                            needRemove = true;
                        if (needRemove)
                        {
                            if (this.NFTPendings.Remove(k))
                            {
                                wb.Delete(SliceBuilder.Begin(FlashStatePersistencePrefixes.Flash_NFTPendingRecord).Add(k));
                            }
                        }
                    }
                });
            }
        }
        public static StringWrapper GetNFTIssueID(NFSStateKey key)
        {
            return new StringWrapper($"{key.NFCID.CID}-{key.IssueBlockIndex}-{key.IssueN}");
        }
        public bool AppendNFTPending(NFTPending ndv)
        {

            NFTPendingRecord record = new NFTPendingRecord
            {
                IssueId = GetNFTIssueID(ndv.Key),
                Pending = ndv
            };
            this.NFTPendings[record.IssueId] = record;
            Do(wb =>
            {
                wb.Put(SliceBuilder.Begin(FlashStatePersistencePrefixes.Flash_NFTPendingRecord).Add(record.IssueId), SliceBuilder.Begin().Add(record));
            });
            return true;
        }
        #endregion
    }
}
