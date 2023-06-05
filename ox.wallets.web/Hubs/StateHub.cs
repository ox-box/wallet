using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
//using ServiceStack;

namespace OX.Wallets.Hubs
{
    public class StateHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            var connId = this.Context.ConnectionId;
            await Clients.All.SendAsync("ReceiveMessage", user, $"{connId}--{message}");
        }
        //[Authorize]
        public async Task BroadMessage(string messageType, byte[] messageData)
        {
            var connId = this.Context.ConnectionId;
            await Clients.All.SendAsync(messageType, connId, messageData);
        }
        public async Task GroupBroadMessage(string groupName, string messageType, byte[] messageData)
        {
            var connId = this.Context.ConnectionId;
            await this.Clients.Group(groupName).SendAsync(messageType, connId, messageData);
        }
    }
}
