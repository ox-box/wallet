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
using Nethereum.Model;
using Nethereum.Util;
using Nethereum.Hex.HexConvertors.Extensions;
using OX.IO;
using NBitcoin.Secp256k1;
using Org.BouncyCastle.Asn1.Ocsp;
using Blazored.SessionStorage;
using System.Xml.Linq;

namespace OX.Wallets.Authentication
{

    public static class OXAuthenticationHelper
    {
        public static IApplicationBuilder UseOXAuthentication(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<OXAuthMiddlerware>();
            return builder;
        }
        public static bool VerifyEthAuthSignature(this EthAuthSignature EthAuthSignature)
        {
            var signer = new Nethereum.Signer.EthereumMessageSigner();
            try
            {
                var address = signer.EncodeUTF8AndEcRecover(EthAuthSignature.EthAuthInfo.ToArray().ToHexString(), EthAuthSignature.Signature);
                return address.ToLower() == EthAuthSignature.EthAddress.ToLower();
            }
            catch
            {
                return false;
            }
        }
        public static bool IsOXAuthenticated(this IHttpContextAccessor httpContextAccessor, out OXUser oxUser)
        {
            oxUser = default;
            var user = httpContextAccessor?.HttpContext?.User;
            if (user.IsNotNull() && user is OXUser ou)
            {
                oxUser = ou;
                return true;
            }
            return false;
        }
        public static bool IsLocalHost(this IHttpContextAccessor httpContextAccessor)
        {
            if (httpContextAccessor.IsNull() || httpContextAccessor.HttpContext.IsNull() || httpContextAccessor.HttpContext.Request.IsNull()) return false;
            var request = httpContextAccessor.HttpContext.Request;
            return request.Host.Host.ToLower() == "localhost" || request.Host.Host.ToLower() == "127.0.0.1";
        }
        public static async Task<string> GetEthAddress(this ISessionStorageService sessionStorage)
        {
            return await sessionStorage.GetItemAsync<string>("_ox_map_ethaddress");
        }
        public static async Task SaveEthAddress(this ISessionStorageService sessionStorage, string ethAddress)
        {
            await sessionStorage.SetItemAsync("_ox_map_ethaddress", ethAddress);
        }
    }
}
