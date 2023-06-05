﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Memory;
using OX.Network.P2P;
using System.Net;
using System.Linq;

namespace OX.Notecase
{


    public partial class Settings
    {
        public PathsSettings Paths { get; }
        public P2PSettings P2P { get; }
        public string PluginURL { get; }
        public SeedSettings SeedNode { get; }
        //public static Settings Default { get; }
        //static Settings()
        //{
        //    Default = new Settings();
        //}

        public Settings()
        {
            IConfigurationSection section = new ConfigurationBuilder().AddJsonFile("config.json").Build().GetSection("ApplicationConfiguration");
            this.Paths = new PathsSettings(section.GetSection("Paths"));
            this.P2P = new P2PSettings(section.GetSection("P2P"));
            this.SeedNode = new SeedSettings(section.GetSection("Seeds"));
        }
    }

    public class PathsSettings
    {
        public string Chain { get; }
        public string BizChain { get; }
        public string Index { get; }

        public PathsSettings(IConfigurationSection section)
        {
            this.Chain = string.Format(section.GetSection("Chain").Value, Message.Magic.ToString("X8"));
            this.BizChain = string.Format(section.GetSection("BizChain").Value, Message.Magic.ToString("X8"));
            this.Index = string.Format(section.GetSection("Index").Value, Message.Magic.ToString("X8"));
        }
    }

    public class P2PSettings
    {
        public ushort Port { get; }
        public ushort WsPort { get; }
        public ushort ApiPort { get; }
        public bool OnlySeed { get; }
        public int MinDesiredConnections { get; }
        public int MaxConnections { get; }
        public int MaxConnectionsPerAddress { get; }

        public P2PSettings(IConfigurationSection section)
        {
            this.Port = ushort.Parse(section.GetSection("Port").Value);
            this.WsPort = ushort.Parse(section.GetSection("WsPort").Value);
            this.ApiPort = ushort.Parse(section.GetSection("ApiPort").Value);
            string onlyconnectseed = section.GetValue("OnlySeed", "false");
            this.OnlySeed = onlyconnectseed.ToLower() == "true";
            this.MinDesiredConnections = section.GetValue("MinDesiredConnections", Peer.DefaultMinDesiredConnections);
            this.MaxConnections = section.GetValue("MaxConnections", Peer.DefaultMaxConnections);
            this.MaxConnectionsPerAddress = section.GetValue("MaxConnectionsPerAddress", 3);
        }
    }
    public class SeedSettings
    {
        public string[] Seeds { get; }

        public SeedSettings(IConfigurationSection section)
        {
            Seeds = section.GetChildren().Select(p => p.Get<string>()).ToArray();
        }
    }
}
