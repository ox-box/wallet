using OX.IO.Json;
using OX.Ledger;
using OX.Network.P2P.Payloads;
using OX.SmartContract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using OX.Wallets.NEP6;
using OX.Cryptography;
using OX.Bapps;
using OX.VM.Types;

namespace OX.Wallets
{
    public class OpenWallet : NEP6Wallet
    {
        public const int MAXTRANSACTIONCOUNT = 20;
        public Func<Dictionary<CoinReference, MyLockAssetMerge>> GetMyLockAssetUTXO { get; set; }
        public Func<Dictionary<CoinReference, LockAssetMerge>> GetAllLockAssetRecord { get; set; }
        public Func<Dictionary<UInt160, AssetTrustContract>> GetAssetTrustContacts { get; set; }
        public Func<Dictionary<AssetTrustOutputKey, AssetTrustOutput>> GetAssetTrustUTXO { get; set; }
        public Func<Dictionary<EthMapOutputKey, TransactionOutput>> GetEthMapUTXO { get; set; }
        public Func<Dictionary<UInt160, EthereumMapTransactionMerge>> GetAllEthereumMaps { get; set; }
        public Func<Dictionary<CoinReference, EthOutputMerge>> GetAllEthereumMapUTXOs { get; set; }
        public string WalletPassword => this.password;
        protected Dictionary<string, OpenAccount> openaccounts { get; private set; } = new Dictionary<string, OpenAccount>();
        public IEnumerable<OpenAccount> EthAccounts => openaccounts.Values.Where(m => m.AccountKind == 60);
        public IEnumerable<OpenAccount> BTCAccounts => openaccounts.Values.Where(m => m.AccountKind == 0);
        public string EncryptedMnemonics { get; private set; }
        public string Mnemonics { get; private set; }
        public string WalletPath { get { return this.path; } }
        public INotecase Notecase { get; private set; }
        public OpenWallet(INotecase notecase, string path, string name = default) : base(notecase.GetIndexer(path), path, name)
        {
            this.Notecase = notecase;
        }

