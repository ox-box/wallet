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

namespace OX.Wallets.Base
{
    public class MyBookKey : ISerializable
    {
        public UInt160 Author;
        public uint Index;
        public ushort N;
        public virtual int Size => Author.Size + sizeof(uint) + sizeof(ushort);
        public void Serialize(BinaryWriter writer)
        {
            writer.Write(Author);
            writer.Write(Index);
            writer.Write(N);
        }
        public void Deserialize(BinaryReader reader)
        {
            Author = reader.ReadSerializable<UInt160>();
            Index = reader.ReadUInt32();
            N = reader.ReadUInt16();
        }
        public override bool Equals(object obj)
        {
            if (obj is MyBookKey mbk)
            {
                return mbk.Index == this.Index && mbk.N == this.N;
            }
            return base.Equals(obj);
        }
        public override int GetHashCode()
        {
            return $"{this.Index}-{this.N}-{this.Author.ToAddress()}".GetHashCode();
        }
    }
    public class BookKey : ISerializable
    {
        public uint Index;
        public ushort N;
        public virtual int Size => sizeof(uint) + sizeof(ushort);
        public void Serialize(BinaryWriter writer)
        {
            writer.Write(Index);
            writer.Write(N);
        }
        public void Deserialize(BinaryReader reader)
        {
            Index = reader.ReadUInt32();
            N = reader.ReadUInt16();
        }
        public override bool Equals(object obj)
        {
            if (obj is MyBookKey mbk)
            {
                return mbk.Index == this.Index && mbk.N == this.N;
            }
            return base.Equals(obj);
        }
        public override int GetHashCode()
        {
            return $"{this.Index}-{this.N}".GetHashCode();
        }
    }
}
