using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Security.Claims;
using OX.IO;
using OX.Network.P2P.Payloads;

namespace OX.Wallets.Eths
{

    public class EthID
    {
        public string EthAddress { get; set; }
        public UInt160 MapAddress { get; private set; }
        public uint AddressID { get; private set; }
        public EthID(string ethAddress)
        {
            EthAddress = ethAddress;
            MapAddress = EthAddress.BuildMapAddress();
            AddressID = MapAddress.BuildAddressId();
        }

        public override bool Equals(object obj)
        {
            if (obj is EthID ethid)
            {
                return ethid.EthAddress.ToLower() == this.EthAddress.ToLower();
            }
            return base.Equals(obj);
        }
        public override int GetHashCode()
        {
            return this.EthAddress.ToLower().GetHashCode();
        }
    }
}
