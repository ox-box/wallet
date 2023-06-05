namespace OX.Wallets.Base.Wallets
{
    public class TxOutListBoxItem : TransferOutput
    {
        public string AssetName;

        public override string ToString()
        {
            return $"{Wallet.ToAddress(ScriptHash)}   {Value}   {AssetName}";
        }
    }
}
