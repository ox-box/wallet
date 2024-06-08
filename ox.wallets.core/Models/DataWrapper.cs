using OX.IO;
using OX.Network.P2P.Payloads;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OX.Wallets
{
    public class StringWrapper : ISerializable
    {
        public string Text { get; private set; }
        public virtual int Size => Text.GetVarSize();
        public StringWrapper() { }
        public StringWrapper(string text) { this.Text = text; }
        public void Serialize(BinaryWriter writer)
        {
            writer.WriteVarString(Text);
        }
        public void Deserialize(BinaryReader reader)
        {
            Text = reader.ReadVarString();
        }
        public override bool Equals(object obj)
        {
            if (obj is StringWrapper sw)
            {
                return sw.Text == this.Text;
            }
            return base.Equals(obj);
        }
        public override int GetHashCode()
        {
            return this.Text.GetHashCode();
        }
        public static bool operator ==(StringWrapper left, StringWrapper right)
        {
            if (ReferenceEquals(left, right))
                return true;
            if (ReferenceEquals(left, null) || ReferenceEquals(right, null))
                return false;
            return left.Equals(right);
        }
        public static bool operator !=(StringWrapper left, StringWrapper right)
        {
            return !(left == right);
        }
    }
    public class ByteArrayWrapper : ISerializable
    {
        public byte[] ByteArray { get; private set; }
        public virtual int Size => ByteArray.GetVarSize();
        public ByteArrayWrapper() { }
        public ByteArrayWrapper(byte[] byteArray) { this.ByteArray = byteArray; }
        public void Serialize(BinaryWriter writer)
        {
            writer.WriteVarBytes(ByteArray);
        }
        public void Deserialize(BinaryReader reader)
        {
            ByteArray = reader.ReadVarBytes();
        }
    }
    public class Uint32Wrapper : ISerializable
    {
        public uint Value { get; set; }
        public virtual int Size => sizeof(uint);
        public Uint32Wrapper() { }
        public Uint32Wrapper(uint Value) { this.Value = Value; }
        public void Serialize(BinaryWriter writer)
        {
            writer.Write(Value);
        }
        public void Deserialize(BinaryReader reader)
        {
            Value = reader.ReadUInt32();
        }
    }
}
