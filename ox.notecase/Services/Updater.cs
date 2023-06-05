using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Linq;
using System.Reflection;
using System.IO;
using System.Net;
using System.Net.Cache;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Akka.Actor;
using OX.Ledger;
using OX.Network.P2P.Payloads;
using OX.IO;
using OX.IO.Data.LevelDB;
using OX.VM;
using OX.Cryptography;
using OX.IO.Caching;
using OX.Network;
using OX.Wallets;
using OX.SmartContract;
using OX.Wallets.NEP6;
using OX.Network.P2P;
using OX.Persistence.LevelDB;

namespace OX.Notecase
{
    public class MyWebClient : WebClient
    {
        /// <summary>
        ///     Response Uri after any redirects.
        /// </summary>
        public Uri ResponseUri;

        /// <inheritdoc />
        protected override WebResponse GetWebResponse(WebRequest request, IAsyncResult result)
        {
            WebResponse webResponse = base.GetWebResponse(request, result);
            ResponseUri = webResponse.ResponseUri;
            return webResponse;
        }
    }
    public class Updater
    {
        public static IWebProxy Proxy;
        public static NetworkCredential FtpCredentials;
        public MyWebClient GetWebClient(Uri uri)
        {
            MyWebClient webClient = new MyWebClient
            {
                CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore)
            };

            if (Proxy != null)
            {
                webClient.Proxy = Proxy;
            }

            if (uri.Scheme.Equals(Uri.UriSchemeFtp))
            {
                webClient.Credentials = FtpCredentials;
            }
            return webClient;
        }
    }
}
