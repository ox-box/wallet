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
    public class SecretLetterKey : ISerializable
    {
        public UInt160 Recipient;
        public uint LetterIndex;
        public UInt256 TxId;
        public ECPoint From;
        public byte[] Msg;
        public virtual int Size => Recipient.Size + sizeof(uint) + TxId.Size + From.Size + Msg.GetVarSize();
        public void Serialize(BinaryWriter writer)
        {
            writer.Write(Recipient);
            writer.Write(LetterIndex);
            writer.Write(TxId);
            writer.Write(From);
            writer.WriteVarBytes(Msg);
        }
        public void Deserialize(BinaryReader reader)
        {
            Recipient = reader.ReadSerializable<UInt160>();
            LetterIndex = reader.ReadUInt32();
            TxId = reader.ReadSerializable<UInt256>();
            From = reader.ReadSerializable<ECPoint>();
            Msg = reader.ReadVarBytes();
        }
    }
}
