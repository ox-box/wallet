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

namespace OX.Wallets.Base
{
    public class WalletBappProvider : BaseBappProvider, IWalletProvider
    {
        public static WalletBappProvider Instance { get; private set; }
        Wallet _wallet;
        public override Wallet Wallet { get { return _wallet; } set { _wallet = value; initHashAccounts(); } }
        public Fixed8 TotalIssuedOXC { get; private set; }
        public Dictionary<UInt256, AccountPack> HashAccounts = new Dictionary<UInt256, AccountPack>();
        internal Dictionary<WalletSettingKey, WalletSettingValue> WalletSettings { get; set; } = new Dictionary<WalletSettingKey, WalletSettingValue>();
        internal Dictionary<OutputKey, LockAssetMerge> MyLockAssets { get; set; } = new Dictionary<OutputKey, LockAssetMerge>();
        public Dictionary<OutputKey, LockAssetMerge> AllLockAssets { get; set; } = new Dictionary<OutputKey, LockAssetMerge>();
        internal Dictionary<UInt160, AssetTrustContract> AssetTrustContacts { get; set; } = new Dictionary<UInt160, AssetTrustContract>();
        internal Dictionary<AssetTrustOutputKey, TransactionOutput> AssetTrustUTXO { get; set; } = new Dictionary<AssetTrustOutputKey, TransactionOutput>();
        internal Dictionary<EthMapOutputKey, TransactionOutput> EthMapUTXO { get; set; } = new Dictionary<EthMapOutputKey, TransactionOutput>();
        internal Dictionary<UInt160, EthereumMapTransactionMerge> AllEthereumMaps { get; set; } = new Dictionary<UInt160, EthereumMapTransactionMerge>();
        internal Dictionary<CoinReference, EthOutputMerge> AllEthereumMapUTXOs { get; set; } = new Dictionary<CoinReference, EthOutputMerge>();
        public WalletBappProvider(Bapp bapp) : base(bapp)
        {
            Db = DB.Open(Path.GetFullPath($"{WalletIndexDirectory}\\wlt_{Message.Magic.ToString("X8")}"), new Options { CreateIfMissing = true });
            Instance = this;
            this.WalletSettings = new Dictionary<WalletSettingKey, WalletSettingValue>(this.GetAll<WalletSettingKey, WalletSettingValue>(WalletBizPersistencePrefixes.Wallet_Setting));
            this.MyLockAssets = new Dictionary<OutputKey, LockAssetMerge>(this.GetMyAllLockAssets());
            this.AllLockAssets = new Dictionary<OutputKey, LockAssetMerge>(this.GeTAllLockAssets());
            this.AssetTrustContacts = new Dictionary<UInt160, AssetTrustContract>(this.GetAllAssetTrustContracts());
            this.AssetTrustUTXO = new Dictionary<AssetTrustOutputKey, TransactionOutput>(this.GetAllAssetTrustUTXOs());
            this.EthMapUTXO = new Dictionary<EthMapOutputKey, TransactionOutput>(this.GetAllEthMapUTXOs());
            this.TotalIssuedOXC = this.Get<Fixed8>(WalletBizPersistencePrefixes.OXC_ALL_Issued, Blockchain.OXC);
            if (this.TotalIssuedOXC == Fixed8.Zero) this.TotalIssuedOXC = Fixed8.One * 40000000;
            if (OXRunTime.RunMode == RunMode.Server)
            {
                this.AllEthereumMaps = new Dictionary<UInt160, EthereumMapTransactionMerge>(this.GetAll<UInt160, EthereumMapTransactionMerge>(WalletBizPersistencePrefixes.ALL_Eth_Map));
                this.AllEthereumMapUTXOs = new Dictionary<CoinReference, EthOutputMerge>(this.GetAll<CoinReference, EthOutputMerge>(WalletBizPersistencePrefixes.ALL_Eth_Map_UTXO));
            }
        }
        void initHashAccounts()
        {
            if (_wallet.IsNotNull())
            {
                if (_wallet is OpenWallet openWallet)
                {
                    openWallet.GetAssetTrustContacts = () => this.AssetTrustContacts;
                    openWallet.GetAssetTrustUTXO = () => this.AssetTrustUTXO;
                    openWallet.GetEthMapUTXO = () => this.EthMapUTXO;
                    openWallet.GetAllEthereumMaps = () => this.AllEthereumMaps;
                    openWallet.GetAllEthereumMapUTXOs = () => this.AllEthereumMapUTXOs;
                }
                this.HashAccounts.Clear();
                foreach (var act in this._wallet.GetHeldAccounts())
                {
                    var key = act.GetKey();
                    this.HashAccounts[act.ScriptHash.Hash] = new AccountPack { Key = key, Address = act.ScriptHash, PublicKey = key.PublicKey };
                }
            }
        }
        public override void OnBappEvent(BappEvent bappEvent) { }
        public override void OnCrossBappMessage(CrossBappMessage message) { }
        public override void OnRebuild(Wallet wallet)
        {
            WriteBatch batch = new WriteBatch();
            ReadOptions options = new ReadOptions { FillCache = false };
            using (Iterator it = Db.NewIterator(options))
            {
                for (it.SeekToFirst(); it.Valid(); it.Next())
                {
                    batch.Delete(it.Key());
                }
            }
            Db.Write(WriteOptions.Default, batch);
            //BlockEvent?.Invoke(this, new BlockEvent() { Block = null, EventType = 1 });
        }
        public override void BeforeOnBlock(Block block)
        { }
        public override void AfterOnBlock(Block block)
        { }
        public override void OnBlock(Block block)
        {
            bool hasEventTransaction = false;

            WriteBatch batch = new WriteBatch();
            ushort m = 0;
            foreach (var tx in block.Transactions)
            {
                if (tx is EventTransaction eventTx)
                {
                    switch (eventTx.EventType)
                    {
                        case EventType.Board:
                            batch.Save_Event_Board(block, eventTx, m);
                            break;
                        case EventType.Engrave:
                            batch.Save_Event_Engrave(this, block, eventTx, m);
                            break;
                        case EventType.Digg:
                            batch.Save_Event_Digg(this, block, eventTx, m);
                            break;
                    }
                    hasEventTransaction = true;
                }
                //else if (tx is NFTCoinTransaction nftcoin)
                //{
                //    batch.Save_NFT_Coin(this, block, nftcoin, m);
                //}
                //else if (tx is NFTDonateTransaction nftdonate)
                //{
                //    batch.Save_NFT_Donate(this, block, nftdonate, m);
                //}
                else if (tx is NftTransaction nft)
                {
                    batch.Save_NFT(this, block, nft, m);
                }
                else if (tx is NftTransferTransaction ntftransfer)
                {
                    batch.Save_NFT_Transfer(this, block, ntftransfer, m);
                }
                else if (tx is LockAssetTransaction lat)
                {
                    batch.Save_LockAssetTransaction(this, block, lat, m);
                }
                else if (tx is AssetTrustTransaction att)
                {
                    batch.Save_AssetTrustTransaction(this, block, att, m);
                }
                else if (tx is BillTransaction bt)
                {

                }
                else if (tx is EthereumMapTransaction emt)
                {
                    if (OXRunTime.RunMode == RunMode.Server)
                        batch.Save_EthereumMapTransaction(this, block, emt, m);
                }
                else if (tx is RewardTransaction rwTx)
                {

                }
                else if (tx is SecretLetterTransaction slt)
                {
                    OnSecretLetterTransaction(batch, block, slt);
                }
                else if (tx is BookTransaction bookTx)
                {
                    batch.Save_BookTransaction(this, block, bookTx, m);
                }
                else if (tx is IssueTransaction it)
                {
                    batch.Save_IssueTransaction(this, block, it, m);
                }
                else if (tx is ClaimTransaction clmTx)
                {
                    foreach (var kp in clmTx.Claims)
                    {
                        OutputKey outputkey = new OutputKey { TxId = kp.PrevHash, N = kp.PrevIndex };
                        batch.Delete(SliceBuilder.Begin(WalletBizPersistencePrefixes.TX_Once_MyLockOXS).Add(outputkey));
                    }
                    TransactionResult result = clmTx.GetTransactionResults().FirstOrDefault(p => p.AssetId == Blockchain.OXC);
                    this.TotalIssuedOXC -= result.Amount;
                    batch.Put(SliceBuilder.Begin(WalletBizPersistencePrefixes.OXC_ALL_Issued).Add(Blockchain.OXC), SliceBuilder.Begin().Add(this.TotalIssuedOXC));
                }

                //txo
                foreach (KeyValuePair<CoinReference, TransactionOutput> kp in tx.References)
                {
                    //watch lock asset
                    OutputKey outputkey = new OutputKey { TxId = kp.Key.PrevHash, N = kp.Key.PrevIndex };
                    if (this.MyLockAssets.ContainsKey(outputkey))
                    {
                        if (kp.Value.AssetId.Equals(Blockchain.OXS))
                        {
                            var lockOXS = this.Get<LockOXS>(WalletBizPersistencePrefixes.TX_Once_MyLockOXS, outputkey);
                            if (lockOXS.IsNotNull())
                            {
                                lockOXS.Flag = LockOXSFlag.Spend;
                                lockOXS.SpendIndex = block.Index;
                                batch.Put(SliceBuilder.Begin(WalletBizPersistencePrefixes.TX_Once_MyLockOXS).Add(outputkey), SliceBuilder.Begin().Add(lockOXS));
                            }
                        }
                        this.MyLockAssets.Remove(outputkey);
                        batch.Delete(SliceBuilder.Begin(WalletBizPersistencePrefixes.TX_MyLockAsset).Add(outputkey));
                    }
                    if (this.AllLockAssets.ContainsKey(outputkey))
                    {
                        this.AllLockAssets.Remove(outputkey);
                        batch.Delete(SliceBuilder.Begin(WalletBizPersistencePrefixes.TX_LockAsset_Record).Add(outputkey));
                    }
                    var assetTrustOutputKey = new AssetTrustOutputKey { TxId = kp.Key.PrevHash, N = kp.Key.PrevIndex };
                    if (this.AssetTrustUTXO.Remove(assetTrustOutputKey))
                    {
                        batch.Delete(SliceBuilder.Begin(WalletBizPersistencePrefixes.AssetTrust_UTXO).Add(assetTrustOutputKey));
                    }
                    var ethMapOutputKey = new EthMapOutputKey { TxId = kp.Key.PrevHash, N = kp.Key.PrevIndex };
                    if (this.EthMapUTXO.Remove(ethMapOutputKey))
                    {
                        batch.Delete(SliceBuilder.Begin(WalletBizPersistencePrefixes.MY_Eth_Map_UTXO).Add(ethMapOutputKey));
                    }
                    if (OXRunTime.RunMode == RunMode.Server && this.AllEthereumMapUTXOs.Remove(kp.Key))
                    {
                        batch.Delete(SliceBuilder.Begin(WalletBizPersistencePrefixes.ALL_Eth_Map_UTXO).Add(kp.Key));
                    }
                }
                //utxo
                if (tx.Outputs.IsNotNullAndEmpty())
                {
                    for (ushort k = 0; k < tx.Outputs.Length; k++)
                    {
                        var output = tx.Outputs[k];
                        if (this.AssetTrustContacts.ContainsKey(output.ScriptHash))
                        {
                            var key = new AssetTrustOutputKey { TxId = tx.Hash, N = k };
                            batch.Put(SliceBuilder.Begin(WalletBizPersistencePrefixes.AssetTrust_UTXO).Add(key), SliceBuilder.Begin().Add(output));
                            this.AssetTrustUTXO[key] = output;
                        }
                        if (this.Wallet is OpenWallet openWallet && openWallet.TryGetEthAccount(output.ScriptHash, out OpenAccount _))
                        {
                            var key = new EthMapOutputKey { TxId = tx.Hash, N = k };
                            batch.Put(SliceBuilder.Begin(WalletBizPersistencePrefixes.MY_Eth_Map_UTXO).Add(key), SliceBuilder.Begin().Add(output));
                            this.EthMapUTXO[key] = output;
                        }
                        if (OXRunTime.RunMode == RunMode.Server && this.AllEthereumMaps.TryGetValue(output.ScriptHash, out EthereumMapTransactionMerge emt))
                        {
                            CoinReference cr = new CoinReference { PrevHash = tx.Hash, PrevIndex = k };
                            EthOutputMerge eom = new EthOutputMerge { Output = output, LockExpirationIndex = emt.EthereumMapTransaction.LockExpirationIndex, EthAddress = emt.EthereumMapTransaction.EthereumAddress };
                            batch.Put(SliceBuilder.Begin(WalletBizPersistencePrefixes.ALL_Eth_Map_UTXO).Add(cr), SliceBuilder.Begin().Add(eom));
                            this.AllEthereumMapUTXOs[cr] = eom;
                        }
                    }
                }
                m++;
            }
            this.Db.Write(WriteOptions.Default, batch);
            if (hasEventTransaction)
                Bapp.PushEvent(new BappEvent { EventItems = new BappEventItem[] { new BappEventItem() { EventType = WalletBappEventType.EventTransactionEvent.Value(), Arg = block } } });
        }
        public void OnSecretLetterTransaction(WriteBatch batch, Block block, SecretLetterTransaction slt)
        {
            if (this.HashAccounts.TryGetValue(slt.ToHash, out AccountPack ap))
            {
                batch.Save_SecretLetterTransaction(this, block, slt, ap);
            }
        }
        public IEnumerable<KeyValuePair<BoardKey, UInt256>> GetRangeBoards(uint indexrange)
        {
            var builder = SliceBuilder.Begin(WalletBizPersistencePrefixes.Event_Board).Add(indexrange);
            return this.Db.Find(ReadOptions.Default, builder, (k, v) =>
            {
                var ks = k.ToArray();
                var length = ks.Length - sizeof(byte);
                ks = ks.TakeLast(length).ToArray();
                byte[] data = v.ToArray();
                return new KeyValuePair<BoardKey, UInt256>(ks.AsSerializable<BoardKey>(), data.AsSerializable<UInt256>());
            });
        }
        public IEnumerable<KeyValuePair<SecretLetterKey, SecretLetterTransaction>> GetMyLetters()
        {
            var builder = SliceBuilder.Begin(WalletBizPersistencePrefixes.SecrectLetter_Inbox);
            return this.Db.Find(ReadOptions.Default, builder, (k, v) =>
            {
                var ks = k.ToArray();
                var length = ks.Length - sizeof(byte);
                ks = ks.TakeLast(length).ToArray();
                byte[] data = v.ToArray();
                return new KeyValuePair<SecretLetterKey, SecretLetterTransaction>(ks.AsSerializable<SecretLetterKey>(), data.AsSerializable<SecretLetterTransaction>());
            });
        }
        public IEnumerable<KeyValuePair<MyBookKey, BookTransaction>> GetMyBooks()
        {
            var builder = SliceBuilder.Begin(WalletBizPersistencePrefixes.Book_Record_My);
            return this.Db.Find(ReadOptions.Default, builder, (k, v) =>
            {
                var ks = k.ToArray();
                var length = ks.Length - sizeof(byte);
                ks = ks.TakeLast(length).ToArray();
                byte[] data = v.ToArray();
                return new KeyValuePair<MyBookKey, BookTransaction>(ks.AsSerializable<MyBookKey>(), data.AsSerializable<BookTransaction>());
            });
        }
        public IEnumerable<KeyValuePair<HolderBoardKey, UInt256>> GetBoardsByHolder(UInt160 holder)
        {
            var builder = SliceBuilder.Begin(WalletBizPersistencePrefixes.Event_Board_Holder).Add(holder);
            return this.Db.Find(ReadOptions.Default, builder, (k, v) =>
            {
                var ks = k.ToArray();
                var length = ks.Length - sizeof(byte);
                ks = ks.TakeLast(length).ToArray();
                byte[] data = v.ToArray();
                return new KeyValuePair<HolderBoardKey, UInt256>(ks.AsSerializable<HolderBoardKey>(), data.AsSerializable<UInt256>());
            });
        }
        public UInt256 GetBoard(BoardKey key)
        {
            Slice value;
            if (this.Db.TryGet(ReadOptions.Default, SliceBuilder.Begin(WalletBizPersistencePrefixes.Event_Board).Add(key), out value))
            {
                byte[] data = value.ToArray();
                return data.AsSerializable<UInt256>();
            }
            else
            {
                return default;
            }
        }
        public EngravePageState GetEngravePageState(BoardKey key)
        {
            Slice value;
            if (this.Db.TryGet(ReadOptions.Default, SliceBuilder.Begin(WalletBizPersistencePrefixes.Event_Engrave_Page_State).Add(key), out value))
            {
                byte[] data = value.ToArray();
                return data.AsSerializable<EngravePageState>();
            }
            else
            {
                return default;
            }
        }
        public HashPage GetEngravePageHash(BoardKey key, uint pageIndex)
        {
            Slice value;
            if (this.Db.TryGet(ReadOptions.Default, SliceBuilder.Begin(WalletBizPersistencePrefixes.Event_Engrave_Page_Index).Add(key).Add(pageIndex), out value))
            {
                byte[] data = value.ToArray();
                return data.AsSerializable<HashPage>();
            }
            else
            {
                return default;
            }
        }
        public DiggPageState GetDiggPageState(UInt256 engraveId)
        {
            Slice value;
            if (this.Db.TryGet(ReadOptions.Default, SliceBuilder.Begin(WalletBizPersistencePrefixes.Event_Digg_Page_State).Add(engraveId), out value))
            {
                byte[] data = value.ToArray();
                return data.AsSerializable<DiggPageState>();
            }
            else
            {
                return default;
            }
        }
        public HashPage GetDiggPageHash(UInt256 engraveId, uint pageIndex)
        {
            Slice value;
            if (this.Db.TryGet(ReadOptions.Default, SliceBuilder.Begin(WalletBizPersistencePrefixes.Event_Digg_Page_Index).Add(engraveId).Add(pageIndex), out value))
            {
                byte[] data = value.ToArray();
                return data.AsSerializable<HashPage>();
            }
            else
            {
                return default;
            }

        }
        public IEnumerable<KeyValuePair<EngraveHolder, Engrave>> GetEngravesByHolder(UInt160 holder)
        {
            var builder = SliceBuilder.Begin(WalletBizPersistencePrefixes.Event_Engrave_Holder).Add(holder);
            return this.Db.Find(ReadOptions.Default, builder, (k, v) =>
            {
                var ks = k.ToArray();
                var length = ks.Length - sizeof(byte);
                ks = ks.TakeLast(length).ToArray();
                byte[] data = v.ToArray();
                return new KeyValuePair<EngraveHolder, Engrave>(ks.AsSerializable<EngraveHolder>(), data.AsSerializable<Engrave>());
            });
        }
        public IEnumerable<KeyValuePair<OutputKey, LockAssetMerge>> GetMyAllLockAssets()
        {
            return this.Db.Find(ReadOptions.Default, SliceBuilder.Begin(WalletBizPersistencePrefixes.TX_MyLockAsset), (k, v) =>
            {
                var ks = k.ToArray();
                var length = ks.Length - sizeof(byte);
                ks = ks.TakeLast(length).ToArray();
                byte[] data = v.ToArray();
                return new KeyValuePair<OutputKey, LockAssetMerge>(ks.AsSerializable<OutputKey>(), data.AsSerializable<LockAssetMerge>());
            });
        }
        public IEnumerable<KeyValuePair<OutputKey, LockAssetMerge>> GeTAllLockAssets()
        {
            return this.Db.Find(ReadOptions.Default, SliceBuilder.Begin(WalletBizPersistencePrefixes.TX_LockAsset_Record), (k, v) =>
            {
                var ks = k.ToArray();
                var length = ks.Length - sizeof(byte);
                ks = ks.TakeLast(length).ToArray();
                byte[] data = v.ToArray();
                return new KeyValuePair<OutputKey, LockAssetMerge>(ks.AsSerializable<OutputKey>(), data.AsSerializable<LockAssetMerge>());
            });
        }
        public IEnumerable<KeyValuePair<UInt160, AssetTrustContract>> GetAllAssetTrustContracts()
        {
            return this.Db.Find(ReadOptions.Default, SliceBuilder.Begin(WalletBizPersistencePrefixes.AssetTrust_Contract), (k, v) =>
            {
                var ks = k.ToArray();
                var length = ks.Length - sizeof(byte);
                ks = ks.TakeLast(length).ToArray();
                byte[] data = v.ToArray();
                return new KeyValuePair<UInt160, AssetTrustContract>(ks.AsSerializable<UInt160>(), data.AsSerializable<AssetTrustContract>());
            });
        }
        public IEnumerable<KeyValuePair<AssetTrustOutputKey, TransactionOutput>> GetAllAssetTrustUTXOs()
        {
            return this.Db.Find(ReadOptions.Default, SliceBuilder.Begin(WalletBizPersistencePrefixes.AssetTrust_UTXO), (k, v) =>
            {
                var ks = k.ToArray();
                var length = ks.Length - sizeof(byte);
                ks = ks.TakeLast(length).ToArray();
                byte[] data = v.ToArray();
                return new KeyValuePair<AssetTrustOutputKey, TransactionOutput>(ks.AsSerializable<AssetTrustOutputKey>(), data.AsSerializable<TransactionOutput>());
            });
        }

