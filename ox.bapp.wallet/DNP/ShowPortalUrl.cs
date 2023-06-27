using Akka.Actor;
using OX.IO.Actors;
using OX.Ledger;
using OX.Network.P2P;
using OX.Network.P2P.Payloads;
using OX.Persistence;
using OX.Wallets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;
using System.Text.RegularExpressions;
using OX.Wallets.UI.Forms;
using OX.Wallets.UI;
using OX.SmartContract;
using OX.IO;
using System.Xml;
using OX.Bapps;
using OX.Wallets.Base.DNP;
using System.Diagnostics;

namespace OX.Wallets.Base
{
    public partial class ShowPortalUrl : DarkDialog
    {
        List<string> Urls;
        public ShowPortalUrl(List<string> urls)
        {
            this.Urls = urls;
            InitializeComponent();
        }


        private void ClaimForm_Load(object sender, EventArgs e)
        {
            this.Text = UIHelper.LocalString("显示门户地址", "Show Portal Url");
            this.btnOk.Text = UIHelper.LocalString("确定", "OK");
            foreach(var url in this.Urls)
            {
                this.flowLayoutPanel1.Controls.Add(new UrlPort(url));
            }
        }

        private void ClaimForm_FormClosing(object sender, FormClosingEventArgs e)
        {

        }





    }
}
