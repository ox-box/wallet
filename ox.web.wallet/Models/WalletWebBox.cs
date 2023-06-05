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

        public override uint BoxIndex { get { return 10; } }
        public WalletWebBox() : base()
        {
        }
        public override void Init()
        {

        }
        public override MenuDataItem[] GetMemus()
        {
            List<MenuDataItem> list = new List<MenuDataItem>();
            if (OXRunTime.RunMode == RunMode.Server)
            {
                list.Add(new MenuDataItem
                {
                    Path = "/blockchain",
                    Name = UIHelper.LocalString("区块链", "Blockchain"),
                    Key = "blockchain",
                    //Icon = "smile",
                    Children = new MenuDataItem[] {
                    new MenuDataItem
                    {
                        Path = "/blockchain/explorer",
                        Name =  UIHelper.LocalString("区块浏览", "Block Explorer"),
                        Key = "explorer"
                    },
                     new MenuDataItem
                    {
                        Path = "/blockchain/transaction",
                        Name =  UIHelper.LocalString("交易浏览", "Transaction Explorer"),
                        Key = "transaction"
                    },
                      new MenuDataItem
                    {
                        Path = "/blockchain/address",
                        Name =  UIHelper.LocalString("地址浏览", "Address Explorer"),
                        Key = "address"
                    }
                }
                });

                list.Add(new MenuDataItem
                {
                    Path = "/wallet",
                    Name = UIHelper.LocalString("账户", "Account"),
                    Key = "wallet",
                    //Icon = "smile",
                    Children = new MenuDataItem[] {
                    new MenuDataItem
                    {
                        Path = "/wallet/accountasset",
                        Name =  UIHelper.LocalString("账户资产", "Account Asset"),
                        Key = "accountasset"
                    }
                }
                });
                list.Add(new MenuDataItem
                {
                    Path = "/nft",
                    Name = UIHelper.LocalString("NFT", "NFT"),
                    Key = "nft",
                    //Icon = "smile",
                    Children = new MenuDataItem[] {
                    new MenuDataItem
                    {
                        Path = "/nft/resale",
                        Name =  UIHelper.LocalString("待售NFT", "For sale NFTs"),
                        Key = "resale"
                    },
                     new MenuDataItem
                    {
                        Path = "/nft/allnft",
                        Name =  UIHelper.LocalString("所有NFT", "All NFTs"),
                        Key = "allnft"
                    },
                     new MenuDataItem
                    {
                        Path = "/nft/mynft",
                        Name = UIHelper.LocalString("我持有的NFT", "My hold NFTs"),
                        Key = "mynft"
                    },
                     new MenuDataItem
                    {
                        Path = "/nft/buynft",
                        Name = UIHelper.LocalString("购买NFT", "Buy NFT"),
                        Key = "buynft"
                    }
                }
                });
            }
            return list.ToArray();
        }

    }
}
