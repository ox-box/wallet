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
    public partial class SetPortalHome : DarkForm
    {
        INotecase Operater;
        public DNPModule Module { get; set; }
        public SetPortalHome(DNPModule module, INotecase operater)
        {
            InitializeComponent();
            this.Module = module;
            this.Operater = operater;
        }


        private void ClaimForm_Load(object sender, EventArgs e)
        {
            this.Text = UIHelper.LocalString("节点门户设置", "Node Portal Setting");
            this.lb_name.Text = UIHelper.LocalString("节点门户名称:", "Node Portal Name:");
            this.lb_baseUrl.Text = UIHelper.LocalString("外网IP或域名:", "IP or Domain Name:");
            this.lb_remark.Text = UIHelper.LocalString("节点门户简介:", "Node Portal Introduce:");
            this.bt_ok.Text = UIHelper.LocalString("确定", "OK");
            this.bt_cancel.Text = UIHelper.LocalString("取消", "Cancel");
            DNPHelper.SetDNP(Module.dnp);
            this.tb_name.Text = DNPHelper.GetDNPSetting()?.DNP_Name;
            this.tb_remark.Text = DNPHelper.GetDNPSetting()?.DNP_Introduce;
            this.tb_baseUrl.Text = DNPHelper.GetDNPSetting()?.Base_Url;
        }

        private void ClaimForm_FormClosing(object sender, FormClosingEventArgs e)
        {

        }




        private void bt_ok_Click(object sender, EventArgs e)
        {
            DNPHelper.SetDNP(Module.dnp);
            var setting = DNPHelper.GetDNPSetting();
            if (setting.IsNotNull())
            {
                setting.DNP_Name = this.tb_name.Text;
                setting.DNP_Introduce = this.tb_remark.Text;
                setting.Base_Url = this.tb_baseUrl.Text;
                Module.dnp = setting.Build();
                DNPHelper.SetDNP(Module.dnp);
                Module.SaveSetting();
            }
            this.Close();
        }

        private void bt_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
