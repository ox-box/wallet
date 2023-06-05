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

namespace OX.Wallets.Base.Trust
{
    public class AssetTrustModule : Module
    {
        public override string ModuleName { get { return "assettrustmodule"; } }
        public override uint Index { get { return 1; } }

        protected INotecase Operater;
        protected MyTrusteeContracts MyTrusteeContracts;
        protected MyTrusterContracts MyTrusterContracts;
        public AssetTrustModule(Bapp bapp) : base(bapp)
        {

        }
        public override void InitEvents() { }
        public override void InitWindows()
        {
            ToolStripMenuItem assetTrustMenu = new ToolStripMenuItem();
            assetTrustMenu.BackColor = System.Drawing.Color.FromArgb(60, 63, 65);

            assetTrustMenu.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            assetTrustMenu.Name = "assetTrustMenu";
            assetTrustMenu.Size = new System.Drawing.Size(39, 21);
            assetTrustMenu.Text = UIHelper.LocalString("&资产信托", "&Asset Trust");
            //signature
            ToolStripMenuItem newTrustAccountMenu = new ToolStripMenuItem();
            newTrustAccountMenu.BackColor = System.Drawing.Color.FromArgb(60, 63, 65);
            newTrustAccountMenu.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            //isingMenu.Image = global::Example.Icons.NewFile_6276;
            newTrustAccountMenu.Name = "newTrustAccountMenu";
            newTrustAccountMenu.ShortcutKeys = Keys.Control | Keys.N;
            newTrustAccountMenu.Size = new System.Drawing.Size(170, 22);
            newTrustAccountMenu.Text = UIHelper.LocalString("&创建信托合约", "&New Trust Contract");
            newTrustAccountMenu.Click += NewTrustAccountMenu_Click;

            //turster
            ToolStripMenuItem tursterMenu = new ToolStripMenuItem();
            tursterMenu.BackColor = System.Drawing.Color.FromArgb(60, 63, 65);
            tursterMenu.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            //exitmenu.Image = global::Example.Icons.NewFile_6276;
            tursterMenu.Name = "tursterMenu";
            tursterMenu.ShortcutKeys = Keys.Control | Keys.R;
            tursterMenu.Size = new System.Drawing.Size(170, 22);
            tursterMenu.Text = UIHelper.LocalString("&我委托的合约", "&My Truster Contracts");
            tursterMenu.Click += TursterMenu_Click;
            //turstee
            ToolStripMenuItem tursteeMenu = new ToolStripMenuItem();
            tursteeMenu.BackColor = System.Drawing.Color.FromArgb(60, 63, 65);
            tursteeMenu.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            //exitmenu.Image = global::Example.Icons.NewFile_6276;
            tursteeMenu.Name = "tursteeMenu";
            tursteeMenu.ShortcutKeys = Keys.Control | Keys.E;
            tursteeMenu.Size = new System.Drawing.Size(170, 22);
            tursteeMenu.Text = UIHelper.LocalString("&我受托的合约", "&My Trustee Contracts");
            tursteeMenu.Click += TursteeMenu_Click;

            assetTrustMenu.DropDownItems.AddRange(new ToolStripItem[] {
                newTrustAccountMenu,
                tursterMenu,
                tursteeMenu
                });
            Container.TopMenus.Items.AddRange(new ToolStripItem[] {
            assetTrustMenu});
        }

        private void TursteeMenu_Click(object sender, EventArgs e)
        {
            if (MyTrusteeContracts == default)
            {
                MyTrusteeContracts = new MyTrusteeContracts();
                MyTrusteeContracts.Module = this;
                if (Operater != default && Operater.Wallet != default)
                    MyTrusteeContracts.ChangeWallet(Operater);
                Container.ToolWindows.Add(MyTrusteeContracts);
            }
            Container.DockPanel.AddContent(MyTrusteeContracts);
        }

        private void TursterMenu_Click(object sender, EventArgs e)
        {
            if (MyTrusterContracts == default)
            {
                MyTrusterContracts = new MyTrusterContracts();
                MyTrusterContracts.Module = this;
                if (Operater != default && Operater.Wallet != default)
                    MyTrusterContracts.ChangeWallet(Operater);
                Container.ToolWindows.Add(MyTrusterContracts);
            }
            Container.DockPanel.AddContent(MyTrusterContracts);
        }



        private void NewTrustAccountMenu_Click(object sender, EventArgs e)
        {
            new NewAssetTrustContract(Operater).ShowDialog();
        }

        public override void OnBappEvent(BappEvent be)
        {
            if (MyTrusteeContracts.IsNotNull())
            {
                MyTrusteeContracts.OnBappEvent(be);
            }
            if (MyTrusterContracts.IsNotNull())
            {
                MyTrusterContracts.OnBappEvent(be);
            }
        }

        public override void OnCrossBappMessage(CrossBappMessage message)
        {
            if (MyTrusteeContracts.IsNotNull())
            {
                MyTrusteeContracts.OnCrossBappMessage(message);
            }
            if (MyTrusterContracts.IsNotNull())
            {
                MyTrusterContracts.OnCrossBappMessage(message);
            }
        }
        public override void HeartBeat(HeartBeatContext context)
        {
            if (MyTrusteeContracts.IsNotNull())
            {
                MyTrusteeContracts.HeartBeat(context);
            }
            if (MyTrusterContracts.IsNotNull())
            {
                MyTrusterContracts.HeartBeat(context);
            }
        }
        public override void BeforeOnBlock(Block block)
        {
            if (MyTrusteeContracts.IsNotNull())
            {
                MyTrusteeContracts.BeforeOnBlock(block);
            }
            if (MyTrusterContracts.IsNotNull())
            {
                MyTrusterContracts.BeforeOnBlock(block);
            }
        }
        public override void OnBlock(Block block)
        {
            if (MyTrusteeContracts.IsNotNull())
            {
                MyTrusteeContracts.OnBlock(block);
            }
            if (MyTrusterContracts.IsNotNull())
            {
                MyTrusterContracts.OnBlock(block);
            }
        }
        public override void AfterOnBlock(Block block)
        {
            if (MyTrusteeContracts.IsNotNull())
            {
                MyTrusteeContracts.AfterOnBlock(block);
            }
            if (MyTrusterContracts.IsNotNull())
            {
                MyTrusterContracts.AfterOnBlock(block);
            }
        }
        public override void ChangeWallet(INotecase operater)
        {
            Operater = operater;
            if (MyTrusteeContracts.IsNotNull())
            {
                MyTrusteeContracts.ChangeWallet(operater);
            }
            if (MyTrusterContracts.IsNotNull())
            {
                MyTrusterContracts.ChangeWallet(operater);
            }
        }
        public override void OnRebuild()
        {
            if (MyTrusteeContracts.IsNotNull())
            {
                MyTrusteeContracts.OnRebuild();
            }
            if (MyTrusterContracts.IsNotNull())
            {
                MyTrusterContracts.OnRebuild();
            }
        }
        public override void OnLoadBappModuleWalletSection(JObject bappSectionObject)
        {
        }



    }
}
