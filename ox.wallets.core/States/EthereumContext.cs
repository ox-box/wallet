using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Security.Claims;
using OX.IO;


namespace OX.Wallets.States
{
    public interface IEthereumContext
    {
        event Action<string,int?> EthAddressChanged;
        string EthAddress { get; }
        int? Chain { get; }
        bool IsEthActive { get { return EthAddress.IsNotNullAndEmpty(); } }
        void SetEthAddress(string ethAddress, int? chain);
    }
    public class EthereumContext : IEthereumContext
    {
        public event Action<string,int?> EthAddressChanged;
        string _ethAddress;
        int? _chain;
        public string EthAddress { get { return _ethAddress; } }
        public int? Chain { get { return _chain; } }
        public void SetEthAddress(string ethAddress, int? chain)
        {
            _ethAddress = ethAddress;
            EthAddressChanged?.Invoke(ethAddress, chain);
        }
    }
}
