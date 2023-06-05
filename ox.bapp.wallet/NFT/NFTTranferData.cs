using OX.Cryptography.ECC;
using OX.IO;
using System.IO;
using OX.Ledger;
using OX.Network.P2P.Payloads;
using System;
using OX.Network.P2P;
//using System.Runtime.InteropServices.WindowsRuntime;

namespace OX.Wallets.Base.NFT
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
    }
    public class NFTTranferData : ISerializable
    {
        public NFSStateKey Key;
        public MixSignatureValidator<NftTransferAuthentication> Validator;

        public virtual int Size => Key.Size + Validator.Size;

        public void Serialize(BinaryWriter writer)
        {
            writer.Write(Key);
            writer.Write(Validator);
        }
        public void Deserialize(BinaryReader reader)
        {
            Key = reader.ReadSerializable<NFSStateKey>();
            Validator = reader.ReadSerializable<MixSignatureValidator<NftTransferAuthentication>>();
        }
    }

}
