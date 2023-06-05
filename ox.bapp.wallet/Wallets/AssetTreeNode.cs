using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OX.Ledger;
using OX.Wallets.UI.Controls;
using OX.Wallets.UI.Docking;



namespace OX.Wallets.Base.Wallets
{
    public class AssetTreeNode : DarkTreeNode
    {
        public readonly AssetLeaftTreeNode OXS = new AssetLeaftTreeNode("OXS");
        public readonly AssetLeaftTreeNode OXC = new AssetLeaftTreeNode("OXC");

        public AssetTreeNode(BalanceModel balance)
        {
            //this.ExpandedIcon = Icons.folder_open;
            //this.Icon = Icons.folder_closed;
            //this.Nodes.Add(Address);
            Nodes.Add(OXS);
            Nodes.Add(OXC);

            SetBalance(balance);
        }
        public BalanceModel Balance { get; private set; }
        public void SetBalance(BalanceModel model)
        {
            Balance = model;
            var s = model.OnlyWatch ? "   #   " : string.Empty;
            if (model.Label.IsNotNullAndEmpty())
                s += $"[{model.Label}] ";
            Text = s + model.Address;
            //this.Address.SetContent(model.Address);
            OXS.SetContent(model.GTS.ToString());
            OXC.SetContent(model.GTC.ToString());
        }

    }

    public class AssetLeaftTreeNode : DarkTreeNode
    {
        public string Title { get; private set; }
        public string Content { get; private set; }
        public AssetLeaftTreeNode(string Title)
        {
            //this.Icon = Icons.files;
            this.Title = Title;
            Text = $"{this.Title}:  {Content}";
        }
        public void SetContent(string Content)
        {
            this.Content = Content;
            Text = $"{Title}:  {this.Content}";
        }
    }
}
