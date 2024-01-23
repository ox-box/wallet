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
        public OpenWallet(WalletIndexer indexer, string path, string name = default) : base(indexer, path, name)
        {
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
    }
}
