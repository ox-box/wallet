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
    public class EthMapOutputKey : ISerializable
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
            if (obj is EthMapOutputKey k)
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
   
}
