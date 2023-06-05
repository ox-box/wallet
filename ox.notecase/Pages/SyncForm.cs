using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using OX;
using OX.Ledger;
using OX.Network.P2P;
using OX.Wallets.NEP6;
using OX.Network.P2P.Payloads;
using OX.Wallets;
using OX.Wallets.UI.Forms;
using OX.Wallets.Mnemonics;
using OX.Bapps;
using Nethereum.Hex.HexConvertors.Extensions;

namespace OX.Notecase.Pages
{
    public partial class SyncForm : DarkForm, INotecaseTrigger
    {
        int k = 1;
        public SyncForm()
        {
            InitializeComponent();
            this.AcceptButton = btOpenWallet;
            Init();
        }

        public void HeartBeat(HeartBeatContext context)
        {
            this.DoInvoke(() =>
            {
                if (k++ >= 8)
                    k = 1;
                StringBuilder sb = new StringBuilder();
                for (int i = 1; i <= 8; i++)
                {
                    sb.Append(i == k ? ">>" : " ");
                }
                sb.Append(LocalNode.Singleton.GetRemoteNodes().Count() + " " + UIHelper.LocalString("节点", "Nodes"));
                lbHeight.Text = $"{UIHelper.LocalString("区块链高度", "Block Chain Height")}    {Blockchain.Singleton.Height}  /  {Blockchain.Singleton.HeaderHeight}    {UIHelper.LocalString("正在同步", "Synchronizing")}{sb.ToString()}";
            });


        }
        public void OnBappEvent(BappEvent be) { }

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
        public void ChangeWallet(INotecase operater) { }
        public void OnRebuild()
        {
        }
        private void Init()
        {
            Bapp.CrossBappMessage += OnCrossBappMessage;
        }
        public void Open()
        {
            this.btSelectWallet.Visible = true;
            this.btOpenWallet.Visible = true;
            this.tbPwd.Visible = true;
            this.tbWalletPath.Visible = true;
            this.lb1.Visible = true;
            this.lb2.Visible = true;
            this.tbPwd.Focus();
        }
        public void FocusPwd()
        {
            this.tbPwd.Focus();
        }
        private void Form_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Close();
        }

        private void SyncForm_Load(object sender, EventArgs e)
        {
            this.tbPwd.PasswordChar = '*';
            this.lb1.Text = UIHelper.LocalString("钱包路径:", "Wallet Path:");
            this.lb2.Text = UIHelper.LocalString("密码:", "Password:");
            this.btSelectWallet.Text = UIHelper.LocalString("选择", "Browse");
            this.btOpenWallet.Text = UIHelper.LocalString("打开钱包", "Open");
            if (File.Exists(Settings.Default.LastWalletPath))
            {
                this.tbWalletPath.Text = Settings.Default.LastWalletPath;
                this.ActiveControl = this.tbPwd;
            }
        }

