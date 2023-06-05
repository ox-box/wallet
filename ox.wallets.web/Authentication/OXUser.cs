using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.AspNetCore.Builder;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using OX.IO;

namespace OX.Wallets.Authentication
{

    public class OXUser : ClaimsPrincipal
    {
        public bool IsSelf { get; private set; }
        public bool ValidEthSigner
        {
            get
            {
                if (EthAuthSignature.IsNull()) return false;
                return EthAuthSignature.VerifyEthAuthSignature() && EthAuthSignature.EthAuthInfo.TimeStamp > DateTime.Now.AddDays(-1).ToTimestamp();
            }
        }
        public EthAuthSignature EthAuthSignature { get; internal set; }
        public OXUser(bool isSelf = false)
        {
            this.IsSelf = isSelf;
        }
    }
    public class EthAuthInfo : ISerializable
    {
        public uint TimeStamp;
        public uint Nonce;
        public virtual int Size => sizeof(uint) + sizeof(uint);
        public void Serialize(BinaryWriter writer)
        {
            writer.Write(TimeStamp);
            writer.Write(Nonce);
        }
        public void Deserialize(BinaryReader reader)
        {
            TimeStamp = reader.ReadUInt32();
            Nonce = reader.ReadUInt32();
        }
    }
    public class EthAuthSignature : ISerializable
    {
        public string EthAddress;
        public EthAuthInfo EthAuthInfo;
        public string Signature;
        public virtual int Size => EthAddress.GetVarSize() + EthAuthInfo.Size + Signature.GetVarSize();
        public void Serialize(BinaryWriter writer)
        {
            writer.WriteVarString(EthAddress);
            writer.Write(EthAuthInfo);
            writer.WriteVarString(Signature);
        }
        public void Deserialize(BinaryReader reader)
        {
            EthAddress = reader.ReadVarString();
            EthAuthInfo = reader.ReadSerializable<EthAuthInfo>();
            Signature = reader.ReadVarString();
        }
    }
}
