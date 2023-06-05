using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Linq;
using System.Reflection;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Akka.Actor;
using OX.Ledger;
using OX.Network.P2P.Payloads;
using OX.IO;
using OX.IO.Data.LevelDB;
using OX.VM;
using OX.Cryptography;
using OX.IO.Caching;
using OX.Network;
using OX.Wallets;
using OX.SmartContract;
using OX.Wallets.NEP6;
using OX.Network.P2P;
using OX.Persistence.LevelDB;

namespace OX.Notecase
{
    public class WalletsBlockHandler : BlockHandler
    {
        public override string[] BizAddresses => new string[] { };
        public static event BlockChainHandler<Block> SyncBlocksCompleted;
        public static event BlockChainHandler<Block> BlockCompleted;
        public WalletsBlockHandler(OXSystem oxsystem) : base(oxsystem)
        {
            this.Start();
        }
        static IActorRef _instance;
        public static IActorRef Instance
        {
            get
            {
                if (_instance == default)
                {
                    _instance = Build();
                }
                return _instance;
            }
        }
        static IActorRef Build()
        {
            var seeds = Settings.Default.SeedNode.Seeds;
            var extseeds = Settings.Default.ExtSeeds;
            if (extseeds != default && extseeds.Length > 0)
            {
                seeds = extseeds.Union(seeds).ToArray();
            }
            ProtocolSettings.InitSeed(seeds, Settings.Default.P2P.OnlySeed);
            LevelDBStore store = new LevelDBStore(Settings.Default.Paths.Chain);
            var oxsystem = new OXSystem(store);
            return oxsystem.ActorSystem.ActorOf(Akka.Actor.Props.Create(() => new WalletsBlockHandler(oxsystem)));
        }

        public override void OnStart()
        {
            this.oxsystem.StartNode(Settings.Default.P2P.Port, Settings.Default.P2P.WsPort, 0);
        }
        public override void OnStop() { }
        protected override void OnReceived(object message)
        {
        }
        protected override void OnBlockPersistCompleted(Block block)
        {
            BlockCompleted?.Invoke(block);
            if (Blockchain.Singleton.Height == Blockchain.Singleton.HeaderHeight)
                SyncBlocksCompleted?.Invoke(block);
        }

    }
}
