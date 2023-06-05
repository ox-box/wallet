namespace OX.Wallets
{
    public class AccountListItem
    {
        public WalletAccount Account { get; private set; }
        public AccountListItem(WalletAccount account)
        {
            this.Account = account;
        }
        public override string ToString()
        {
            return $"{Account.Label}-{ Account.Address}";
        }
    }
    public class OpenAccountListItem
    {
        public OpenAccount Account { get; private set; }
        public OpenAccountListItem(OpenAccount account)
        {
            this.Account = account;
        }
        public override string ToString()
        {
            return Account.Address;
        }
    }
}
