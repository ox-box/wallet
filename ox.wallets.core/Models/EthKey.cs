using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using OX.IO;
using OX.Network.P2P;
using OX.Network.P2P.Payloads;
using OX.Cryptography.ECC;

namespace OX.Wallets
{
    public class EthOutputMerge : ISerializable
    {
        public string EthAddress;
        public uint LockExpirationIndex;
        public TransactionOutput Output;


        public virtual int Size => EthAddress.GetVarSize() + sizeof(uint) + Output.Size;
        public void Serialize(BinaryWriter writer)
        {
            writer.WriteVarString(EthAddress);
            writer.Write(LockExpirationIndex);
            writer.Write(Output);
        }
        public void Deserialize(BinaryReader reader)
        {
            EthAddress = reader.ReadVarString();
            LockExpirationIndex = reader.ReadUInt32();
            Output = reader.ReadSerializable<TransactionOutput>();
        }
    }
    public class EthereumMapTransactionMerge : ISerializable
    {
        public EthereumMapTransaction EthereumMapTransaction;
        public uint LastIndex;


        public virtual int Size => EthereumMapTransaction.Size + sizeof(uint);
        public void Serialize(BinaryWriter writer)
        {
            writer.Write(EthereumMapTransaction);
            writer.Write(LastIndex);
        }
        public void Deserialize(BinaryReader reader)
        {
            EthereumMapTransaction = reader.ReadSerializable<EthereumMapTransaction>();
            LastIndex = reader.ReadUInt32();
        }
    }
}
