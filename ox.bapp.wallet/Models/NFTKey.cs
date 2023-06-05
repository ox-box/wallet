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
using OX.Ledger;
using OX.Wallets.Base.NFT;

namespace OX.Wallets.Base
{
    public class NFTCoinKey : ISerializable
    {
        public uint Range;
        public uint Index;
        public ushort N;
        public virtual int Size => sizeof(uint) + sizeof(uint) + sizeof(ushort);
        public void Serialize(BinaryWriter writer)
        {
            writer.Write(Range);
            writer.Write(Index);
            writer.Write(N);
        }
        public void Deserialize(BinaryReader reader)
        {
            Range = reader.ReadUInt32();
            Index = reader.ReadUInt32();
            N = reader.ReadUInt16();
        }
    }
    public class MyNFTCoinKey : ISerializable
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
    }
    public class MyNFTTransferKey : ISerializable
    {
        public UInt160 Holder;
        public uint Index;
        public ushort N;
        public virtual int Size => Holder.Size + sizeof(uint) + sizeof(ushort);
        public void Serialize(BinaryWriter writer)
        {
            writer.Write(Holder);
            writer.Write(Index);
            writer.Write(N);
        }
        public void Deserialize(BinaryReader reader)
        {
            Holder = reader.ReadSerializable<UInt160>();
            Index = reader.ReadUInt32();
            N = reader.ReadUInt16();
        }
    }

    public class NFTSellKey : ISerializable
    {
        public NftID NftId;
        public uint Index;
        public ushort N;
        public virtual int Size => NftId.Size + sizeof(uint) + sizeof(ushort);
        public void Serialize(BinaryWriter writer)
        {
            writer.Write(NftId);
            writer.Write(Index);
            writer.Write(N);
        }
        public void Deserialize(BinaryReader reader)
        {
            NftId = reader.ReadSerializable<NftID>();
            Index = reader.ReadUInt32();
            N = reader.ReadUInt16();
        }
    }
    public class NFTSellValue : ISerializable
    {
        public Fixed8 Amount;
        public NFSHolder From;
        public NFSHolder To;
        public UInt256 TransferHash;
        public uint Time;
        public virtual int Size => Amount.Size + From.Size + To.Size + TransferHash.Size + sizeof(uint);
        public void Serialize(BinaryWriter writer)
        {
            writer.Write(Amount);
            writer.Write(From);
            writer.Write(To);
            writer.Write(TransferHash);
            writer.Write(Time);
        }
        public void Deserialize(BinaryReader reader)
        {
            Amount = reader.ReadSerializable<Fixed8>();
            From = reader.ReadSerializable<NFSHolder>();
            To = reader.ReadSerializable<NFSHolder>();
            TransferHash = reader.ReadSerializable<UInt256>();
            Time = reader.ReadUInt32();
        }
    }

    public class EthNftTransferKey : ISerializable
    {
        public StringWrapper EthAddress;
        public NFSStateKey NFSStateKey;

        public virtual int Size => EthAddress.Size + NFSStateKey.Size;
        public void Serialize(BinaryWriter writer)
        {
            writer.Write(EthAddress);
            writer.Write(NFSStateKey);
        }
        public void Deserialize(BinaryReader reader)
        {
            EthAddress = reader.ReadSerializable<StringWrapper>();
            NFSStateKey = reader.ReadSerializable<NFSStateKey>();
        }
    }
}
