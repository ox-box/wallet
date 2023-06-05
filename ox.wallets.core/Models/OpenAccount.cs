using OX.IO.Json;
using OX.Ledger;
using OX.Network.P2P.Payloads;
using OX.SmartContract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using OX.Wallets.NEP6;
using OX.Wallets;
using OX.Cryptography;
using System.Runtime.CompilerServices;

namespace OX.Wallets
{
    public abstract class MapAccount
    {
        public UInt160 MapScriptHash { get; internal set; }
    }
    public class EthMapAccount : MapAccount
    {

    }
    public class OpenAccount
    {
        public OpenWallet Wallet { get; private set; }
        public string Address { get; internal set; }
        public string PublicKey { get; internal set; }
        byte[] privateKey;
        public string Key { get; internal set; }
        public int AccountKind { get; internal set; }
        public MapAccount MapAccount { get; private set; }
        public bool HaveMapAccount { get { return MapAccount.IsNotNull(); } }
        public uint LastTransferHeight;
        public JObject Extra;
        public OpenAccount(OpenWallet wallet, string address, string key)
        {
            this.Wallet = wallet;
            this.Address = address;
            this.Key = key;
        }
        public byte[] GetPrivateKey(string password)
        {
            if (privateKey.IsNullOrEmpty())
            {
                this.privateKey = GetOpenAccountPrivateKey(this.Key, password, Wallet.Scrypt.N, Wallet.Scrypt.R, Wallet.Scrypt.P);
            }
            return this.privateKey;
        }
        public OpenAccount(OpenWallet wallet, byte[] privatekey, int kind)
        {
            this.Wallet = wallet;
            this.privateKey = privatekey;
            this.AccountKind = kind;
        }
        public OpenAccount BuildMapAccount()
        {
            if (this.AccountKind == 60)
            {
                EthereumMapTransaction emt = new EthereumMapTransaction
                {
                    EthereumAddress = this.Address
                };
                this.MapAccount = new EthMapAccount
                {
                    MapScriptHash = emt.GetContract().ScriptHash
                };
            }
            return this;
        }
        public static OpenAccount FromJson(JObject json, OpenWallet wallet)
        {
            return new OpenAccount(wallet, json["address"].AsString(), json["key"]?.AsString())
            {
                PublicKey = json["publickey"].AsString(),
                AccountKind = int.Parse(json["kind"].AsString()),
                Extra = json["extra"]
            };
        }

        public JObject ToJson()
        {
            JObject account = new JObject();
            account["address"] = this.Address;
            account["publickey"] = this.PublicKey;
            account["key"] = this.Key;
            account["kind"] = this.AccountKind.ToString();
            account["extra"] = this.Extra;
            return account;
        }
        private static byte[] XOR(byte[] x, byte[] y)
        {
            if (x.Length != y.Length) throw new ArgumentException();
            return x.Zip(y, (a, b) => (byte)(a ^ b)).ToArray();
        }
        public string EncodeOpenAccountPrivateKey(string passphrase, int N = 16384, int r = 8, int p = 8)
        {
            byte[] addresshash = Encoding.ASCII.GetBytes(this.Address).Sha256().Sha256().Take(4).ToArray();
            byte[] derivedkey = SCrypt.DeriveKey(Encoding.UTF8.GetBytes(passphrase), addresshash, N, r, p, 64);
            byte[] derivedhalf1 = derivedkey.Take(32).ToArray();
            byte[] derivedhalf2 = derivedkey.Skip(32).ToArray();
            byte[] encryptedkey = XOR(privateKey, derivedhalf1).AES256Encrypt(derivedhalf2);
            byte[] buffer = new byte[39];
            buffer[0] = 0x01;
            buffer[1] = 0x42;
            buffer[2] = 0xe0;
            Buffer.BlockCopy(addresshash, 0, buffer, 3, addresshash.Length);
            Buffer.BlockCopy(encryptedkey, 0, buffer, 7, encryptedkey.Length);
            return buffer.Base58CheckEncode();
        }
        public static byte[] GetOpenAccountPrivateKey(string nep2, string passphrase, int N = 16384, int r = 8, int p = 8)
        {
            if (nep2 == null) throw new ArgumentNullException(nameof(nep2));
            if (passphrase == null) throw new ArgumentNullException(nameof(passphrase));
            byte[] data = nep2.Base58CheckDecode();
            if (data.Length != 39 || data[0] != 0x01 || data[1] != 0x42 || data[2] != 0xe0)
                throw new FormatException();
            byte[] addresshash = new byte[4];
            Buffer.BlockCopy(data, 3, addresshash, 0, 4);
            byte[] derivedkey = SCrypt.DeriveKey(Encoding.UTF8.GetBytes(passphrase), addresshash, N, r, p, 64);
            byte[] derivedhalf1 = derivedkey.Take(32).ToArray();
            byte[] derivedhalf2 = derivedkey.Skip(32).ToArray();
            byte[] encryptedkey = new byte[32];
            Buffer.BlockCopy(data, 7, encryptedkey, 0, 32);
            byte[] prikey = XOR(encryptedkey.AES256Decrypt(derivedhalf2), derivedhalf1);
            return prikey;
        }
    }
}
