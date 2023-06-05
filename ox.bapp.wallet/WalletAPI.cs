using OX.Bapps;
using OX.IO;
using OX.Network.P2P.Payloads;
using OX.Wallets;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace OX.Wallets.Base
{
    public partial class WalletApi : IBappApi
    {
        public Bapp Bapp { get; set; }
        public WalletApi(Bapp bapp)
        {
            this.Bapp = bapp;

        }
        public void OnBappEvent(BappEvent bappEvent)
        {

        }
        public void OnCrossBappMessage(CrossBappMessage message)
        {

        }
        public void OnBlock(Block block)
        {

        }
        public void BeforeOnBlock(Block block)
        {

        }
        public void AfterOnBlock(Block block)
        {

        }
        public void OnRebuild()
        {

        }
        public bool ProcessAsync(HttpContext context, string path, Dictionary<string, string> query, out string resp)
        {
            resp = "not found api";
           
            return false;
        }
        public bool GetHomeHtml(Microsoft.AspNetCore.Http.HttpContext context, string path, Dictionary<string, string> query, out string resp)
        {
            resp = "nothing";
            return true;
        }
    }
}
