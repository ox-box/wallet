using AntDesign;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
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
        protected NavigationManager NavigationManager { get; set; }
        [Inject]
        protected ILocalStorageService LocalStorage { get; set; }
        public string Language { get; set; } = string.Empty;
        protected override async void OnInitialized()
        {
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
    }
}
