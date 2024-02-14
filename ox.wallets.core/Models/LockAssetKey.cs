using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using OX.IO;
using OX.Network.P2P.Payloads;
using OX.Cryptography.ECC;

namespace OX.Wallets
{
    public class MyLockAssetMeta : ISerializable
    {
        public UInt160 Owner;
        public LockAssetTransaction Tx;
        public virtual int Size => Owner.Size + Tx.Size;

        public void Serialize(BinaryWriter writer)
        {
            writer.Write(Owner);
            writer.Write(Tx);
        }
        public void Deserialize(BinaryReader reader)
        {
            Owner = reader.ReadSerializable<UInt160>();
            Tx = reader.ReadSerializable<LockAssetTransaction>();
        }

    }

    public class MyLockAssetMerge : ISerializable
    {
        public UInt160 Owner;
        public LockAssetTransaction Tx;
        public TransactionOutput Output;
        public bool IsNativeLock;
        public uint SpentIndex;
        public virtual int Size => Owner.Size + Tx.Size + Output.Size + sizeof(bool) + sizeof(uint);
        public MyLockAssetMerge()
        {
            SpentIndex = 0;
        }
        public void Serialize(BinaryWriter writer)
        {
            writer.Write(Owner);
            writer.Write(Tx);
            writer.Write(Output);
            writer.Write(IsNativeLock);
            writer.Write(SpentIndex);
        }
        public void Deserialize(BinaryReader reader)
        {
            Owner = reader.ReadSerializable<UInt160>();
            Tx = reader.ReadSerializable<LockAssetTransaction>();
            Output = reader.ReadSerializable<TransactionOutput>();
            IsNativeLock = reader.ReadBoolean();
            SpentIndex = reader.ReadUInt32();
        }

    }
    public class LockAssetMerge : ISerializable
    {
        public LockAssetTransaction Tx;
        public TransactionOutput Output;
        public bool IsNativeLock;
        public virtual int Size => Tx.Size + Output.Size + sizeof(bool);
        public void Serialize(BinaryWriter writer)
        {
            writer.Write(Tx);
            writer.Write(Output);
            writer.Write(IsNativeLock);
        }
        public void Deserialize(BinaryReader reader)
        {
            Tx = reader.ReadSerializable<LockAssetTransaction>();
            Output = reader.ReadSerializable<TransactionOutput>();
            IsNativeLock = reader.ReadBoolean();
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
        public bool IsNativeLock;
        public LockOXSFlag Flag;
        public uint Index;
        public uint SpendIndex;
        public virtual int Size => Holder.Size + Output.Size + Tx.Size + sizeof(bool) + sizeof(LockOXSFlag) + sizeof(uint) + sizeof(uint);
        public void Serialize(BinaryWriter writer)
        {
            writer.Write(Holder);
            writer.Write(Output);
            writer.Write(Tx);
            writer.Write(IsNativeLock);
            writer.Write((byte)Flag);
            writer.Write(Index);
            writer.Write(SpendIndex);
        }
        public void Deserialize(BinaryReader reader)
        {
            Holder = reader.ReadSerializable<UInt160>();
            Output = reader.ReadSerializable<TransactionOutput>();
            Tx = reader.ReadSerializable<LockAssetTransaction>();
            IsNativeLock = reader.ReadBoolean();
            Flag = (LockOXSFlag)reader.ReadByte();
            Index = reader.ReadUInt32();
            SpendIndex = reader.ReadUInt32();
        }

    }
}
