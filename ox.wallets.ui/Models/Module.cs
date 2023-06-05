using OX.Bapps;
using OX.Network.P2P.Payloads;
using OX.IO.Json;

namespace OX.Wallets
{
    public abstract class Module : IUIModule, INotecaseTrigger
    {
        public Bapp Bapp { get; set; }
        public JObject moduleWalletSection { get; private set; }
        protected IModuleContainer Container;
        public abstract string ModuleName { get; }
        public abstract uint Index { get; }

        public abstract void InitEvents();
        public abstract void InitWindows();
        public abstract void HeartBeat(HeartBeatContext context);
        public abstract void ChangeWallet(INotecase operater);
        public abstract void OnBappEvent(BappEvent bappEvent);
        public abstract void OnCrossBappMessage(CrossBappMessage message);
        public abstract void OnBlock(Block block);
        public abstract void BeforeOnBlock(Block block);
        public abstract void AfterOnBlock(Block block);
        public abstract void OnRebuild();
        public void LoadBappModuleWalletSection(JObject bappSectionObject)
        {
            moduleWalletSection = bappSectionObject;
            if (moduleWalletSection.IsNull()) moduleWalletSection = new JObject();
            OnLoadBappModuleWalletSection(moduleWalletSection);
        }
        public abstract void OnLoadBappModuleWalletSection(JObject bappSectionObject);
        public Module(Bapp bapp)
        {
            this.Bapp = bapp;
        }
        public void Init(IModuleContainer container)
        {
            this.Container = container;
            InitWindows();
            InitEvents();
        }

    }
}
