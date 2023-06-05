using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OX.Wallets.UI;
using OX.Network.P2P.Payloads;
using OX.Bapps;
using OX.Wallets.NEP6;
using System.ComponentModel.Design.Serialization;
using System.Security.Claims;
using OX.Wallets.UI.Forms;
using System.Security.Cryptography;
using OX.IO;
using OX.IO.Json;

namespace OX.Wallets.Base.DNP
{
    public class DNPModule : Module
    {
        public override string ModuleName { get { return "walletdnpmodule"; } }
        public override uint Index { get { return int.MaxValue - 9; } }

        protected INotecase Operater;
        public JObject dnp;
        public DNPModule(Bapp bapp) : base(bapp)
        {

        }
        public override void InitEvents() { }
        public override void InitWindows()
        {
            ToolStripMenuItem walletMenu = new ToolStripMenuItem();
            walletMenu.BackColor = System.Drawing.Color.FromArgb(60, 63, 65);

            walletMenu.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            walletMenu.Name = "walletserverMenu";
            walletMenu.Size = new System.Drawing.Size(39, 21);
            walletMenu.Text = UIHelper.LocalString("&节点门户", "&Node Portal");

            ToolStripMenuItem dnpSettingMenu = new ToolStripMenuItem();
            dnpSettingMenu.BackColor = System.Drawing.Color.FromArgb(60, 63, 65);
            dnpSettingMenu.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            //isingMenu.Image = global::Example.Icons.NewFile_6276;
            dnpSettingMenu.Name = "newLetterMenu";
            dnpSettingMenu.ShortcutKeys = Keys.Control | Keys.N;
            dnpSettingMenu.Size = new System.Drawing.Size(170, 22);
            dnpSettingMenu.Text = UIHelper.LocalString("&节点设置", "&Node Setting");
            dnpSettingMenu.Click += DnpSettingMenu_Click;

            ToolStripMenuItem goPortalMenu = new ToolStripMenuItem();
            goPortalMenu.BackColor = System.Drawing.Color.FromArgb(60, 63, 65);
            goPortalMenu.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            //isingMenu.Image = global::Example.Icons.NewFile_6276;
            goPortalMenu.Name = "goPortalMenu";
            goPortalMenu.ShortcutKeys = Keys.Control | Keys.N;
            goPortalMenu.Size = new System.Drawing.Size(170, 22);
            goPortalMenu.Text = UIHelper.LocalString("&打开门户", "&Open Portal");
            goPortalMenu.Click += GoPortalMenu_Click;

            walletMenu.DropDownItems.AddRange(new ToolStripItem[] {
                dnpSettingMenu,
                goPortalMenu
                });
            Container.TopMenus.Items.AddRange(new ToolStripItem[] {
            walletMenu});
        }

        private void GoPortalMenu_Click(object sender, EventArgs e)
        {
            OXRunTime.GoWeb("/");
        }

        private void DnpSettingMenu_Click(object sender, EventArgs e)
        {
            new SetPortalHome(this, this.Operater).ShowDialog();
        }

        public override void OnBappEvent(BappEvent be)
        {

        }

        public override void OnCrossBappMessage(CrossBappMessage message)
        {

        }
        public override void HeartBeat(HeartBeatContext context)
        {

        }
        public override void BeforeOnBlock(Block block)
        {

        }
        public override void OnBlock(Block block)
        {

        }
        public override void AfterOnBlock(Block block)
        {

        }
        public override void ChangeWallet(INotecase operater)
        {
            Operater = operater;

        }
        public override void OnRebuild()
        {

        }
        public override void OnLoadBappModuleWalletSection(JObject bappSectionObject)
        {
            dnp = bappSectionObject["dnp"];
            if (dnp.IsNotNull())
            {
                DNPHelper.SetDNP(dnp);
            }
        }
        public void SaveSetting()
        {
            if (this.Operater.Wallet is OpenWallet openWallet)
            {
                DNPHelper.SetDNP(dnp);
                this.moduleWalletSection["dnp"] = dnp;
                openWallet.Save();
            }
        }


    }
}
