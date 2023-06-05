using OX.Bapps;
using OX.Network.P2P.Payloads;
using OX.IO.Json;
using System.Diagnostics;

namespace OX.Wallets
{
    public enum RunMode : byte
    {
        Node = 1,
        Mix = 2,
        Server = 3
    }
    public enum RunStatus : byte
    {
        Started = 1,
        WalletOpened = 2
    }
    public static class OXRunTime
    {
        public static int Port { get; set; }
        public static RunMode RunMode { get; set; }
        public static RunStatus RunState { get; set; }
        public static void GoWeb(string url)
        {
            var baseUrl = $"http://localhost:{Port}";
            Process.Start(new ProcessStartInfo($"{baseUrl}{url}") { UseShellExecute = true });
        }
        public static void OpenUrl(string url)
        {
            Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
        }
    }
}