        public virtual void CreateOpenAccount(byte[] privateKey, string address, string publickey, int accountKind)
        {
            var act = new OpenAccount(this, privateKey, accountKind);
            act.AccountKind = accountKind;
            act.Address = address;
            act.PublicKey = publickey;
            act.Key = act.EncodeOpenAccountPrivateKey(this.password, this.Scrypt.N, this.Scrypt.R, this.Scrypt.P);
            this.openaccounts[address] = act.BuildMapAccount();
        }
        protected override void LoadWallet()
        {
            var obj = (JArray)wallet["openaccounts"];
            if (obj.IsNotNullAndEmpty())
                this.openaccounts = obj.Select(p => OpenAccount.FromJson(p, this).BuildMapAccount()).ToDictionary(p => p.Address);
            EncryptedMnemonics = wallet["mnemonics"]?.AsString();
            foreach (var module in Bapps.Bapp.AllUIModules())
            {
                module.Value.LoadBappModuleWalletSection(wallet[module.Value.ModuleName]);
            }
        }
        public override void OnSave()
        {
            wallet["openaccounts"] = new JArray(openaccounts.Values.Select(p => p.ToJson()));
            wallet["mnemonics"] = this.EncryptedMnemonics;
            foreach (var module in Bapps.Bapp.AllUIModules())
            {
                if (module.Value.moduleWalletSection.IsNotNull())
                {
                    wallet[module.Value.ModuleName] = module.Value.moduleWalletSection;
                }
            }
            base.OnSave();
        }
        public byte[] Decrypt(byte[] data)
        {
            Rfc2898DeriveBytes deriver = new Rfc2898DeriveBytes(this.password, new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 });
            var aes = Aes.Create();
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;
            aes.Key = deriver.GetBytes(16);
            aes.Key = deriver.GetBytes(24);
            aes.Key = deriver.GetBytes(32);
            aes.IV = deriver.GetBytes(16);
            ICryptoTransform transform = aes.CreateDecryptor();
            return transform.TransformFinalBlock(data, 0, data.Length);
        }
        public void DecryptMnemonics()
        {
            if (EncryptedMnemonics.IsNotNullAndEmpty())
            {
                var data = this.EncryptedMnemonics.HexToBytes();
                var bs = Decrypt(data);
                this.Mnemonics = Encoding.UTF8.GetString(bs);
            }
        }
        public byte[] Encrypt(byte[] data)
        {
            Rfc2898DeriveBytes deriver = new Rfc2898DeriveBytes(this.password, new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 });
            var aes = Aes.Create();
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;
            aes.Key = deriver.GetBytes(16);
            aes.Key = deriver.GetBytes(24);
            aes.Key = deriver.GetBytes(32);
            aes.IV = deriver.GetBytes(16);
            ICryptoTransform transform = aes.CreateEncryptor();
            return transform.TransformFinalBlock(data, 0, data.Length);
        }
        public void EncryptMnemonics(string nmenonics)
        {
            var m = Encoding.UTF8.GetBytes(nmenonics);
            var bs = Encrypt(m);
            this.EncryptedMnemonics = bs.ToHexString();
        }
        public bool TryGetEthAccount(UInt160 ethMapAddress, out OpenAccount openAccount)
        {
            openAccount = default;
            if (this.EthAccounts.IsNotNullAndEmpty())
            {
                var act = this.EthAccounts.FirstOrDefault(m => m.HaveMapAccount && m.MapAccount.MapScriptHash == ethMapAddress);
                if (act.IsNotNull())
                {
                    openAccount = act;
                    return true;
                }
            }
            return false;
        }
        public bool TryGetBTCAccount(UInt160 btcMapAddress, out OpenAccount openAccount)
        {
            openAccount = default;
            if (this.BTCAccounts.IsNotNullAndEmpty())
            {
                var act = this.BTCAccounts.FirstOrDefault(m => m.HaveMapAccount && m.MapAccount.MapScriptHash == btcMapAddress);
                if (act.IsNotNull())
                {
                    openAccount = act;
                    return true;
                }
            }
            return false;
        }
        public void ReSetIndex(string path, string defaultPathSetting)
        {
            var p = path.Substring(0, path.LastIndexOf('.'));
            if (!Directory.Exists(p))
                Directory.CreateDirectory(p);
            BaseBappProvider.WalletIndexDirectory = p;
            this.indexer = new WalletIndexer(p + "\\" + defaultPathSetting);
        }
        public bool TryGetWalletAccountBalance(UInt160 walletAccountScrptHash, out Dictionary<UInt256, WalletAccountBalance> balances)
        {
            balances = new Dictionary<UInt256, WalletAccountBalance>();
            var Coins = FindUnspentCoins(walletAccountScrptHash);
            if (Coins.IsNotNullAndEmpty())
            {
                foreach (var cs in Coins.GroupBy(m => m.Output.AssetId))
                {
                    var v = cs.Sum(m => m.Output.Value);
                    balances[cs.Key] = new WalletAccountBalance(walletAccountScrptHash, cs.Key) { TotalBalance = v, MasterBalance = v, AvailableBalance = v };
                }
            }
            var las = this.GetMyLockAssetUTXO()?.Where(m => m.Value.Owner.Equals(walletAccountScrptHash));
            if (las.IsNotNullAndEmpty())
            {
                foreach (var l in las.GroupBy(m => m.Value.Output.AssetId))
                {
                    var lasV = l.Sum(m => m.Value.Output.Value);
                    if (!balances.TryGetValue(l.Key, out WalletAccountBalance balance))
                    {
                        balance = new WalletAccountBalance(walletAccountScrptHash, l.Key);
                        balances[l.Key] = balance;
                    }
                    balance.TotalBalance += lasV;
                    balance.TotalLockBalance += lasV;
                    balance.LockAssets = l;
                    var AvailableLas = l.Where(m => m.Value.SpentIndex == 0 && ((m.Value.Tx.IsTimeLock && DateTime.Now.ToTimestamp() > m.Value.Tx.LockExpiration) || (!m.Value.Tx.IsTimeLock && Blockchain.Singleton.Height > m.Value.Tx.LockExpiration)));
                    if (AvailableLas.IsNotNullAndEmpty())
                    {
                        var alblv = AvailableLas.Sum(m => m.Value.Output.Value);
                        balance.TotalUnlockBalance += alblv;
                        balance.AvailableBalance += alblv;
                    }
                }
            }
            return balances.IsNotNullAndEmpty();
        }
        #region build transaction
        bool sortSearch(int MaxTxCount, IEnumerable<MixUTXO> items, long amount, out MixUTXO[] selectedutxos, out long remainder)
        {
            List<MixUTXO> result = new List<MixUTXO>();
            var utxos = items.OrderBy(m => m.Amount);
            int C = utxos.Count();
            IOrderedEnumerable<MixUTXO> range = default;
            if (C <= MaxTxCount)
            {
                range = utxos;
            }
            else
            {
                for (int i = 0; i <= C - MaxTxCount; i++)
                {
                    range = utxos.Take(new Range(new Index(i), new Index(i + MaxTxCount))).OrderBy(m => m.Amount);
                    if (range.Sum(m => m.Amount.GetInternalValue()) >= amount) break;
                }
            }
            if (range.IsNotNullAndEmpty())
            {
                var total = range.Sum(m => m.Amount.GetInternalValue());
                var surplus = amount;
                if (total >= amount)
                {
                    foreach (var item in range.OrderBy(m => m.Amount.GetInternalValue()))
                    {
                        if (surplus > 0)
                        {
                            result.Add(item);
                            surplus -= item.Amount.GetInternalValue();
                        }
                        else break;
                    }
                }
                if (surplus <= 0)
                {
                    selectedutxos = result.ToArray();
                    remainder = selectedutxos.Sum(m => m.Amount.GetInternalValue()) - amount;
                    return true;
                }
            }
            selectedutxos = default;
            remainder = 0;
            return false;
        }
        List<MixUTXO> queryMixUtxos(UInt160 from, UInt256 assetId, Dictionary<CoinReference, MyLockAssetMerge> LockAssets)
        {
            List<MixUTXO> mixUTXOs = new List<MixUTXO>();
            var unspentCoins = FindUnspentCoins(from)?.Where(m => m.Output.AssetId.Equals(assetId))?.Select(m => new MixUTXO { Owner = m.Output.ScriptHash, Amount = m.Output.Value, AssetId = m.Output.AssetId, IsLockCoin = false, UnlockCoin = m });
            if (unspentCoins.IsNotNullAndEmpty())
                mixUTXOs.AddRange(unspentCoins);
            var las = LockAssets?.Where(m => m.Value.SpentIndex == 0 && m.Value.Owner.Equals(from) && m.Value.Output.AssetId.Equals(assetId) && ((m.Value.Tx.IsTimeLock && DateTime.Now.ToTimestamp() > m.Value.Tx.LockExpiration) || (!m.Value.Tx.IsTimeLock && Blockchain.Singleton.Height > m.Value.Tx.LockExpiration)))?.Select(m => new MixUTXO { Owner = m.Value.Owner, AssetId = m.Value.Output.AssetId, Amount = m.Value.Output.Value, IsLockCoin = true, LockCoin = m });
            if (las.IsNotNullAndEmpty())
                mixUTXOs.AddRange(las);
            return mixUTXOs;
        }
        public List<MixUTXO> FindMixUnspentUtxos(UInt160 from)
        {
            List<MixUTXO> mixUTXOs = new List<MixUTXO>();
            var unspentCoins = FindUnspentCoins(from)?.Select(m => new MixUTXO { Owner = m.Output.ScriptHash, Amount = m.Output.Value, AssetId = m.Output.AssetId, IsLockCoin = false, UnlockCoin = m });
            if (unspentCoins.IsNotNullAndEmpty())
                mixUTXOs.AddRange(unspentCoins);
            var las = GetMyAvailableLockAssetUTXO(from);
            if (las.IsNotNullAndEmpty())
                mixUTXOs.AddRange(las);
            return mixUTXOs;
        }
        public int GetMyAvailableLockAssetUTXONumber(UInt160 from)
        {
            var utoxs = GetMyAvailableLockAssetUTXO(from);
            return utoxs.IsNotNullAndEmpty() ? utoxs.Count() : 0;
        }
        public IEnumerable<MixUTXO> GetMyAvailableLockAssetUTXO(UInt160 from)
        {
            return this.GetMyLockAssetUTXO()?.Where(m => m.Value.SpentIndex == 0 && m.Value.Owner.Equals(from) && ((m.Value.Tx.IsTimeLock && DateTime.Now.ToTimestamp() > m.Value.Tx.LockExpiration) || (!m.Value.Tx.IsTimeLock && Blockchain.Singleton.Height > m.Value.Tx.LockExpiration)))?.Select(m => new MixUTXO { Owner = m.Value.Owner, AssetId = m.Value.Output.AssetId, Amount = m.Value.Output.Value, IsLockCoin = true, LockCoin = m });
        }
        public bool MixBuildAndRelaySingleOutputTransaction<T>(T tx, UInt160 from, Action<T> transactionCompleted) where T : Transaction
        {
            var newTx = tx.Outputs.IsNullOrEmpty() ? MixBuildNoneOutputTransaction(tx, from) : MixBuildSingleOutputTransaction(tx, from);
            if (newTx.IsNull()) return false;
            if (newTx.IsNotNull())
            {
                this.ApplyTransaction(newTx);
                this.Notecase.Relay(newTx);
                if (transactionCompleted != default)
                {
                    transactionCompleted(newTx);
                }
                return true;
            }
            return false;
        }
        public T MixBuildSingleOutputTransaction<T>(T tx, UInt160 from) where T : Transaction
        {
            if (tx.Outputs.IsNullOrEmpty()) return default;
            if (tx.Outputs.Count() != 1) return default;
            var walletAccount = this.GetAccount(from);
            if (walletAccount.IsNull()) return default;
            if (tx.Attributes.IsNullOrEmpty())
                tx.Attributes = new TransactionAttribute[0];
            if (tx.Inputs.IsNullOrEmpty())
                tx.Inputs = new CoinReference[0];
            var myLockAssets = this.GetMyLockAssetUTXO();
            var output = tx.Outputs[0];
            var assetId = output.AssetId;
            var amount = output.Value;
            var fee = tx.SystemFee;
            if (fee > Fixed8.Zero && !assetId.Equals(Blockchain.OXC))
            {
                var mixUTXOs = queryMixUtxos(from, assetId, myLockAssets);
                if (sortSearch(MAXTRANSACTIONCOUNT - 5, mixUTXOs, amount.GetInternalValue(), out MixUTXO[] selectedUtxos, out long remainder))
                {
                    var feeMaxTxCount = MAXTRANSACTIONCOUNT - selectedUtxos.Count();
                    var feeUTXOs = queryMixUtxos(from, Blockchain.OXC, myLockAssets);
                    if (sortSearch(feeMaxTxCount, mixUTXOs, amount.GetInternalValue(), out MixUTXO[] selectedFeeUtxos, out long feeRemainder))
                    {
                        var outputList = new List<TransactionOutput>(tx.Outputs);
                        if (remainder > 0)
                            outputList.Add(new TransactionOutput { AssetId = assetId, ScriptHash = from, Value = new Fixed8(remainder) });
                        if (feeRemainder > 0)
                            outputList.Add(new TransactionOutput { AssetId = Blockchain.OXC, ScriptHash = from, Value = new Fixed8(feeRemainder) });
                        tx.Outputs = outputList.ToArray();
                        List<CoinReference> crfs = new List<CoinReference>();
                        List<AvatarAccount> avatars = new List<AvatarAccount>();
                        foreach (var utxo in selectedUtxos)
                        {
                            if (utxo.IsLockCoin)
                            {
                                avatars.Add(LockAssetHelper.CreateAccount(this, utxo.LockCoin.Value.Tx.GetContract(), walletAccount.GetKey()));
                                crfs.Add(utxo.LockCoin.Key);
                            }
                            else
                            {
                                avatars.Add(LockAssetHelper.CreateAccount(this, walletAccount.Contract, walletAccount.GetKey()));
                                crfs.Add(utxo.UnlockCoin.Reference);
                            }
                        }
                        foreach (var utxo in selectedFeeUtxos)
                        {
                            if (utxo.IsLockCoin)
                            {
                                avatars.Add(LockAssetHelper.CreateAccount(this, utxo.LockCoin.Value.Tx.GetContract(), walletAccount.GetKey()));
                                crfs.Add(utxo.LockCoin.Key);
                            }
                            else
                            {
                                avatars.Add(LockAssetHelper.CreateAccount(this, walletAccount.Contract, walletAccount.GetKey()));
                                crfs.Add(utxo.UnlockCoin.Reference);
                            }
                        }
                        tx.Inputs = crfs.ToArray();
                        return LockAssetHelper.Build(tx, avatars.ToArray());
                    }
                }
            }
            else
            {
                amount += fee;
                var mixUTXOs = queryMixUtxos(from, assetId, myLockAssets);
                if (sortSearch(MAXTRANSACTIONCOUNT, mixUTXOs, amount.GetInternalValue(), out MixUTXO[] selectedUtxos, out long remainder))
                {
                    var outputList = new List<TransactionOutput>(tx.Outputs);
                    if (remainder > 0)
                        outputList.Add(new TransactionOutput { AssetId = assetId, ScriptHash = from, Value = new Fixed8(remainder) });
                    tx.Outputs = outputList.ToArray();
                    List<CoinReference> crfs = new List<CoinReference>();
                    List<AvatarAccount> avatars = new List<AvatarAccount>();
                    foreach (var utxo in selectedUtxos)
                    {
                        if (utxo.IsLockCoin)
                        {
                            avatars.Add(LockAssetHelper.CreateAccount(this, utxo.LockCoin.Value.Tx.GetContract(), walletAccount.GetKey()));
                            crfs.Add(utxo.LockCoin.Key);
                        }
                        else
                        {
                            avatars.Add(LockAssetHelper.CreateAccount(this, walletAccount.Contract, walletAccount.GetKey()));
                            crfs.Add(utxo.UnlockCoin.Reference);
                        }
                    }
                    tx.Inputs = crfs.ToArray();
                    return LockAssetHelper.Build(tx, avatars.ToArray());
                }
            }
            return default;
        }
        public T MixBuildNoneOutputTransaction<T>(T tx, UInt160 from) where T : Transaction
        {
            if (tx.Outputs.IsNotNullAndEmpty()) return default;
            var walletAccount = this.GetAccount(from);
            if (walletAccount.IsNull()) return default;
            if (tx.Attributes.IsNullOrEmpty())
                tx.Attributes = new TransactionAttribute[0];
            if (tx.Inputs.IsNullOrEmpty())
                tx.Inputs = new CoinReference[0];
            var myLockAssets = this.GetMyLockAssetUTXO();
            var fee = tx.SystemFee;
            var mixUTXOs = queryMixUtxos(from, Blockchain.OXC, myLockAssets);
            if (sortSearch(MAXTRANSACTIONCOUNT, mixUTXOs, fee.GetInternalValue(), out MixUTXO[] selectedUtxos, out long remainder))
            {
                var outputList = new List<TransactionOutput>();
                if (remainder > 0)
                    outputList.Add(new TransactionOutput { AssetId = Blockchain.OXC, ScriptHash = from, Value = new Fixed8(remainder) });
                tx.Outputs = outputList.ToArray();
                List<CoinReference> crfs = new List<CoinReference>();
                List<AvatarAccount> avatars = new List<AvatarAccount>();
                foreach (var utxo in selectedUtxos)
                {
                    if (utxo.IsLockCoin)
                    {
                        avatars.Add(LockAssetHelper.CreateAccount(this, utxo.LockCoin.Value.Tx.GetContract(), walletAccount.GetKey()));
                        crfs.Add(utxo.LockCoin.Key);
                    }
                    else
                    {
                        avatars.Add(LockAssetHelper.CreateAccount(this, walletAccount.Contract, walletAccount.GetKey()));
                        crfs.Add(utxo.UnlockCoin.Reference);
                    }
                }
                tx.Inputs = crfs.ToArray();
                return LockAssetHelper.Build(tx, avatars.ToArray());
            }
            return default;
        }
        #endregion
    }
}
