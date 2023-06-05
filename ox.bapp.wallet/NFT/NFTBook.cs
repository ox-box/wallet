using OX.Cryptography.ECC;
using OX.IO;
using System.IO;
using OX.Ledger;
using OX.Network.P2P.Payloads;
using System;
using OX.Network.P2P;
using OX.IO.Json;
using System.Collections.Generic;
using OX.Persistence;
using System.Linq;
//using System.Runtime.InteropServices.WindowsRuntime;

namespace OX.Wallets.Base.NFT
{

    public class NFTBook
    {
        public static NFTBook Instance { get; private set; }
        public static NFTBook BuildNFTBook(JArray json, Action saveAction)
        {
            if (Instance.IsNull())
                Instance = new NFTBook(json, saveAction);
            return Instance;
        }
        Action saveWalletAction;
        public Dictionary<string, NFTBookRecord> Records = new Dictionary<string, NFTBookRecord>();
        NFTBook(JArray json, Action saveAction)
        {
            this.saveWalletAction = saveAction;
            if (json.IsNotNull())
            {
                for (int i = 0; i < json.Count; i++)
                {
                    var j = json[i];
                    if (j.IsNotNull())
                    {
                        var r = new NFTBookRecord(j);
                        if (r.TryParse())
                        {
                            Records[r.IssueID] = r;
                        }
                    }
                }
            }
        }
        public JArray ToJson()
        {
            JArray ja = new JArray();
            foreach (var record in Records)
            {
                JObject jo = new JObject();
                jo["issueid"] = record.Key;
                jo["auth"] = record.Value.Auth;
                ja.Add(jo);
            }
            return ja;
        }
        public void SaveWallet()
        {
            saveWalletAction?.Invoke();
        }
        public bool Check()
        {
            bool needSave = false;
            var keys = Records.Keys.ToArray();
            foreach (var k in keys)
            {
                var ndv = Records[k].TranferData;
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
                if (ndv.Validator.Target.MaxIndex < Blockchain.Singleton.Height || ndv.Validator.Target.Amount <= Fixed8.Zero)
                    needRemove = true;
                if (needRemove)
                {
                    Records.Remove(k);
                    needSave = true;
                    break;
                }
            }
            return needSave;
        }
        public bool Append(NFTTranferData ndv)
        {
            try
            {

                NFTBookRecord record = new NFTBookRecord
                {
                    IssueID = ndv.Key.GetNFTIssueID(),
                    Auth = ndv.ToArray().ToHexString(),
                    TranferData = ndv
                };
                this.Records[record.IssueID] = record;
                return true;
            }
            catch
            {
                return false;
            }
          
        }
    }
    public class NFTBookRecord
    {
        public string IssueID { get; set; }
        public string Auth { get; set; }
        public NFTTranferData TranferData { get; set; }
        public NFTBookRecord()
        {

        }
        public NFTBookRecord(JObject json)
        {
            if (json.IsNotNull())
            {
                this.IssueID = json["issueid"].AsString();
                this.Auth = json["auth"].AsString();
            }
        }
        public bool TryParse()
        {
            try
            {
                var ndv = this.Auth.HexToBytes().AsSerializable<NFTTranferData>();
                if (ndv.Key.GetNFTIssueID() == this.IssueID)
                {
                    TranferData = ndv;
                    return true;
                }
            }
            catch
            {
                return false;
            }
            return false;
        }
    }
    public static class NFTBookHelper
    {
        public static string GetNFTIssueID(this NFSStateKey key)
        {
            return $"{key.NFCID.CID}-{key.IssueBlockIndex}-{key.IssueN}";
        }
    }
}
