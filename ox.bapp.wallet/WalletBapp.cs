using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OX.Bapps;
using OX.Wallets.Base;
using OX.Network.P2P.Payloads;
using OX.Cryptography.ECC;
using OX.Wallets.Flash;

namespace OX.Wallets
{
    public class WalletBapp : Bapp
    {
        public override string MatchKernelVersion => "1.2.0";
        public override ECPoint[] BizPublicKeys => default;
        public override IFlashMessageProvider BuildFlashMessageProvider()
        {
            return new FlashMessageProvider(this, null, null, null);
        }
        public override IBappProvider BuildBappProvider()
        {
            return new WalletBappProvider(this);
        }
        public override IBappApi BuildBappApi()
        {
            return default;
            //return new WalletApi(this);
        }
        public override IBappUi BuildBappUi()
        {
            return new WalletUI(this);
        }
        public override SideScope[] GetSideScopes()
        {
            return default;
        }
        protected override void InitBapp() { }

    }
}
