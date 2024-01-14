using Akka.Actor;
using Akka.Util.Internal;
using OX.Bapps;
using OX.Network.P2P.Payloads;
using OX.Notecase.Pages;
using OX.Wallets;
using System;
using System.IO;
using System.Windows.Forms;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace OX.Notecase
{

    public class WebStarter
    {
        static WebStarter _instance;
        public static WebStarter Instance
        {
            get
            {
                if (_instance == default)
                {
                    _instance = new WebStarter();
                }
                return _instance;
            }
        }
        public bool NeedWebService { get { return WebStartAction.IsNotNull(); } }
        public Action WebStartAction { get; set; }

        public void StartWeb()
        {
            if (WebStartAction.IsNotNull())
                WebStartAction();
        }

    }
}
