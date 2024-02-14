using OX.IO;
using OX.Network.P2P.Payloads;
using System.IO;

namespace OX.Wallets
{
    public static class WalletBizPersistencePrefixes
    {
        public const byte Event_Board = 0x01;
        public const byte Event_Engrave = 0x02;
        public const byte Event_Engrave_Holder = 0x03;
        public const byte Event_Digg = 0x04;
        public const byte Event_Digg_Holder = 0x05;
        public const byte Event_Engrave_Page_State = 0x06;
        public const byte Event_Engrave_Page_Index = 0x07;
        public const byte Event_Board_Holder = 0x08;
        public const byte Event_Digg_Page_State = 0x09;
        public const byte Event_Digg_Page_Index = 0x0A;
        public const byte NFT_Coin_My = 0x0B;
        public const byte NFT_Transfer_My = 0x0C;
        public const byte NFT_Transfer_Hash_My = 0x0D;
        public const byte TX_MyLockAsset = 0x0E;
        public const byte TX_Once_MyLockOXS = 0x0F;
        public const byte NFT_Coin = 0x10;
        public const byte NFT_Transfer = 0x11;
        public const byte Wallet_Setting = 0x12;
        public const byte NFT_Transfer_Record = 0x13;
        public const byte Book_Record = 0x14;
        public const byte Book_Record_My = 0x15;
        public const byte IssueTransaction_History = 0x16;
        public const byte SecrectLetter_Inbox = 0x17;
        public const byte AssetTrust_Contract = 0x18;
        public const byte AssetTrust_UTXO = 0x19;
        public const byte TX_LockAsset_Record = 0x1A;
        public const byte OXC_ALL_Issued = 0x1B;
        public const byte MY_Eth_Map_UTXO = 0x1C;
        public const byte ALL_Eth_Map = 0x1D;
        public const byte ALL_Eth_Map_UTXO = 0x1E;
        public const byte NFT_Issue_Record = 0x1F;
        public const byte NFT_Transfer_Record_Server = 0x20;
        public const byte TX_LockAssetMeta = 0x21;
    }
    public enum WalletSettingKind
    {
        NFTCoin_Counter = 1
    }
    public class WalletSettingKey : ISerializable
    {
        public WalletSettingKind Key;
        public virtual int Size => sizeof(byte);
        public void Serialize(BinaryWriter writer)
        {
            writer.Write((byte)Key);
        }
        public void Deserialize(BinaryReader reader)
        {
            Key = (WalletSettingKind)reader.ReadByte();
        }
        public override bool Equals(object obj)
        {
            if (obj is WalletSettingKey key)
            {
                return key.Key == this.Key;
            }
            return base.Equals(obj);
        }
        public override int GetHashCode()
        {
            return this.Key.GetHashCode();
        }
        public override string ToString()
        {
            return this.Key.ToString();
        }
    }
    public class WalletSettingValue : ISerializable
    {
        public byte[] Data;
        public virtual int Size => Data.GetVarSize();
        public void Serialize(BinaryWriter writer)
        {
            writer.WriteVarBytes(Data);
        }
        public void Deserialize(BinaryReader reader)
        {
            Data = reader.ReadVarBytes();
        }
    }
}
