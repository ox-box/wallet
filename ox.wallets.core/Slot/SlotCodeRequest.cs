using OX.Cryptography;
using OX.IO;
using System;
using System.IO;
using System.Linq;
using OX.Network.P2P;
using System.Globalization;

namespace OX.Wallets.Slot
{
    public class SlotCodeRequest : ISerializable
    {
        public string KernelVersion;
        public string BoxVersion;
        public uint SlotVersion;
        public uint CodeIndex;
        public byte[] Code;
        public virtual int Size => KernelVersion.GetVarSize()+BoxVersion.GetVarSize() + sizeof(uint) + sizeof(uint) + Code.GetVarSize();
        private UInt256 _hash = null;
        public UInt256 Hash
        {
            get
            {
                if (_hash == null)
                {
                    _hash = new UInt256(Crypto.Default.Hash256(this.GetHashData()));
                }
                return _hash;
            }
        }
        public void Serialize(BinaryWriter writer)
        {
            writer.WriteVarString(KernelVersion);
            writer.WriteVarString(BoxVersion);
            writer.Write(SlotVersion);
            writer.Write(CodeIndex);
            writer.WriteVarBytes(Code);
        }
        public void Deserialize(BinaryReader reader)
        {
            KernelVersion = reader.ReadVarString();
            BoxVersion = reader.ReadVarString();
            SlotVersion = reader.ReadUInt32();
            CodeIndex = reader.ReadUInt32();
            Code = reader.ReadVarBytes();
        }

    }
}
