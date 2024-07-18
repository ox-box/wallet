using AntDesign;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using OX.Wallets.Eths;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OX.Wallets
{
    public class WebBoxComponentBase : AntDomComponentBase, ILanguage
    {
        [Inject]
        protected IHttpContextAccessor HttpContextAccessor { get; set; }
        [Inject]
        protected NavigationManager NavigationManager { get; set; }
        [Inject]
        protected ILocalStorageService LocalStorage { get; set; }
        public string Language { get; set; } = string.Empty;
        public bool IsMobile = false;
        public UserAgentStatus UserAgentStatus = default;
        protected override async void OnInitialized()
        {
            IsMobile = this.HttpContextAccessor.IsMobileBrowser();
            UserAgentStatus = this.HttpContextAccessor?.HttpContext?.Request?.GetUserAgent();
            if (OXRunTime.RunState != RunStatus.WalletOpened)
                NavigationManager.NavigateTo("/");
            Language = await this.GetLocalStorage("_ox_box_language");
            base.OnInitialized();
            this.OnInitWebBox();
        }
        protected virtual void OnInitWebBox()
        {

        }
        public async Task SetLocalStorage(string key, string value)
        {
            await LocalStorage.SetItemAsync(key, value);
        }
        public async ValueTask<string> GetLocalStorage(string key)
        {
            return await LocalStorage.GetItemAsync<string>(key);
        }
        public bool TryOmitString(string str, out string outStr, int omitLength = 6)
        {
            if (UserAgentStatus.IsNotNull() && !UserAgentStatus.IsMobile)
            {
                outStr = str;
                return false;
            }
            else
            {
                outStr = str.Omit(omitLength);
                return true;

            }
        }
        public string OmitString(string str, int omitLength = 6)
        {
            this.TryOmitString(str, out string outStr, omitLength);
            return outStr;
        }
        public bool TryOmitOnlyLeftString(string str, out string outStr, int omitLength = 6)
        {
            if (UserAgentStatus.IsNotNull() && !UserAgentStatus.IsMobile)
            {
                outStr = str;
                return false;
            }
            else
            {
                outStr = str.OmitOnlyLeft(omitLength);
                return true;

            }
        }
        public string OmitOnlyLeftString(string str, int omitLength = 6)
        {
            this.TryOmitOnlyLeftString(str, out string outStr, omitLength);
            return outStr;
        }
        public bool TryOmitOnlyRightString(string str, out string outStr, int omitLength = 6)
        {
            if (UserAgentStatus.IsNotNull() && !UserAgentStatus.IsMobile)
            {
                outStr = str;
                return false;
            }
            else
            {
                outStr = str.OmitOnlyRight(omitLength);
                return true;

            }
        }
        public string OmitOnlyRightString(string str, int omitLength = 6)
        {
            this.TryOmitOnlyRightString(str, out string outStr, omitLength);
            return outStr;
        }
    }
}
