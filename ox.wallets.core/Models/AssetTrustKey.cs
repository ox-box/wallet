using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using OX.IO;
using OX.Network.P2P.Payloads;
using OX.Cryptography.ECC;
using OX.Ledger;
using OX.SmartContract;
using OX.VM;

namespace OX.Wallets
{
    public class AssetTrustOutputKey : ISerializable
    {
        public UInt256 TxId;
        public ushort N;
        public virtual int Size => TxId.Size + sizeof(ushort);
        public void Serialize(BinaryWriter writer)
        {
            writer.Write(TxId);
            writer.Write(N);
        }
        public void Deserialize(BinaryReader reader)
        {
            TxId = reader.ReadSerializable<UInt256>();
            N = reader.ReadUInt16();
        }
        public override bool Equals(object obj)
        {
            if (obj is AssetTrustOutputKey k)
            {
                return this.TxId.Equals(k.TxId) && this.N == k.N;
            }
            return base.Equals(obj);
        }
        public override int GetHashCode()
        {
            return TxId.GetHashCode() + N;
        }
        public override string ToString()
        {
            return $"{TxId.ToString()}-{N}";
        }
    }
    public class AssetTrustOutput : ISerializable
    {
        public TransactionOutput OutPut;
        public bool WaitSpent;
        public virtual int Size => OutPut.Size + sizeof(bool);
        public AssetTrustOutput() { }
        public AssetTrustOutput(TransactionOutput output)
        {
            this.OutPut = output;
            this.WaitSpent = false;
        }
        public void Serialize(BinaryWriter writer)
        {
            writer.Write(OutPut);
            writer.Write(WaitSpent);
        }
        public void Deserialize(BinaryReader reader)
        {
            OutPut = reader.ReadSerializable<TransactionOutput>();
            WaitSpent = reader.ReadBoolean();
        }

    }
    public class AssetTrustContract : ISerializable
    {
        public ECPoint Trustee;
        public ECPoint Truster;
        public bool IsMustRelateTruster;
        public UInt160[] Targets;
        public UInt160[] SideScopes;
        public UInt160 TrustContract;


        public int Size => Trustee.Size + Truster.Size + sizeof(bool) + Targets.GetVarSize() + SideScopes.GetVarSize() + TrustContract.Size;
        public void Deserialize(BinaryReader reader)
        {
            Trustee = reader.ReadSerializable<ECPoint>();
            Truster = reader.ReadSerializable<ECPoint>();
            IsMustRelateTruster = reader.ReadBoolean();
            Targets = reader.ReadSerializableArray<UInt160>();
            SideScopes = reader.ReadSerializableArray<UInt160>();
            TrustContract = reader.ReadSerializable<UInt160>();
        }

        public void Serialize(BinaryWriter writer)
        {
            writer.Write(Trustee);
            writer.Write(Truster);
            writer.Write(IsMustRelateTruster);
            writer.Write(Targets);
            writer.Write(SideScopes);
            writer.Write(TrustContract);
        }
        public Contract GetContract()
        {
            using (ScriptBuilder sb = new ScriptBuilder())
            {
                sb.EmitPush(this.Trustee);
                sb.EmitPush(this.Truster);
                byte[] targetDatas = new byte[0];
                foreach (var target in this.Targets.OrderBy(p => p))
                {
                    targetDatas = targetDatas.Concat(target.ToArray()).ToArray();
                }
                sb.EmitPush(targetDatas);
                sb.EmitPush(this.IsMustRelateTruster);
                var sideDatas = new byte[0];
                foreach (var sideScope in this.SideScopes.OrderBy(p => p))
                {
                    sideDatas = sideDatas.Concat(sideScope.ToArray()).ToArray();
                }
                sb.EmitPush(sideDatas);
                sb.EmitAppCall(this.TrustContract);
                return Contract.Create(new[] { ContractParameterType.Signature }, sb.ToArray());
            }
        }
    }

}
