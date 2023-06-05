using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using OX.IO;
using OX.Network.P2P.Payloads;
using OX.Cryptography.ECC;

namespace OX.Wallets.Base
{
    public class OutputKey : ISerializable
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
            if (obj is OutputKey k)
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
    public class LockAssetMerge : ISerializable
    {
        public LockAssetTransaction Tx;
        public TransactionOutput Output;
        public virtual int Size => Tx.Size + Output.Size;
        public void Serialize(BinaryWriter writer)
        {
            writer.Write(Tx);
            writer.Write(Output);
        }
        public void Deserialize(BinaryReader reader)
        {
            Tx = reader.ReadSerializable<LockAssetTransaction>();
            Output = reader.ReadSerializable<TransactionOutput>();
        }

    }
    public enum LockOXSFlag : byte
    {
        Unspend = 1 << 0,
        Spend = 1 << 1,
        Claimed = 1 << 2,
    }
    public class LockOXS : ISerializable
    {
        public UInt160 Holder;
        public TransactionOutput Output;
        public LockAssetTransaction Tx;
        public LockOXSFlag Flag;
        public uint Index;
        public uint SpendIndex;
        public virtual int Size => Holder.Size + Output.Size + Tx.Size + sizeof(LockOXSFlag) + sizeof(uint) + sizeof(uint);
        public void Serialize(BinaryWriter writer)
        {
            writer.Write(Holder);
            writer.Write(Output);
            writer.Write(Tx);
            writer.Write((byte)Flag);
            writer.Write(Index);
            writer.Write(SpendIndex);
        }
        public void Deserialize(BinaryReader reader)
        {
            Holder = reader.ReadSerializable<UInt160>();
            Output = reader.ReadSerializable<TransactionOutput>();
            Tx = reader.ReadSerializable<LockAssetTransaction>();
            Flag = (LockOXSFlag)reader.ReadByte();
            Index = reader.ReadUInt32();
            SpendIndex = reader.ReadUInt32();
        }

    }
}
