using Akka.Actor;
using Akka.Util.Internal;
using OX.Bapps;
using OX.Network.P2P.Payloads;
using OX.Notecase.Pages;
using OX.Wallets;
using System;
using System.IO;
using System.Windows.Forms;

namespace OX.Notecase
{
    public class NotecaseApp : INotecase
    {
        public event Action<HeartBeatContext> OnHeartBeat;
        private IActorRef WalletsHandler;
        private WebBoxBuilderBase WebBoxBuilder;
        public OpenWallet Wallet { get; private set; }
        public SyncForm SyncForm;
        public ModuleContainer Container;
        DateTime PreDT = DateTime.MinValue;
        private DateTime LastDT = DateTime.Now.AddDays(1);
        private uint PreBlockTime = 0;
        private string path;
        public WalletIndexer Indexer { get; private set; }
        private System.Windows.Forms.Timer timer;
        static NotecaseApp _instance;
        public static NotecaseApp Instance
        {
            get
            {
                if (_instance == default)
                {
                    _instance = new NotecaseApp();
                }
                return _instance;
            }
        }
        public bool IsNormalSync
        {
            get
            {
                //Solution 1
                //var span = (LastDT - PreDT).TotalSeconds;
                //if (span > ProtocolSettings.Default.SecondsPerBlock * 3 || span < ProtocolSettings.Default.SecondsPerBlock)
                //    return false;
                //if (LastDT.AddSeconds(ProtocolSettings.Default.SecondsPerBlock * 3) > DateTime.Now)
                //    return true;
                ////return DateTime.Now > LastDT.AddSeconds(Blockchain.SecondsPerBlock - 1);
                //else return false;

                //Solution 2
                var stamp = ProtocolSettings.Default.SecondsPerBlock * 2 + 10;
                return PreBlockTime + stamp > DateTime.Now.ToTimestamp();
            }
        }
        NotecaseApp()
        {
            WalletsHandler = WalletsBlockHandler.Instance;
            WebBoxBuilder = new WebBoxBuilderBase();
            WalletsBlockHandler.SyncBlocksCompleted += SingleTon_SyncBlocksCompleted;
            this.timer = new Timer();
            this.timer.Enabled = true;
            this.timer.Interval = 500;
            this.timer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            HeartBeatContext context = new HeartBeatContext() { IsNormalSync = IsNormalSync };
            if (this.SyncForm != default)
                this.SyncForm.HeartBeat(context);
            if (this.Container != default)
                this.Container.HeartBeat(context);
            if (this.OnHeartBeat != default)
                this.OnHeartBeat(context);
        }

        private void SingleTon_SyncBlocksCompleted(Network.P2P.Payloads.Block block)
        {
            PreBlockTime = block.Timestamp;
            //if (DateTime.Now > LastDT.AddSeconds(Blockchain.SecondsPerBlock - 1))
            //{
            //    SyncForm.DoInvoke(() =>
            //    {
            //        SyncForm.Open();
            //    });
            //}
            if (IsNormalSync)
            {
                SyncForm.DoInvoke(() =>
                {
                    SyncForm.Open();
                });
            }
            PreDT = LastDT;
            LastDT = DateTime.Now;
        }
        public WalletIndexer GetIndexer(string walletpath)
        {
            if (Indexer is null)
            {
                path = walletpath;
                var p = path.Substring(0, path.LastIndexOf('.'));
                if (!Directory.Exists(p))
                    Directory.CreateDirectory(p);
                BaseBappProvider.WalletIndexDirectory = p;
                Indexer = new WalletIndexer(p + "\\" + Settings.Default.Paths.Index, false);
            }
            return Indexer;
        }

        public void ChangeWallet(OpenWallet wallet)
        {
            this.Wallet = wallet;
            this.WalletsHandler.Tell(new BlockHandler.WalletCommand() { Wallet = wallet });
            WalletIndexer indexer = GetIndexer(path);
            var providers = Bapps.Bapp.AllBappProviders();
            foreach (var provider in providers)
            {
                provider.Wallet = wallet;
            }
            if (this.SyncForm != default)
                this.SyncForm.ChangeWallet(this);
            if (this.Container != default)
                this.Container.ChangeWallet(this);
            WebBox.SetNotecase(this);
        }
        public void Relay(IInventory inventory)
        {
            if (this.WalletsHandler != default)
                this.WalletsHandler.Tell(inventory);
        }
        public void Close()
        {
            SyncForm.DoInvoke(() =>
            {
                Container.Hide();
                SyncForm.Show();
                SyncForm.FocusPwd();
            });
        }
        public void Exit()
        {
            Application.Exit();
        }
    }
}
