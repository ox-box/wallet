using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using OX.IO;
using System.Net;
using Org.BouncyCastle.Asn1.Ocsp;

namespace OX.Wallets.Authentication
{
    public sealed class OXAuthMiddlerware
    {
        public const string COOKIENAME = "_OX_BOX_AUTH";

        private readonly RequestDelegate _next;

        public OXAuthMiddlerware(RequestDelegate next)
        {
            _next = next;

        }
        public async Task InvokeAsync(HttpContext context)
        {
            var request = context.Request;          
            if (request.Host.Host.ToLower() == "localhost" || request.Host.Host.ToLower() == "127.0.0.1")
            {
                context.User = new OXUser(true);
            }
            else
            {
                if (request.Cookies.TryGetValue(COOKIENAME, out string str))
                {
                    try
                    {
                        var bs = str.HexToBytes();
                        var EthAuthSignature = bs.AsSerializable<EthAuthSignature>();
                        if (EthAuthSignature.VerifyEthAuthSignature() && EthAuthSignature.EthAuthInfo.TimeStamp > DateTime.Now.AddDays(-1).ToTimestamp())
                        {
                            context.User = new OXUser()
                            {
                                EthAuthSignature = EthAuthSignature
                            };
                        }
                        else
                        {
                            context.Response.Cookies.Delete(COOKIENAME);
                        }
                    }
                    catch
                    {

                    }
                }
            }
            await _next.Invoke(context);
        }

    }
}
