//using Microsoft.AspNetCore.Http;
using OX.IO.Data.LevelDB;
using OX.IO.Json;
using OX.Ledger;
using OX.Network.RPC;
using OX.VM;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Snapshot = OX.Persistence.Snapshot;
using OX.Plugins;
using OX.Network.P2P.Payloads;
using OX.Network.P2P;
using OX.IO;
using OX.Wallets;
using OX.SmartContract;
using Akka.Util.Internal;
using OX.Bapps;
using OX.Wallets.Base.Events;
using OX.Wallets.Base.Trust;
using OX.Wallets.Base.Help;
using OX.Wallets.Base.Letters;
using OX.Wallets.Base.NFT;
using OX.Wallets.Base.DNP;

namespace OX.Wallets.Base
{

    public class WalletUI : IBappUi
    {
        public Bapp Bapp { get; set; }
        Dictionary<string, IUIModule> _modules = new Dictionary<string, IUIModule>();
        public IUIModule[] Modules { get { return this._modules.Values.ToArray(); } }
        public WalletUI(Bapp bapp)
        {
            this.Bapp = bapp;
            WalletModule module = new WalletModule(bapp);
            this._modules[module.ModuleName] = module;
            AssetTrustModule tm = new AssetTrustModule(bapp);
            this._modules[tm.ModuleName] = tm;
            HelpModule m = new HelpModule(bapp);
            this._modules[m.ModuleName] = m;
            LetterModule lm = new LetterModule(bapp);
            this._modules[lm.ModuleName] = lm;
            EventModule em = new EventModule(bapp);
            this._modules[em.ModuleName] = em;
            NFTModule nm = new NFTModule(bapp);
            this._modules[nm.ModuleName] = nm;
            if(OXRunTime.RunMode== RunMode.Server)
            {
                DNPModule dnpmodule = new DNPModule(bapp);
                this._modules[dnpmodule.ModuleName] = dnpmodule;
            }
            //BookModule bm = new BookModule(bapp);
            //this._modules[bm.ModuleName] = bm;
        }
        public void OnBappEvent(BappEvent bappEvent)
        {
            foreach (var m in this.Modules)
                if (m is Module module)
                    module.OnBappEvent(bappEvent);
        }
        public void OnCrossBappMessage(CrossBappMessage message)
        {
            foreach (var m in this.Modules)
                if (m is Module module)
                    module.OnCrossBappMessage(message);
        }
        public void OnBlock(Block block)
        {
            foreach (var m in this.Modules)
                if (m is Module module)
                    module.OnBlock(block);
        }
        public void BeforeOnBlock(Block block)
        {
            foreach (var m in this.Modules)
                if (m is Module module)
                    module.BeforeOnBlock(block);
        }
        public void AfterOnBlock(Block block)
        {
            foreach (var m in this.Modules)
                if (m is Module module)
                    module.AfterOnBlock(block);
        }
        public void OnRebuild()
        {
            foreach (var m in this.Modules)
                if (m is Module module)
                    module.OnRebuild();
        }
    }
}
