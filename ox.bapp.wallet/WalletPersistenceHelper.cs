using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using OX.IO.Data.LevelDB;
using OX.Network.P2P.Payloads;
using OX.IO;
using OX.SmartContract;
using OX.Ledger;
using OX.Persistence;
using OX.Cryptography.AES;
using Akka.Util.Internal;
using OX.Wallets.Base.Events;
using OX.Wallets.Base.Wallets;

namespace OX.Wallets.Base
{
    public static partial class WalletPersistenceHelper
    {
        public static void Save_Event_Board(this WriteBatch batch, Block block, EventTransaction model, ushort n)
        {
            if (model.IsNotNull())
            {
                var board = model.Data.AsSerializable<Board>();
                if (board.IsNotNull())
                {
                    BoardKey key = new BoardKey() { BoardTxIndex = block.Index, BoardTxPosition = n };
                    HolderBoardKey hkey = new HolderBoardKey() { Holder = model.ScriptHash, BoardTxIndex = block.Index, BoardTxPosition = n };
                    batch.Put(SliceBuilder.Begin(WalletBizPersistencePrefixes.Event_Board).Add(key), SliceBuilder.Begin().Add(model.Hash));
                    batch.Put(SliceBuilder.Begin(WalletBizPersistencePrefixes.Event_Board_Holder).Add(hkey), SliceBuilder.Begin().Add(model.Hash));
                }
            }
        }
        public static void Save_Event_Engrave(this WriteBatch batch, IWalletProvider persistence, Block block, EventTransaction model, ushort n)
        {
            if (model.IsNotNull())
            {
                var engrave = model.Data.AsSerializable<Engrave>();
                if (engrave.IsNotNull())
                {
                    BoardKey bkey = new BoardKey() { BoardTxIndex = engrave.BoardTxIndex, BoardTxPosition = engrave.BoardTxPosition };
                    EngraveKey key = new EngraveKey() { BoardTxIndex = engrave.BoardTxIndex, BoardTxPosition = engrave.BoardTxPosition, Index = block.Index, EngraveHash = model.Hash };
                    batch.Put(SliceBuilder.Begin(WalletBizPersistencePrefixes.Event_Engrave).Add(key), SliceBuilder.Begin().Add(model.ScriptHash));
                    var pageState = persistence.GetEngravePageState(bkey);
                    if (pageState.IsNull()) pageState = new EngravePageState();
                    pageState.EngraveCount++;
                    pageState.LastEngraveId = model.Hash;
                    batch.Put(SliceBuilder.Begin(WalletBizPersistencePrefixes.Event_Engrave_Page_State).Add(bkey), SliceBuilder.Begin().Add(pageState));
                    var pageIndex = pageState.LastPageIndex;
                    var hashPage = persistence.GetEngravePageHash(bkey, pageIndex);
                    if (hashPage.IsNull()) hashPage = new HashPage();
                    hashPage.AddHash(model.Hash);
                    batch.Put(SliceBuilder.Begin(WalletBizPersistencePrefixes.Event_Engrave_Page_Index).Add(bkey).Add(pageIndex), SliceBuilder.Begin().Add(hashPage));
                    if (persistence.Wallet.IsNotNull())
                    {
                        EngraveHolder en = new EngraveHolder() { Holder = model.ScriptHash, EngraveHash = model.Hash };
                        if (persistence.Wallet.ContainsAndHeld(model.ScriptHash))
                            batch.Put(SliceBuilder.Begin(WalletBizPersistencePrefixes.Event_Engrave_Holder).Add(en), SliceBuilder.Begin().Add(engrave));
                    }
                }
            }
        }
        public static void Save_Event_Digg(this WriteBatch batch, IWalletProvider persistence, Block block, EventTransaction model, ushort n)
        {
            if (model.IsNull()) return;
            var digg = model.Data.AsSerializable<Digg>();
            if (digg.IsNull()) return;
            var tx = Blockchain.Singleton.GetTransaction(digg.EngraveId);
            if (tx.IsNull()) return;
            if (tx is EventTransaction et && et.EventType == EventType.Engrave)
            {
                var engrave = et.Data.AsSerializable<Engrave>();
                if (engrave.IsNull()) return;
                DiggKey key = new DiggKey() { BoardTxIndex = engrave.BoardTxIndex, BoardTxPosition = engrave.BoardTxPosition, EngraveHash = digg.EngraveId, DiggHash = model.Hash };
                batch.Put(SliceBuilder.Begin(WalletBizPersistencePrefixes.Event_Digg).Add(key), SliceBuilder.Begin().Add(model.ScriptHash));
                var pageState = persistence.GetDiggPageState(digg.EngraveId);
                if (pageState.IsNull()) pageState = new DiggPageState();
                pageState.DiggCount++;
                pageState.LastDiggId = model.Hash;
                batch.Put(SliceBuilder.Begin(WalletBizPersistencePrefixes.Event_Digg_Page_State).Add(digg.EngraveId), SliceBuilder.Begin().Add(pageState));
                var pageIndex = pageState.LastPageIndex;
                var hashPage = persistence.GetDiggPageHash(digg.EngraveId, pageIndex);
                if (hashPage.IsNull()) hashPage = new HashPage();
                hashPage.AddHash(model.Hash);
                batch.Put(SliceBuilder.Begin(WalletBizPersistencePrefixes.Event_Digg_Page_Index).Add(digg.EngraveId).Add(pageIndex), SliceBuilder.Begin().Add(hashPage));
            }
        }
        public static void Save_NFT_Transfer(this WriteBatch batch, WalletBappProvider provider, Block block, NftTransferTransaction nftdonate, ushort n)
        {
            if (nftdonate.IsNotNull())
            {
                if (provider.Wallet.IsNotNull())
                {
                    var oldHolder = nftdonate.Auth?.Target.Target;
                    var newHolder = nftdonate.NFSHolder;
                    if (nftdonate.NftChangeType == NftChangeType.Transfer)
                    {
                        var dotnateKey = provider.Get<MyNFTTransferKey>(WalletBizPersistencePrefixes.NFT_Transfer_Hash_My, nftdonate.Auth.Target.PreHash);
                        if (dotnateKey.IsNotNull())
                            batch.Delete(SliceBuilder.Begin(WalletBizPersistencePrefixes.NFT_Transfer_My).Add(dotnateKey));
                        batch.Delete(SliceBuilder.Begin(WalletBizPersistencePrefixes.NFT_Transfer_Hash_My).Add(nftdonate.Auth.Target.PreHash));
                        NFTSellKey nsk = new NFTSellKey { NftId = nftdonate.NFSStateKey.NFCID, Index = block.Index, N = n };
                        NFTSellValue nsv = new NFTSellValue { Amount = nftdonate.Auth.Target.Amount, To = newHolder, From = oldHolder, TransferHash = nftdonate.Hash, Time = block.Timestamp };
                        batch.Put(SliceBuilder.Begin(WalletBizPersistencePrefixes.NFT_Transfer_Record).Add(nsk), SliceBuilder.Begin().Add(nsv));
                    }
                    var nkey = new NFSStateKey { NFCID = nftdonate.NFSStateKey.NFCID, IssueBlockIndex = nftdonate.NFSStateKey.IssueBlockIndex, IssueN = nftdonate.NFSStateKey.IssueN };
                    if (nftdonate.NftChangeType == NftChangeType.Issue)
                    {
                        nkey.IssueBlockIndex = block.Index;
                        nkey.IssueN = n;
                    }
                    batch.Put(SliceBuilder.Begin(WalletBizPersistencePrefixes.NFT_Issue_Record).Add(nkey), SliceBuilder.Begin().Add(nftdonate));
                    if (OXRunTime.RunMode == RunMode.Server)
                    {
                        if (nftdonate.NftChangeType == NftChangeType.Transfer)
                        {
                            if (oldHolder.MixAccountType == Network.P2P.MixAccountType.Ethereum)
                            {
                                var oldEthAddress = oldHolder.AsEthAddress();
                                EthNftTransferKey oldEkey = new EthNftTransferKey { NFSStateKey = nftdonate.NFSStateKey, EthAddress = new NFT.StringWrapper(oldEthAddress.ToLower()) };
                                batch.Delete(SliceBuilder.Begin(WalletBizPersistencePrefixes.NFT_Transfer_Record_Server).Add(oldEkey));
                            }
                        }
                        if (nftdonate.NFSHolder.MixAccountType == Network.P2P.MixAccountType.Ethereum)
                        {
                            var ethAddress = nftdonate.NFSHolder.AsEthAddress();
                            EthNftTransferKey ekey = new EthNftTransferKey { NFSStateKey = nkey, EthAddress = new NFT.StringWrapper(ethAddress.ToLower()) };
                            batch.Put(SliceBuilder.Begin(WalletBizPersistencePrefixes.NFT_Transfer_Record_Server).Add(ekey), SliceBuilder.Begin().Add(nftdonate));
                        }
                    }
                    if (newHolder.MixAccountType == Network.P2P.MixAccountType.OX && newHolder.Verify())
                    {
                        var sh = newHolder.AsOXAddress();
                        if (provider.Wallet.ContainsAndHeld(sh))
                        {
                            MyNFTTransferKey key = new MyNFTTransferKey { Holder = sh, Index = block.Index, N = n };
                            batch.Put(SliceBuilder.Begin(WalletBizPersistencePrefixes.NFT_Transfer_My).Add(key), SliceBuilder.Begin().Add(nftdonate));
                            batch.Put(SliceBuilder.Begin(WalletBizPersistencePrefixes.NFT_Transfer_Hash_My).Add(nftdonate.Hash), SliceBuilder.Begin().Add(key));
                        }
                    }
                }
            }
        }
        public static void Save_NFT(this WriteBatch batch, WalletBappProvider provider, Block block, NftTransaction nftcoin, ushort n)
        {
            if (nftcoin.IsNotNull())
            {
                if (provider.Wallet.IsNotNull())
                {
                    var sh = Contract.CreateSignatureRedeemScript(nftcoin.Author).ToScriptHash();

                    var Counter = provider.GetWalletSetting(WalletSettingKind.NFTCoin_Counter);
                    var count = Counter.IsNotNull() ? BitConverter.ToUInt32(Counter.Data) : 0;
                    count++;
                    NFTCoinKey nk = new NFTCoinKey { Range = count / 10, Index = block.Index, N = n };
                    batch.Put(SliceBuilder.Begin(WalletBizPersistencePrefixes.NFT_Coin).Add(nk), SliceBuilder.Begin().Add(nftcoin));
                    provider.SetWalletSetting(batch, WalletSettingKind.NFTCoin_Counter, new WalletSettingValue { Data = BitConverter.GetBytes(count) });
                    if (provider.Wallet.ContainsAndHeld(sh))
                    {
                        MyNFTCoinKey key = new MyNFTCoinKey { Author = sh, Index = block.Index, N = n };
                        batch.Put(SliceBuilder.Begin(WalletBizPersistencePrefixes.NFT_Coin_My).Add(key), SliceBuilder.Begin().Add(nftcoin));
                    }
                }
            }
        }
        //public static void Save_NFT_Coin(this WriteBatch batch, WalletBappProvider provider, Block block, NFTCoinTransaction nftcoin, ushort n)
        //{
        //    if (nftcoin.IsNotNull())
        //    {
        //        if (provider.Wallet.IsNotNull())
        //        {
        //            var sh = Contract.CreateSignatureRedeemScript(nftcoin.Author).ToScriptHash();