        public IEnumerable<KeyValuePair<AssetTrustOutputKey, TransactionOutput>> GetAssetTrustUTXOs(UInt160 contractScriptHash, UInt256 assetId = default)
        {
            if (assetId.IsNotNull())
                return this.AssetTrustUTXO.Where(m => m.Value.ScriptHash.Equals(contractScriptHash) && m.Value.AssetId.Equals(assetId));
            else
                return this.AssetTrustUTXO.Where(m => m.Value.ScriptHash.Equals(contractScriptHash));
        }
        public IEnumerable<KeyValuePair<EthMapOutputKey, TransactionOutput>> GetAllEthMapUTXOs()
        {
            return this.Db.Find(ReadOptions.Default, SliceBuilder.Begin(WalletBizPersistencePrefixes.MY_Eth_Map_UTXO), (k, v) =>
            {
                var ks = k.ToArray();
                var length = ks.Length - sizeof(byte);
                ks = ks.TakeLast(length).ToArray();
                byte[] data = v.ToArray();
                return new KeyValuePair<EthMapOutputKey, TransactionOutput>(ks.AsSerializable<EthMapOutputKey>(), data.AsSerializable<TransactionOutput>());
            });
        }
        public IEnumerable<KeyValuePair<EthMapOutputKey, TransactionOutput>> GetEthMapUTXOs(UInt160 ethMapAddress, UInt256 assetId = default)
        {
            if (assetId.IsNotNull())
                return this.EthMapUTXO.Where(m => m.Value.ScriptHash.Equals(ethMapAddress) && m.Value.AssetId.Equals(assetId));
            else
                return this.EthMapUTXO.Where(m => m.Value.ScriptHash.Equals(ethMapAddress));
        }
        public WalletSettingValue GetWalletSetting(WalletSettingKind settingKind)
        {
            WalletSettingKey key = new WalletSettingKey { Key = settingKind };
            if (!this.WalletSettings.TryGetValue(key, out WalletSettingValue value))
            {
                value = this.Get<WalletSettingValue>(WalletBizPersistencePrefixes.Wallet_Setting, key);
            }
            return value;
        }
        public void SetWalletSetting(WriteBatch batch, WalletSettingKind settingKind, WalletSettingValue value)
        {
            WalletSettingKey key = new WalletSettingKey { Key = settingKind };
            batch.Put(SliceBuilder.Begin(WalletBizPersistencePrefixes.Wallet_Setting).Add(key), SliceBuilder.Begin().Add(value));
            this.WalletSettings[key] = value;
        }

    }
}