        private void btSelectWallet_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.tbWalletPath.Text = openFileDialog1.FileName;
            }
        }

        private void btOpenWallet_Click(object sender, EventArgs e)
        {
            this.btOpenWallet.Text = UIHelper.LocalString("打开中...", "Opening");
            this.btOpenWallet.Enabled = false;
            string path = this.tbWalletPath.Text;
            OX.Wallets.Wallet wallet = default;


            OpenWallet OpenWallet = new OpenWallet(NotecaseApp.Instance.GetIndexer(path), path);
            try
            {
                OpenWallet.Unlock(this.tbPwd.Text);
                OpenWallet.DecryptMnemonics();
                wallet = OpenWallet;
            }
            catch (CryptographicException)
            {
                this.FocusPwd();
                DarkMessageBox.ShowError(UIHelper.LocalString("密码错误", "password incorrect"), "");
                this.btOpenWallet.Text = UIHelper.LocalString("打开钱包", "Open");
                this.btOpenWallet.Enabled = true;
                return;
            }


            if (NotecaseApp.Instance.Container == default)
            {
                var mc = new ModuleContainer();
                var ip = Dns.GetHostEntry(Dns.GetHostName()).AddressList.FirstOrDefault(p => p.AddressFamily.ToString() == "InterNetwork")?.ToString();
                var apiurl = $"http://{ip}:{Settings.Default.P2P.ApiPort}";
                //mc.Text = apiurl;
                mc.WebApiUrl = apiurl;
                NotecaseApp.Instance.Container = mc;
            }

            NotecaseApp.Instance.ChangeWallet(wallet);
            Settings.Default.LastWalletPath = path;
            Settings.Default.Save();
            NotecaseApp.Instance.Indexer.StartIndex();
            this.Hide();
            this.tbPwd.Text = "";
            NotecaseApp.Instance.Container.Show();
            this.btOpenWallet.Text = UIHelper.LocalString("打开钱包", "Open");
            this.btOpenWallet.Enabled = true;
            OXRunTime.RunState = RunStatus.WalletOpened;
        }


        private void SyncForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {

                OX.Wallets.UI.Controls.DarkContextMenu menu = new OX.Wallets.UI.Controls.DarkContextMenu();
                ToolStripMenuItem item = new ToolStripMenuItem(UIHelper.LocalString("创建新钱包", "New Wallet"));
                item.Click += CreateWallet_Click;
                menu.Items.Add(item);
                item = new ToolStripMenuItem(UIHelper.LocalString("助记词重建钱包", "Rebuild wallet with mnemonics"));
                item.Click += RebuildWallet_Click;
                menu.Items.Add(item);
                item = new ToolStripMenuItem(UIHelper.LocalString("私钥重建钱包", "Rebuild wallet with private key"));
                item.Click += RebuildWalletByPrivateKey_Click;
                menu.Items.Add(item);
                item = new ToolStripMenuItem(UIHelper.LocalString("种子节点", "Seed Node"));
                item.Click += SeedNode_Click;
                menu.Items.Add(item);
                item = new ToolStripMenuItem(UIHelper.LocalString("退出", "Exit"));
                item.Click += Exit_Click;
                menu.Items.Add(item);
                menu.Show(sender as Control, e.Location);
            }

        }



        private void RebuildWalletByPrivateKey_Click(object sender, System.EventArgs e)
        {
            using (ImportPrivateKey importKey = new ImportPrivateKey())
            {
                if (importKey.ShowDialog() != DialogResult.OK) return;
                var key = importKey.GetKey();
                if (key.IsNotNullAndEmpty())
                    using (CreateWallet dialog = new CreateWallet())
                    {
                        if (dialog.ShowDialog() != DialogResult.OK) return;
                        var path = dialog.WalletPath;
                        var pwd = dialog.Password;
                        OpenWallet OpenWallet = new OpenWallet(NotecaseApp.Instance.GetIndexer(path), path);
                        OpenWallet.Unlock(pwd);

                        try
                        {
                            OpenWallet.Import(key);
                            //wallet.CreateAccount();
                            OpenWallet.Save();
                            NotecaseApp.Instance.ChangeWallet(OpenWallet);
                            Settings.Default.LastWalletPath = path;
                            Settings.Default.Save();
                            this.tbWalletPath.Text = Settings.Default.LastWalletPath;
                            this.ActiveControl = this.tbPwd;
                        }
                        catch
                        {
                            DarkMessageBox.ShowError(UIHelper.LocalString("私钥导入错误！", "Private key import failed!"), "");
                        }
                    }
            }

        }
        private void RebuildWallet_Click(object sender, System.EventArgs e)
        {
            using (RebuildWallet mnemonicsWallet = new RebuildWallet())
            {
                if (mnemonicsWallet.ShowDialog() != DialogResult.OK) return;
                var Mnemonics = mnemonicsWallet.Words;
                if (Mnemonics.IsNotNullAndEmpty() && mnemonicsWallet.Verify())
                    using (CreateWallet dialog = new CreateWallet())
                    {
                        if (dialog.ShowDialog() != DialogResult.OK) return;
                        var path = dialog.WalletPath;
                        var pwd = dialog.Password;
                        var AccountNumber = dialog.AccountNumber;
                        OpenWallet wallet = new OpenWallet(NotecaseApp.Instance.GetIndexer(path), path);
                        wallet.Unlock(pwd);
                        for (int i = 0; i < AccountNumber; i++)
                        {
                            var seed = Mnemonic.MnemonicToSeed(Mnemonics, pwd + "ox" + i.ToString());
                            var privateKey = Mnemonic.SeedToWPF(seed, 888);
                            wallet.Import(privateKey);
                        }
                        for (int i = 0; i < 7; i++)
                        {
                            var seed = Mnemonic.MnemonicToSeed(Mnemonics, pwd + "ox" + i.ToString());
                            var privateKey = Mnemonic.SeedToPrivateKey(seed, 0);
                            var btckey = new NBitcoin.Key(privateKey);
                            var address = btckey.PubKey.GetAddress(NBitcoin.ScriptPubKeyType.Legacy, NBitcoin.Network.Main);
                            var publickkey = btckey.PubKey.ToHex();
                            wallet.CreateOpenAccount(privateKey, address.ToString(), publickkey, 0);
                        }
                        for (int i = 0; i < 7; i++)
                        {
                            var seed = Mnemonic.MnemonicToSeed(Mnemonics, pwd + "ox" + i.ToString());
                            var privateKey = Mnemonic.SeedToPrivateKey(seed, 60);
                            var ecKey = new Nethereum.Signer.EthECKey(privateKey, true);
                            //var ecKey =new  Nethereum.Signer.EthECKey.GenerateKey();
                            //var privateKey = ecKey.GetPrivateKey();
                            var publickey = ecKey.GetPubKey().ToHex(false);
                            var address = ecKey.GetPublicAddress();
                            wallet.CreateOpenAccount(privateKey, address, publickey, 60);
                        }
                        //wallet.CreateAccount();
                        wallet.EncryptMnemonics(Mnemonics);
                        wallet.Save();
                        NotecaseApp.Instance.ChangeWallet(wallet);
                        Settings.Default.LastWalletPath = path;
                        Settings.Default.Save();
                        this.tbWalletPath.Text = Settings.Default.LastWalletPath;
                        this.ActiveControl = this.tbPwd;
                    }
            }

        }
        private void CreateWallet_Click(object sender, System.EventArgs e)
        {
            using (MnemonicsWallet nmenonicsWallet = new MnemonicsWallet())
            {
                if (nmenonicsWallet.ShowDialog() != DialogResult.OK) return;
                var Mnemonics = nmenonicsWallet.Words;
                using (CreateWallet dialog = new CreateWallet())
                {
                    if (dialog.ShowDialog() != DialogResult.OK) return;
                    var path = dialog.WalletPath;
                    var pwd = dialog.Password;
                    var AccountNumber = dialog.AccountNumber;
                    OpenWallet wallet = new OpenWallet(NotecaseApp.Instance.GetIndexer(path), path);
                    wallet.Unlock(pwd);
                    for (int i = 0; i < AccountNumber; i++)
                    {
                        var seed = Mnemonic.MnemonicToSeed(Mnemonics, pwd + "ox" + i.ToString());
                        var privateKey = Mnemonic.SeedToWPF(seed, 888);
                        wallet.Import(privateKey);
                    }
                    for (int i = 0; i < 7; i++)
                    {
                        var seed = Mnemonic.MnemonicToSeed(Mnemonics, pwd + "ox" + i.ToString());
                        var privateKey = Mnemonic.SeedToPrivateKey(seed, 0);
                        var btckey = new NBitcoin.Key(privateKey);
                        var address = btckey.PubKey.GetAddress(NBitcoin.ScriptPubKeyType.Legacy, NBitcoin.Network.Main);
                        var publickkey = btckey.PubKey.ToHex();
                        wallet.CreateOpenAccount(privateKey, address.ToString(), publickkey, 0);
                    }
                    for (int i = 0; i < 7; i++)
                    {
                        var seed = Mnemonic.MnemonicToSeed(Mnemonics, pwd + "ox" + i.ToString());
                        var privateKey = Mnemonic.SeedToPrivateKey(seed, 60);
                        var ecKey = new Nethereum.Signer.EthECKey(privateKey, true);
                        //var ecKey =new  Nethereum.Signer.EthECKey.GenerateKey();
                        //var privateKey = ecKey.GetPrivateKey();
                        var publickey = ecKey.GetPubKey().ToHex(false);
                        var address = ecKey.GetPublicAddress();
                        wallet.CreateOpenAccount(privateKey, address, publickey, 60);
                    }
                    //wallet.CreateAccount();
                    wallet.EncryptMnemonics(Mnemonics);
                    wallet.Save();
                    NotecaseApp.Instance.ChangeWallet(wallet);
                    Settings.Default.LastWalletPath = path;
                    Settings.Default.Save();
                    this.tbWalletPath.Text = Settings.Default.LastWalletPath;
                    this.ActiveControl = this.tbPwd;
                }
            }

        }
        private void SeedNode_Click(object sender, System.EventArgs e)
        {
            new SeedManage().ShowDialog();
        }
        private void Exit_Click(object sender, System.EventArgs e)
        {
            Application.Exit();
        }


        //public void Start()
        //{
        //    this.timer1.Start();
        //}
        //public void Stop()
        //{
        //    this.timer1.Stop();
        //}
    }
}
