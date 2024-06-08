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
    public enum TalkKind : byte
    {
        Inbox = 0x01,
        OutBox = 0x02
    }
    public class TalkLineKey : ISerializable
    {
        public UInt256 TalkLine;
        public uint Range;
        public TalkKind TalkKind;
        public UInt256 FMHash;
        public virtual int Size => TalkLine.Size + sizeof(uint) + sizeof(TalkKind) + FMHash.Size;
        public void Serialize(BinaryWriter writer)
        {
            writer.Write(TalkLine);
            writer.Write(Range);
            writer.Write((byte)TalkKind);
            writer.Write(FMHash);
        }
        public void Deserialize(BinaryReader reader)
        {
            TalkLine = reader.ReadSerializable<UInt256>();
            Range = reader.ReadUInt32();
            TalkKind = (TalkKind)reader.ReadByte();
            FMHash = reader.ReadSerializable<UInt256>();
        }
    }
    public class FlashUnicastRecord : ISerializable
    {
        public FlashUnicast FlashUnicast;
        public uint Timestamp;
        public uint RecordIndex;
        public virtual int Size => FlashUnicast.Size + sizeof(uint) + sizeof(uint);
        public void Serialize(BinaryWriter writer)
        {
            writer.Write(FlashUnicast);
            writer.Write(Timestamp);
            writer.Write(RecordIndex);
        }
        public void Deserialize(BinaryReader reader)
        {
            FlashUnicast = reader.ReadSerializable<FlashUnicast>();
            Timestamp = reader.ReadUInt32();
            RecordIndex = reader.ReadUInt32();
        }
    }
    public class FlashMulticastRecord : ISerializable
    {
        public FlashMulticast FlashUnicast;
        public uint Timestamp;
        public uint RecordIndex;
        public virtual int Size => FlashUnicast.Size + sizeof(uint) + sizeof(uint);
        public void Serialize(BinaryWriter writer)
        {
            writer.Write(FlashUnicast);
            writer.Write(Timestamp);
            writer.Write(RecordIndex);
        }
        public void Deserialize(BinaryReader reader)
        {
            FlashUnicast = reader.ReadSerializable<FlashMulticast>();
            Timestamp = reader.ReadUInt32();
            RecordIndex = reader.ReadUInt32();
        }
    }
    public class UnicastTalkLineValue : ISerializable
    {
        public UInt160 Local;
        public ECPoint Remote;
        public string Label = string.Empty;
        public virtual int Size => Local.Size + Remote.Size + Label.GetVarSize();
        public void Serialize(BinaryWriter writer)
        {
            writer.Write(Local);
            writer.Write(Remote);
            writer.Write(Label);
        }
        public void Deserialize(BinaryReader reader)
        {
            Local = reader.ReadSerializable<UInt160>();
            Remote = reader.ReadSerializable<ECPoint>();
            Label = reader.ReadVarString();
        }
        public override bool Equals(object obj)
        {
            if (obj is UnicastTalkLineValue utlk)
            {
                return utlk.Local.Equals(this.Local) && utlk.Remote.Equals(this.Remote);
            }
            return base.Equals(obj);
        }
        public override int GetHashCode()
        {
            var s = $"{this.Local.ToString()}-{this.Remote.ToString()}";
            return s.GetHashCode();
        }
        public override string ToString()
        {
            return $"{this.Local.ToString()}-{this.Remote.ToString()}";
        }
    }
    public class MulticastTalkLineValue : ISerializable
    {
        public UInt160 Local;
        public byte[] Key;
        public string Label;
        public virtual int Size => Local.Size + Key.GetVarSize() + Label.GetVarSize();
        public void Serialize(BinaryWriter writer)
        {
            writer.Write(Local);
            writer.WriteVarBytes(Key);
            writer.WriteVarString(Label);
        }
        public void Deserialize(BinaryReader reader)
        {
            Local = reader.ReadSerializable<UInt160>();
            Key = reader.ReadVarBytes();
            Label = reader.ReadVarString();
        }
        public override bool Equals(object obj)
        {
            if (obj is MulticastTalkLineValue mtlv)
            {
                return mtlv.Key.SequenceEqual(this.Key) && mtlv.Label == this.Label;
            }
            return base.Equals(obj);
        }
        public override int GetHashCode()
        {
            return this.Key.ToHexString().GetHashCode() + this.Label.GetHashCode();
        }
        public override string ToString()
        {
            return $"{this.Key.ToHexString()}-{this.Label}";
        }
    }
    public class FlashStateCommentKey : ISerializable
    {
        public UInt256 FSHash;
        public UInt256 ParentCommentHash;
        public UInt256 CommentHash;
        public virtual int Size => FSHash.Size + ParentCommentHash.Size + CommentHash.Size;
        public void Serialize(BinaryWriter writer)
        {
            writer.Write(FSHash);
            writer.Write(ParentCommentHash);
            writer.Write(CommentHash);
        }
        public void Deserialize(BinaryReader reader)
        {
            FSHash = reader.ReadSerializable<UInt256>();
            ParentCommentHash = reader.ReadSerializable<UInt256>();
            CommentHash = reader.ReadSerializable<UInt256>();
        }
    }
    public class FlashStateCommentValue : ISerializable
    {
        public UInt160 Sender;
        public byte[] Data;
        public uint Timestamp;
        public virtual int Size => Sender.Size + Data.GetVarSize() + sizeof(uint);
        public void Serialize(BinaryWriter writer)
        {
            writer.Write(Sender);
            writer.WriteVarBytes(Data);
            writer.Write(Timestamp);
        }
        public void Deserialize(BinaryReader reader)
        {
            Sender = reader.ReadSerializable<UInt160>();
            Data = reader.ReadVarBytes();
            Timestamp = reader.ReadUInt32();
        }
    }
    public class SenderFlashStateKey : ISerializable
    {
        public UInt160 Sender;
        public uint Range;
        public UInt256 FSHash;
        public virtual int Size => Sender.Size + sizeof(uint) + FSHash.Size;
        public void Serialize(BinaryWriter writer)
        {
            writer.Write(Sender);
            writer.Write(Range);
            writer.Write(FSHash);
        }
        public void Deserialize(BinaryReader reader)
        {
            Sender = reader.ReadSerializable<UInt160>();
            Range = reader.ReadUInt32();
            FSHash = reader.ReadSerializable<UInt256>();
        }
    }
    public class FlashStateRecord : ISerializable
    {
        public FlashState FlashState;
        public uint Timestamp;
        public uint CommentCount;
        public virtual int Size => FlashState.Size + sizeof(uint) + sizeof(uint);
        public void Serialize(BinaryWriter writer)
        {
            writer.Write(FlashState);
            writer.Write(Timestamp);
            writer.Write(CommentCount);
        }
        public void Deserialize(BinaryReader reader)
        {
            FlashState = reader.ReadSerializable<FlashState>();
            Timestamp = reader.ReadUInt32();
            CommentCount = reader.ReadUInt32();
        }
    }
    public class GlobalFlashStateKey : ISerializable
    {
        public uint Range;
        public UInt256 FSHash;
        public virtual int Size => sizeof(uint) + FSHash.Size;
        public void Serialize(BinaryWriter writer)
        {
            writer.Write(Range);
            writer.Write(FSHash);
        }
        public void Deserialize(BinaryReader reader)
        {
            Range = reader.ReadUInt32();
            FSHash = reader.ReadSerializable<UInt256>();
        }
    }
    public class TagFlashStateKey : ISerializable
    {
        public FlashStateTag Tag;
        public uint Range;
        public UInt256 FSHash;
        public virtual int Size => Tag.Size + sizeof(uint) + FSHash.Size;
        public void Serialize(BinaryWriter writer)
        {
            writer.Write(Tag);
            writer.Write(Range);
            writer.Write(FSHash);
        }
        public void Deserialize(BinaryReader reader)
        {
            Tag = reader.ReadSerializable<FlashStateTag>();
            Range = reader.ReadUInt32();
            FSHash = reader.ReadSerializable<UInt256>();
        }
    }
    public class CommentCountValue : ISerializable
    {
        public Uint32Wrapper Count;
        public uint Timestamp;
        public virtual int Size => Count.Size + sizeof(uint) ;
        public void Serialize(BinaryWriter writer)
        {
            writer.Write(Count);
            writer.Write(Timestamp);
        }
        public void Deserialize(BinaryReader reader)
        {
            Count = reader.ReadSerializable<Uint32Wrapper>();
            Timestamp = reader.ReadUInt32();
        }
    }
}
