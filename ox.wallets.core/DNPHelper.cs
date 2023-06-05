using OX.Bapps;
using OX.Network.P2P.Payloads;
using OX.IO.Json;
using System.Diagnostics;

namespace OX.Wallets
{
    public class DNPSetting
    {
        internal DNPSetting(JObject dnp)
        {
            if (dnp.IsNotNull())
            {
                DNP_Name = dnp["dnp_name"]?.AsString();
                DNP_Introduce = dnp["dnp_introduce"]?.AsString();
            }
        }
        public string DNP_Name { get; set; }
        public string DNP_Introduce { get; set; }

        public JObject Build()
        {
            JObject dnp = new JObject();
            dnp["dnp_name"] = DNP_Name;
            dnp["dnp_introduce"] = DNP_Introduce;
            return dnp;
        }
    }

    public static class DNPHelper
    {
        public static DNPSetting GetDNPSetting()
        {
            return new DNPSetting(dnp);
        }
        static JObject dnp;
        public static void SetDNP(JObject setting)
        {
            dnp = setting;
        }
    }
}
