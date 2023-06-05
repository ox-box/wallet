
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR.Client;
using OX.Wallets.Authentication;
using OX.Wallets.States;
using System.Security.Claims;
using System.Threading.Tasks;
namespace OX.Wallets.Hubs
{
    public abstract class HubComponentBase<T> : AuthComponentBase where T : IHubMessage
    {
        [Inject]
        protected IStateDispatch StateDispatcher { get; set; }
        [Inject]
        protected NavigationManager NavigationManager { get; set; }
        protected HubConnection? hubConnection;
        public bool IsConnected => hubConnection?.State == HubConnectionState.Connected;
        protected override void OnInit()
        {
            hubConnection = new HubConnectionBuilder()
          .WithUrl(NavigationManager.ToAbsoluteUri("/statehub").ToString(), options =>
          {
              //foreach (var cookie in HttpContextAccessor.HttpContext!.Request.Cookies)
              //options.Cookies.Add(new System.Net.Cookie(cookie.Key, cookie.Value) { Domain = HttpContextAccessor.HttpContext!.Request.Host.Host });
          })
          .WithAutomaticReconnect().Build();
            hubConnection.On<string, byte[]>(typeof(T).FullName, (connId, message) =>
            {
                HandleHubMessage(message);
                InvokeAsync(StateHasChanged);
            });
            hubConnection.On<string, string>("ReceiveMessage", (user, message) =>
            {
                OnBeat();
                //var encodedMsg = $"{user}: {message}";
                //messages.Add(encodedMsg);
                InvokeAsync(StateHasChanged);
            });
            this.OnHubInit();
            await hubConnection.StartAsync();
            //await base.OnInitializedAsync();
        }
        protected virtual void OnHubInit() { }

        protected async Task BroadAll(T message)
        {
            if (hubConnection is not null)
            {
                await hubConnection.SendAsync("BroadMessage", message.GetType().FullName, message.MessageData);
            }
        }
        protected async Task BroadGroup(string groupName, T message)
        {
            if (hubConnection is not null)
            {
                await hubConnection.SendAsync("GroupBroadMessage", groupName, message.GetType().FullName, message.MessageData);
            }
        }
        protected async Task SendMessage()
        {
            if (hubConnection is not null)
            {
                await hubConnection.SendAsync("BroadMessage", "", "");
            }
        }
        protected abstract void HandleHubMessage(byte[] messageData);
        protected abstract void OnBeat();
    }
}
