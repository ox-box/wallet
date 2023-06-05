using OX.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms.VisualStyles;

namespace OX.Wallets.Base.Events
{
    public class HolderBoardKey : ISerializable
    {
        public UInt160 Holder;
        public uint BoardTxIndex;
        public ushort BoardTxPosition;
        public int Size => Holder.Size + sizeof(uint) + sizeof(ushort);
        public void Serialize(BinaryWriter writer)
        {
            writer.Write(Holder);
            writer.Write(BoardTxIndex);
            writer.Write(BoardTxPosition);
        }
        public void Deserialize(BinaryReader reader)
        {
            Holder = reader.ReadSerializable<UInt160>();
            BoardTxIndex = reader.ReadUInt32();
            BoardTxPosition = reader.ReadUInt16();
        }
        public string ToKey()
        {
            return $"{BoardTxIndex}-{BoardTxPosition}";
        }
    }
    public class BoardKey : ISerializable
    {
        public uint RangeIndex => BoardTxIndex - BoardTxIndex % 100000;
        public uint BoardTxIndex;
        public ushort BoardTxPosition;
        public int Size => sizeof(uint) + sizeof(uint) + sizeof(ushort);
        public void Serialize(BinaryWriter writer)
        {
            writer.Write(RangeIndex);
            writer.Write(BoardTxIndex);
            writer.Write(BoardTxPosition);
        }
        public void Deserialize(BinaryReader reader)
        {
            var rindex = reader.ReadUInt32();
            BoardTxIndex = reader.ReadUInt32();
            BoardTxPosition = reader.ReadUInt16();
        }
        public string ToKey()
        {
            return $"{BoardTxIndex}-{BoardTxPosition}";
        }
        public static bool TryParser(string Keystr, out BoardKey boardKey)
        {
            var ss = Keystr.Split('-');
            try
            {
                var index = uint.Parse(ss[0]);
                var n = ushort.Parse(ss[1]);
                boardKey = new BoardKey() { BoardTxIndex = index, BoardTxPosition = n };
                return true;
            }
            catch
            {
                boardKey = default;
                return false;
            }
        }
    }
    public class EngraveKey : ISerializable
    {
        public uint BoardTxIndex;
        public ushort BoardTxPosition;
        public uint Index;
        public UInt256 EngraveHash;
        public int Size => sizeof(uint) + sizeof(ushort) + sizeof(uint) + EngraveHash.Size;
        public void Serialize(BinaryWriter writer)
        {
            writer.Write(BoardTxIndex);
            writer.Write(BoardTxPosition);
            writer.Write(Index);
            writer.Write(EngraveHash);
        }
        public void Deserialize(BinaryReader reader)
        {
            BoardTxIndex = reader.ReadUInt32();
            BoardTxPosition = reader.ReadUInt16();
            Index = reader.ReadUInt32();
            EngraveHash = reader.ReadSerializable<UInt256>();
        }
    }
    public class EngraveHolder : ISerializable
    {
        public UInt160 Holder;
        public UInt256 EngraveHash;
        public int Size => Holder.Size + EngraveHash.Size;
        public void Serialize(BinaryWriter writer)
        {
            writer.Write(Holder);
            writer.Write(EngraveHash);
        }
        public void Deserialize(BinaryReader reader)
        {
            Holder = reader.ReadSerializable<UInt160>();
            EngraveHash = reader.ReadSerializable<UInt256>();
        }
    }
    public class DiggKey : ISerializable
    {
        public uint BoardTxIndex;
        public ushort BoardTxPosition;
        public UInt256 EngraveHash;
        public UInt256 DiggHash;
        public int Size => sizeof(uint) + sizeof(ushort) + EngraveHash.Size + DiggHash.Size;
        public void Serialize(BinaryWriter writer)
        {
            writer.Write(BoardTxIndex);
            writer.Write(BoardTxPosition);
            writer.Write(EngraveHash);
            writer.Write(DiggHash);
        }
        public void Deserialize(BinaryReader reader)
        {
            BoardTxIndex = reader.ReadUInt32();
            BoardTxPosition = reader.ReadUInt16();
            EngraveHash = reader.ReadSerializable<UInt256>();
            DiggHash = reader.ReadSerializable<UInt256>();
        }
    }
    public class EngravePageState : ISerializable
    {
        public uint EngraveCount;
        public UInt256 LastEngraveId;
        public int Size => sizeof(uint) + LastEngraveId.Size;
        public void Serialize(BinaryWriter writer)
        {
            writer.Write(EngraveCount);
            writer.Write(LastEngraveId);
        }
        public void Deserialize(BinaryReader reader)
        {
            EngraveCount = reader.ReadUInt32();
            LastEngraveId = reader.ReadSerializable<UInt256>();
        }
        public uint LastPageIndex
        {
            get
            {
                var pageIndex = EngraveCount / HashPage.MaxHashsPerPage;
                var rem = EngraveCount % HashPage.MaxHashsPerPage;
                if (rem == 0 && EngraveCount > 0)
                    pageIndex--;
                return pageIndex;
            }
        }
    }
    public class DiggPageState : ISerializable
    {
        public uint DiggCount;
        public UInt256 LastDiggId;
        public int Size => sizeof(uint) + LastDiggId.Size;
        public void Serialize(BinaryWriter writer)
        {
            writer.Write(DiggCount);
            writer.Write(LastDiggId);
        }
        public void Deserialize(BinaryReader reader)
        {
            DiggCount = reader.ReadUInt32();
            LastDiggId = reader.ReadSerializable<UInt256>();
        }
        public uint LastPageIndex
        {
            get
            {
                var pageIndex = DiggCount / HashPage.MaxHashsPerPage;
                var rem = DiggCount % HashPage.MaxHashsPerPage;
                if (rem == 0 && DiggCount > 0)
                    pageIndex--;
                return pageIndex;
            }
        }
    }
}