        //            var Counter = provider.GetWalletSetting(WalletSettingKind.NFTCoin_Counter);
        //            var count = Counter.IsNotNull() ? BitConverter.ToUInt32(Counter.Data) : 0;
        //            count++;
        //            NFTCoinKey nk = new NFTCoinKey { Range = count / 10, Index = block.Index, N = n };
        //            batch.Put(SliceBuilder.Begin(WalletBizPersistencePrefixes.NFT_Coin).Add(nk), SliceBuilder.Begin().Add(nftcoin));
        //            provider.SetWalletSetting(batch, WalletSettingKind.NFTCoin_Counter, new WalletSettingValue { Data = BitConverter.GetBytes(count) });
        //            if (provider.Wallet.ContainsAndHeld(sh))
        //            {
        //                MyNFTCoinKey key = new MyNFTCoinKey { Author = sh, Index = block.Index, N = n };
        //                batch.Put(SliceBuilder.Begin(WalletBizPersistencePrefixes.NFT_Coin_My).Add(key), SliceBuilder.Begin().Add(nftcoin));
        //            }
        //        }
        //    }
        //}
        //public static void Save_NFT_Donate(this WriteBatch batch, WalletBappProvider provider, Block block, NFTDonateTransaction nftdonate, ushort n)
        //{
        //    if (nftdonate.IsNotNull())
        //    {
        //        if (provider.Wallet.IsNotNull())
        //        {
        //            var oldSh = Contract.CreateSignatureRedeemScript(nftdonate.DonateAuthentication.Target.PublicKey).ToScriptHash();
        //            if (provider.Wallet.ContainsAndHeld(oldSh) && nftdonate.DonateAuthentication.Target.NFTDonateType != NFTDonateType.Issue)
        //            {
        //                var dotnateKey = provider.Get<MyNFTDonateKey>(WalletBizPersistencePrefixes.NFT_Donate_Hash_My, nftdonate.DonateAuthentication.Target.PreHash);
        //                if (dotnateKey.IsNotNull())
        //                    batch.Delete(SliceBuilder.Begin(WalletBizPersistencePrefixes.NFT_Donate_My).Add(dotnateKey));
        //                batch.Delete(SliceBuilder.Begin(WalletBizPersistencePrefixes.NFT_Donate_Hash_My).Add(nftdonate.DonateAuthentication.Target.PreHash));
        //            }
        //            var sh = nftdonate.NFTOwner;
        //            var nftdonateKey = new NFTDonateKey { NFTCoinHash = nftdonate.NFTDonateStateKey.NFTCoinHash };
        //            if (nftdonate.DonateAuthentication.Target.NFTDonateType == NFTDonateType.Issue)
        //            {
        //                nftdonateKey.IssueIndex = block.Index;
        //                nftdonateKey.IssueN = n;
        //            }
        //            else
        //            {
        //                nftdonateKey.IssueIndex = nftdonate.NFTDonateStateKey.IssueBlockIndex;
        //                nftdonateKey.IssueN = nftdonate.NFTDonateStateKey.IssueN;
        //            }
        //            batch.Put(SliceBuilder.Begin(WalletBizPersistencePrefixes.NFT_Donate).Add(nftdonateKey), SliceBuilder.Begin().Add(nftdonate));
        //            if (nftdonate.DonateAuthentication.Target.NFTDonateType == NFTDonateType.Sell)
        //            {
        //                NFTSellKey nsk = new NFTSellKey { NFTCoinHash = nftdonate.NFTDonateStateKey.NFTCoinHash, Index = block.Index, N = n };
        //                NFTSellValue nsv = new NFTSellValue { Amount = nftdonate.DonateAuthentication.Target.NFTDonateSell.Amount, To = sh, From = oldSh, DonateHash = nftdonate.Hash, Time = block.Timestamp };
        //                batch.Put(SliceBuilder.Begin(WalletBizPersistencePrefixes.NFT_Sell_Record).Add(nsk), SliceBuilder.Begin().Add(nsv));
        //            }

