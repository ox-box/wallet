using AntDesign;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OX.Wallets
{
    public class WebBoxComponentBase : AntDomComponentBase
    {
        [Inject]
        protected NavigationManager NavigationManager { get; set; }
        protected override void OnInitialized()
        {
            if (OXRunTime.RunState != RunStatus.WalletOpened)
                NavigationManager.NavigateTo("/");
            base.OnInitialized();
        }
    }
}
