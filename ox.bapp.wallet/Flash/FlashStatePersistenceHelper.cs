using OX.IO;
using OX.IO.Data.LevelDB;
using OX.Ledger;
using OX.Network.P2P.Payloads;
using OX.SmartContract;
using OX.Wallets.Base.Events;
using OX.Wallets.Base.Wallets;
using System;
using OX.Cryptography.ECC;
using Org.BouncyCastle.Bcpg;
using OX.Cryptography;
using OX.Wallets.Base.NFT;
using OX.Wallets;
using OX.Wallets.Base.Flash;
using Akka.Util;
using System.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace OX.Wallets.Flash
{
    public static partial class FlashStatePersistenceHelper
    {
        public static void Save_FlashState(this WriteBatch batch, FlashMessageProvider provider, FlashState fs)
        {
            if (fs.IsNotNull())
            {
                var sh = fs.Author;
                if (!provider.FlashStateCount.TryGetValue(sh, out Uint32Wrapper count))
                {
                    count = new Uint32Wrapper(0);
                    provider.FlashStateCount[sh] = count;
                }
                count.Value++;
                batch.Put(SliceBuilder.Begin(FlashStatePersistencePrefixes.FlashState_LatestCount).Add(sh), SliceBuilder.Begin().Add(count));
                if (!provider.FlashStateCount.TryGetValue(UInt160.Zero, out Uint32Wrapper zerocount))
                {
                    zerocount = new Uint32Wrapper(0);
                    provider.FlashStateCount[UInt160.Zero] = zerocount;
                }
                zerocount.Value++;
                batch.Put(SliceBuilder.Begin(FlashStatePersistencePrefixes.FlashState_LatestCount).Add(UInt160.Zero), SliceBuilder.Begin().Add(zerocount));

                SenderFlashStateKey fsk = new SenderFlashStateKey { FSHash = fs.Hash, Range = count.Value / FlashMemoryHelper.RangeSize, Sender = sh };
                GlobalFlashStateKey gfsk = new GlobalFlashStateKey { FSHash = fs.Hash, Range = zerocount.Value / FlashMemoryHelper.RangeSize };
                var fsr = new FlashStateRecord { FlashState = fs, Timestamp = DateTime.Now.ToTimestamp(), CommentCount = 0 };
                batch.Put(SliceBuilder.Begin(FlashStatePersistencePrefixes.FlashState_Record).Add(fs.Hash), SliceBuilder.Begin().Add(fsr));
                batch.Put(SliceBuilder.Begin(FlashStatePersistencePrefixes.FlashState_SenderRecord).Add(fsk), SliceBuilder.Begin().Add(fs.Hash));
                batch.Put(SliceBuilder.Begin(FlashStatePersistencePrefixes.FlashState_GlobalRecord).Add(gfsk), SliceBuilder.Begin().Add(fs.Hash));
                foreach (var tag in fs.Tags)
                {
                    if (!provider.FlashStateTagCount.TryGetValue(tag, out Uint32Wrapper tagcount))
                    {
                        tagcount = new Uint32Wrapper(0);
                        provider.FlashStateTagCount[tag] = tagcount;
                    }
                    tagcount.Value++;
                    batch.Put(SliceBuilder.Begin(FlashStatePersistencePrefixes.FlashState_TagCount).Add(tag), SliceBuilder.Begin().Add(tagcount));
                    TagFlashStateKey tfsk = new TagFlashStateKey { FSHash = fs.Hash, Range = tagcount.Value / FlashMemoryHelper.RangeSize, Tag = tag };
                    batch.Put(SliceBuilder.Begin(FlashStatePersistencePrefixes.FlashState_TagRecord).Add(tfsk), SliceBuilder.Begin().Add(fs.Hash));
                }
            }
        }
        public static void Save_FlashStateComment(this WriteBatch batch, FlashMessageProvider provider, FlashStateComment fsc)
        {
            if (fsc.IsNotNull())
            {
                var sh = fsc.Author;
                foreach (var comment in fsc.Comments)
                {
                    var fs = provider.Get<FlashStateRecord>(FlashStatePersistencePrefixes.FlashState_Record, comment.StateHash);
                    if (fs.IsNotNull())
                    {
                        var ts = DateTime.Now.ToTimestamp();
                        FlashStateCommentKey fsck = new FlashStateCommentKey { FSHash = comment.StateHash, ParentCommentHash = comment.ParentCommentHash, CommentHash = comment.Hash };
                        FlashStateCommentValue fscv = new FlashStateCommentValue { Sender = sh, Data = comment.Data, Timestamp = ts };
                        batch.Put(SliceBuilder.Begin(FlashStatePersistencePrefixes.FlashStateComment_Record).Add(fsck), SliceBuilder.Begin().Add(fscv));
                        var fsr = provider.GetFlashStatRecord(comment.StateHash);
                        if (fsr.IsNotNull())
                        {
                            fsr.CommentCount++;
                            provider.FlashStateCommentCount[comment.StateHash] = new CommentCountValue { Count = new Uint32Wrapper() { Value = fsr.CommentCount }, Timestamp = ts };
                            batch.Put(SliceBuilder.Begin(FlashStatePersistencePrefixes.FlashState_Record).Add(comment.StateHash), SliceBuilder.Begin().Add(fsr));
                            provider.CropCommentCount();
                        }
                    }
                }
            }
        }
        public static void Save_FlashMulticast(this WriteBatch batch, FlashMessageProvider provider, FlashMulticast fm, TalkKind TalkKind)
        {
            if (fm.IsNotNull())
            {
                if (!provider.TalkCount.TryGetValue(fm.TalkLine, out Uint32Wrapper count))
                {
                    count = new Uint32Wrapper(0);
                    provider.TalkCount[fm.TalkLine] = count;
                }
                count.Value++;
                batch.Put(SliceBuilder.Begin(FlashStatePersistencePrefixes.FlashTalk_Count).Add(fm.TalkLine), SliceBuilder.Begin().Add(count));

                TalkLineKey tlk = new TalkLineKey { FMHash = fm.Hash, Range = count.Value / FlashMemoryHelper.RangeSize, TalkKind = TalkKind, TalkLine = fm.TalkLine };
                var fmr = new FlashMulticastRecord { FlashUnicast = fm, Timestamp = DateTime.Now.ToTimestamp(), RecordIndex = count.Value };
                batch.Put(SliceBuilder.Begin(FlashStatePersistencePrefixes.FlashMulticast_Record).Add(tlk), SliceBuilder.Begin().Add(fmr));
                FlashMemoryHelper.MulticastQueue.Enqueue(new Tuple<TalkLineKey, FlashMulticastRecord>(tlk, fmr));
            }
        }
        public static void Save_FlashUnicast(this WriteBatch batch, FlashMessageProvider provider, AccountPack localPack, FlashUnicast fu, TalkKind TalkKind)
        {
            if (fu.IsNotNull())
            {
                if (!provider.TalkCount.TryGetValue(fu.TalkLine, out Uint32Wrapper count))
                {
                    count = new Uint32Wrapper(0);
                    provider.TalkCount[fu.TalkLine] = count;
                }
                count.Value++;
                batch.Put(SliceBuilder.Begin(FlashStatePersistencePrefixes.FlashTalk_Count).Add(fu.TalkLine), SliceBuilder.Begin().Add(count));

                TalkLineKey tlk = new TalkLineKey { FMHash = fu.Hash, Range = count.Value / FlashMemoryHelper.RangeSize, TalkKind = TalkKind, TalkLine = fu.TalkLine };
                var fur = new FlashUnicastRecord { FlashUnicast = fu, Timestamp = DateTime.Now.ToTimestamp(), RecordIndex = count.Value };
                batch.Put(SliceBuilder.Begin(FlashStatePersistencePrefixes.FlashUnicast_Record).Add(tlk), SliceBuilder.Begin().Add(fur));
                FlashMemoryHelper.UnicastQueue.Enqueue(new Tuple<TalkLineKey, FlashUnicastRecord>(tlk, fur));
                if (TalkKind == TalkKind.Inbox)
                {
                    if (!provider.UnicastTalkLines.TryGetValue(fu.TalkLine, out UnicastTalkLineValue utlv))
                    {
                        utlv = new UnicastTalkLineValue { Local = localPack.Address, Remote = fu.Sender, Label = string.Empty };
                        batch.Put(SliceBuilder.Begin(FlashStatePersistencePrefixes.FlashUnicast_TalkLine).Add(fu.TalkLine), SliceBuilder.Begin().Add(utlv));
                        provider.UnicastTalkLines[fu.TalkLine] = utlv;
                    }
                }
            }
        }
        public static void Save_MulticastTalkLine(this WriteBatch batch, FlashMessageProvider provider, UInt160 localAddress, byte[] shareKey, string msg)
        {
            var talkLine = new UInt256(Crypto.Default.Hash256(Crypto.Default.Hash256(shareKey)));
            MulticastTalkLineValue baw = new MulticastTalkLineValue { Local = localAddress, Key = shareKey, Label = msg };
            batch.Put(SliceBuilder.Begin(FlashStatePersistencePrefixes.FlashMulticast_TalkLine).Add(talkLine), SliceBuilder.Begin().Add(baw));
            provider.MulticastTalkLines[talkLine] = baw;
        }
    }
}
