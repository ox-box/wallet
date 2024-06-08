using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using OX.IO;
using OX.Network.P2P;
using OX.Network.P2P.Payloads;
using System.Windows.Forms.VisualStyles;
using OX.Cryptography.ECC;
using OX.Wallets.Letters;

namespace OX.Wallets.Base
{
    public class AssetIssueKey : ISerializable
    {
        public UInt256 AssetId;
        public UInt256 IssueTx;
        public Fixed8 Amount;
        public virtual int Size => AssetId.Size + IssueTx.Size + Amount.Size;
        public void Serialize(BinaryWriter writer)
        {
            writer.Write(AssetId);
            writer.Write(IssueTx);
            writer.Write(Amount);
        }
        public void Deserialize(BinaryReader reader)
        {
            AssetId = reader.ReadSerializable<UInt256>();
            IssueTx = reader.ReadSerializable<UInt256>();
            Amount = reader.ReadSerializable<Fixed8>();
        }
    }
    public enum SecretLetterKind : byte
    {
        Inbox = 0x01,
        OutBox = 0x02
    }
    public class LetterPair : ISerializable
    {
        public UInt160 Local;
        public ECPoint Remote;

        public virtual int Size => Local.Size + Remote.Size;
        public void Serialize(BinaryWriter writer)
        {
            writer.Write(Local);
            writer.Write(Remote);
        }
        public void Deserialize(BinaryReader reader)
        {
            Local = reader.ReadSerializable<UInt160>();
            Remote = reader.ReadSerializable<ECPoint>();
        }
    }
    public class SecretLetterKey : ISerializable
    {
        public UInt256 LetterLine;
        public UInt256 LetterId;

        public virtual int Size => LetterLine.Size + LetterId.Size;
        public void Serialize(BinaryWriter writer)
        {
            writer.Write(LetterLine);
            writer.Write(LetterId);
        }
        public void Deserialize(BinaryReader reader)
        {
            LetterLine = reader.ReadSerializable<UInt256>();
            LetterId = reader.ReadSerializable<UInt256>();
        }
    }
    public class SecretLetterState : ISerializable
    {
        public SecretLetterTransaction SecretLetterTransaction;
        public SecretLetterKind LetterKind;
        public uint Index;
        public uint Timestamp;

        public virtual int Size => SecretLetterTransaction.Size + sizeof(SecretLetterKind) + sizeof(uint) + sizeof(uint);
        public void Serialize(BinaryWriter writer)
        {
            writer.Write(SecretLetterTransaction);
            writer.Write((byte)LetterKind);
            writer.Write(Index);
            writer.Write(Timestamp);
        }
        public void Deserialize(BinaryReader reader)
        {
            SecretLetterTransaction = reader.ReadSerializable<SecretLetterTransaction>();
            LetterKind = (SecretLetterKind)reader.ReadByte();
            Index = reader.ReadUInt32();
            Timestamp = reader.ReadUInt32();
        }
    }
}