        //            if (provider.Wallet.ContainsAndHeld(sh))
        //            {
        //                MyNFTDonateKey key = new MyNFTDonateKey { NewOwner = sh, Index = block.Index, N = n };
        //                batch.Put(SliceBuilder.Begin(WalletBizPersistencePrefixes.NFT_Donate_My).Add(key), SliceBuilder.Begin().Add(new NFTDonateTxMerge { NFTDonate = nftdonate, NFSStateKey = nftdonateKey }));
        //                batch.Put(SliceBuilder.Begin(WalletBizPersistencePrefixes.NFT_Donate_Hash_My).Add(nftdonate.Hash), SliceBuilder.Begin().Add(key));
        //            }
        //        }
        //    }
        //}
        public static void TrySave_ExcludeLockAssetTransaction(this WriteBatch batch, WalletBappProvider provider, Block block, Transaction tx, ushort blockN)
        {
            if (tx is LockAssetTransaction) return;
            for (ushort n = 0; n < tx.Outputs.Length; n++)
            {
                var output = tx.Outputs[n];
                if (provider.LockAssetMetas.TryGetValue(output.ScriptHash, out MyLockAssetMeta lockAssetMeta))
                {
                    var holder = Contract.CreateSignatureRedeemScript(lockAssetMeta.Tx.Recipient).ToScriptHash();
                    var key = new CoinReference { PrevHash = tx.Hash, PrevIndex = n };
                    var LockAssetMerge = new LockAssetMerge { Tx = lockAssetMeta.Tx, Output = output, IsNativeLock = false };
                    var MyLockAssetMerge = new MyLockAssetMerge { Owner = holder, Tx = lockAssetMeta.Tx, Output = output, IsNativeLock = false, SpentIndex = 0 };
                    if (provider.Wallet.ContainsAndHeld(holder))
                    {
                        if (output.AssetId.Equals(Blockchain.OXS))
                        {
                            batch.Put(SliceBuilder.Begin(WalletBizPersistencePrefixes.TX_Once_MyLockOXS).Add(key), SliceBuilder.Begin().Add(new LockOXS { Holder = holder, Output = output, Tx = lockAssetMeta.Tx, IsNativeLock = false, Flag = LockOXSFlag.Unspend, Index = block.Index, SpendIndex = 0 }));
                        }
                        batch.Put(SliceBuilder.Begin(WalletBizPersistencePrefixes.TX_MyLockAsset).Add(key), SliceBuilder.Begin().Add(MyLockAssetMerge));
                        provider.MyLockAssets[key] = MyLockAssetMerge;
                    }
                    batch.Put(SliceBuilder.Begin(WalletBizPersistencePrefixes.TX_LockAsset_Record).Add(key), SliceBuilder.Begin().Add(LockAssetMerge));
                    provider.AllLockAssets[key] = LockAssetMerge;
                }
            }
        }
        public static void Save_LockAssetTransaction(this WriteBatch batch, WalletBappProvider provider, Block block, LockAssetTransaction lat, ushort blockN)
        {
            if (lat.IsNotNull() && lat.LockContract.Equals(Blockchain.LockAssetContractScriptHash) && provider.Wallet.IsNotNull())
            {
                var holder = Contract.CreateSignatureRedeemScript(lat.Recipient).ToScriptHash();

                var sh = lat.GetContract().ScriptHash;
                var txid = lat.Hash;
                for (ushort n = 0; n < lat.Outputs.Length; n++)
                {
                    var output = lat.Outputs[n];
                    if (output.ScriptHash.Equals(sh))
                    {
                        var key = new CoinReference { PrevHash = lat.Hash, PrevIndex = n };
                        var LockAssetMerge = new LockAssetMerge { Tx = lat, Output = output, IsNativeLock = true };
                   
                        var MyLockAssetMeta = new MyLockAssetMeta { Owner = holder, Tx = lat };
                        if (provider.Wallet.ContainsAndHeld(holder))
                        {
                            if (output.AssetId.Equals(Blockchain.OXS))
                            {
                                batch.Put(SliceBuilder.Begin(WalletBizPersistencePrefixes.TX_Once_MyLockOXS).Add(key), SliceBuilder.Begin().Add(new LockOXS { Holder = holder, Output = output, Tx = lat, IsNativeLock = true, Flag = LockOXSFlag.Unspend, Index = block.Index, SpendIndex = 0 }));
                            }
                            var MyLockAssetMerge = new MyLockAssetMerge { Owner = holder, Tx = lat, Output = output, IsNativeLock = true, SpentIndex = 0 };
                            batch.Put(SliceBuilder.Begin(WalletBizPersistencePrefixes.TX_MyLockAsset).Add(key), SliceBuilder.Begin().Add(MyLockAssetMerge));
                            provider.MyLockAssets[key] = MyLockAssetMerge;
                        }
                        if (!provider.LockAssetMetas.ContainsKey(sh))
                        {
                            batch.Put(SliceBuilder.Begin(WalletBizPersistencePrefixes.TX_LockAssetMeta).Add(sh), SliceBuilder.Begin().Add(MyLockAssetMeta));
                            provider.LockAssetMetas[sh] = MyLockAssetMeta;
                        }
                        batch.Put(SliceBuilder.Begin(WalletBizPersistencePrefixes.TX_LockAsset_Record).Add(key), SliceBuilder.Begin().Add(LockAssetMerge));
                        provider.AllLockAssets[key] = LockAssetMerge;
                    }
                }

            }
        }
        public static void Save_BookTransaction(this WriteBatch batch, WalletBappProvider provider, Block block, BookTransaction bt, ushort blockN)
        {
            if (bt.IsNotNull())
            {
                BookKey bk = new BookKey { Index = block.Index, N = blockN };
                batch.Put(SliceBuilder.Begin(WalletBizPersistencePrefixes.Book_Record).Add(bk), SliceBuilder.Begin().Add(bt));
                var sh = Contract.CreateSignatureRedeemScript(bt.Author).ToScriptHash();
                if (provider.Wallet.IsNotNull() && provider.Wallet.ContainsAndHeld(sh))
                {
                    MyBookKey mbk = new MyBookKey { Author = sh, Index = block.Index, N = blockN };
                    batch.Put(SliceBuilder.Begin(WalletBizPersistencePrefixes.Book_Record_My).Add(mbk), SliceBuilder.Begin().Add(bt));
                }
            }
        }
        public static void Save_IssueTransaction(this WriteBatch batch, WalletBappProvider provider, Block block, IssueTransaction it, ushort blockN)
        {
            if (it.IsNotNull())
            {
                foreach (var output in it.Outputs)
                {
                    if (output.Value > Fixed8.Zero)
                    {
                        AssetIssueKey bk = new AssetIssueKey { AssetId = output.AssetId, IssueTx = it.Hash, Amount = output.Value };
                        batch.Put(SliceBuilder.Begin(WalletBizPersistencePrefixes.IssueTransaction_History).Add(bk), SliceBuilder.Begin().Add(it));
                    }
                }
            }
        }
        public static void Save_SecretLetterTransaction(this WriteBatch batch, WalletBappProvider provider, Block block, SecretLetterTransaction slt, AccountPack ap)
        {
            if (slt.IsNotNull() && slt.Flag == 1)
            {
                try
                {
                    var sharekey = ap.Key.DiffieHellman(slt.From);
                    var decryptedData = slt.Data.Decrypt(sharekey);
                    SecretLetterKey slk = new SecretLetterKey
                    {
                        LetterIndex = block.Index,
                        Recipient = ap.Address,
                        From = slt.From,
                        TxId = slt.Hash,
                        Msg = slt.Data
                    };
                    batch.Put(SliceBuilder.Begin(WalletBizPersistencePrefixes.SecrectLetter_Inbox).Add(slk), SliceBuilder.Begin().Add(slt));
                }
                catch
                {

                }
            }
        }
        public static void Save_AssetTrustTransaction(this WriteBatch batch, WalletBappProvider provider, Block block, AssetTrustTransaction att, ushort blockN)
        {
            if (att.IsNotNull() && att.TrustContract.Equals(Blockchain.TrustAssetContractScriptHash) && provider.Wallet.IsNotNull())
            {
                var trustee = Contract.CreateSignatureRedeemScript(att.Trustee).ToScriptHash();
                var truster = Contract.CreateSignatureRedeemScript(att.Truster).ToScriptHash();
                if (provider.Wallet.ContainsAndHeld(trustee) || provider.Wallet.ContainsAndHeld(truster))
                {
                    var sh = att.GetContract().ScriptHash;
                    var txid = att.Hash;
                    for (ushort n = 0; n < att.Outputs.Length; n++)
                    {
                        var output = att.Outputs[n];
                        if (output.ScriptHash.Equals(sh))
                        {
                            if (!provider.AssetTrustContacts.TryGetValue(sh, out AssetTrustContract contract))
                            {
                                contract = new AssetTrustContract { IsMustRelateTruster = att.IsMustRelateTruster, Targets = att.Targets, SideScopes = att.SideScopes, TrustContract = att.TrustContract, Trustee = att.Trustee, Truster = att.Truster };
                                batch.Put(SliceBuilder.Begin(WalletBizPersistencePrefixes.AssetTrust_Contract).Add(sh), SliceBuilder.Begin().Add(contract));
                                provider.AssetTrustContacts[sh] = contract;
                            }
                            var key = new AssetTrustOutputKey { TxId = att.Hash, N = n };
                            AssetTrustOutput ato = new AssetTrustOutput(output);
                            batch.Put(SliceBuilder.Begin(WalletBizPersistencePrefixes.AssetTrust_UTXO).Add(key), SliceBuilder.Begin().Add(ato));
                            provider.AssetTrustUTXO[key] = ato;
                        }
                    }
                }

            }
        }

    }
}
