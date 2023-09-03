using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AntDesign.ProLayout;
using OX.Wallets;

namespace OX.Web.Models
{
    public class WalletWebBox : WebBoxBlazor
    {
        //public override string Name => this.WebLocalString("钱包", "Wallet");
        public override bool SupportMobile => false;
        public override uint BoxIndex { get { return 10; } }
        public WalletWebBox() : base()
        {
        }
        public override void Init()
        {

        }
        public override MenuDataItem[] GetMemus(string language)
        {
            List<MenuDataItem> list = new List<MenuDataItem>();
            if (OXRunTime.RunMode == RunMode.Server)
            {
                list.Add(new MenuDataItem
                {
                    Path = "/_pc/blockchain",
                    Name = UIHelper.WebLocalString(language, "区块链", "Blockchain"),
                    Key = "blockchain",
                    //Icon = "smile",
                    Children = new MenuDataItem[] {
                    new MenuDataItem
                    {
                        Path = "/_pc/blockchain/explorer",
                        Name = UIHelper.WebLocalString(language, "区块浏览", "Block Explorer"),
                        Key = "explorer"
                    },
                     new MenuDataItem
                    {
                        Path = "/_pc/blockchain/transaction",
                        Name = UIHelper.WebLocalString(language, "交易浏览", "Transaction Explorer"),
                        Key = "transaction"
                    },
                      new MenuDataItem
                    {
                        Path = "/_pc/blockchain/address",
                        Name = UIHelper.WebLocalString(language, "地址浏览", "Address Explorer"),
                        Key = "address"
                    }
                      ,
                      new MenuDataItem
                    {
                        Path = "/_pc/blockchain/tokens",
                        Name = UIHelper.WebLocalString(language, "资产详情", "Asset Details"),
                        Key = "tokens"
                    }
                }
                });

                list.Add(new MenuDataItem
                {
                    Path = "/_pc/wallet",
                    Name = UIHelper.WebLocalString(language, "账户", "Account"),
                    Key = "wallet",
                    //Icon = "smile",
                    Children = new MenuDataItem[] {
                    new MenuDataItem
                    {
                        Path = "/_pc/wallet/accountasset",
                        Name = UIHelper.WebLocalString(language, "账户资产", "Account Asset"),
                        Key = "accountasset"
                    }
                }
                });
                list.Add(new MenuDataItem
                {
                    Path = "/_pc/nft",
                    Name = UIHelper.WebLocalString(language, "NFT", "NFT"),
                    Key = "nft",
                    //Icon = "smile",
                    Children = new MenuDataItem[] {
                    new MenuDataItem
                    {
                        Path = "/_pc/nft/resale",
                        Name =  UIHelper.WebLocalString(language, "待售NFT", "For sale NFTs"),
                        Key = "resale"
                    },
                     new MenuDataItem
                    {
                        Path = "/_pc/nft/allnft",
                        Name = UIHelper.WebLocalString(language, "所有NFT", "All NFTs"),
                        Key = "allnft"
                    },
                     new MenuDataItem
                    {
                        Path = "/_pc/nft/mynft",
                        Name =UIHelper.WebLocalString(language, "我持有的NFT", "My hold NFTs"),
                        Key = "mynft"
                    },
                     new MenuDataItem
                    {
                        Path = "/_pc/nft/buynft",
                        Name = UIHelper.WebLocalString(language, "购买NFT", "Buy NFT"),
                        Key = "buynft"
                    }
                }
                });
            }
            return list.ToArray();
        }
        public override MenuDataItem[] GetMobileMemus(string language)
        {
            List<MenuDataItem> list = new List<MenuDataItem>();

            return list.ToArray();
        }
    }
}
