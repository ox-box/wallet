using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
//using ServiceStack;

namespace OX.Wallets.Hubs
{
    public interface IOXID
    {

    }
    public interface IHubMessage
    {
        IOXID OXID { get; }
        byte[] MessageData { get; }
    }
    public abstract class HubMessage<T> where T : IHubMessage, new()
    {
        public string MessageType { get { return typeof(T).FullName; } }
    }
}
