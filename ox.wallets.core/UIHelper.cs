using OX.Network.P2P.Payloads;
using OX.SmartContract;
using System;
using System.Collections.Generic;

namespace OX.Wallets
{
    public static class UIHelper
    {
        public static bool SignAndSendTx(this INotecase operater, Transaction tx)
        {
            ContractParametersContext context;
            try
            {
                context = new ContractParametersContext(tx);
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Error creating contract params: {ex}");
                throw;
            }
            operater.Wallet.Sign(context);
            string msg;
            if (context.Completed)
            {
                tx.Witnesses = context.GetWitnesses();
                operater.Wallet.ApplyTransaction(tx);
                operater.Relay(tx);
                msg = $"Signed and relayed transaction with hash={tx.Hash}";
                Console.WriteLine(msg);
                return true;
            }
            msg = $"Failed sending transaction with hash={tx.Hash}";
            Console.WriteLine(msg);
            return true;
        }

        public static bool IsChina()
        {
            return System.Globalization.CultureInfo.InstalledUICulture.Name.ToLower() == "zh-cn";
        }
        public static string LocalString(string ChinaString, string EnglishString)
        {
            return IsChina() ? ChinaString : EnglishString;
        }
        
    }
}
