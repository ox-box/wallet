using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AntDesign.ProLayout;
using OX.Wallets;

namespace OX.Web.Models
{
    public class HelpWebBox : WebBoxBlazor
    {

        public override uint BoxIndex { get { return 1000000; } }
        public override string Name => UIHelper.LocalString("帮助", "Help");
        public override bool SupportMobile => false;
        public HelpWebBox() : base()
        {
        }
        public override void Init()
        {

        }
        public override MenuDataItem[] GetMemus()
        {
            List<MenuDataItem> list = new List<MenuDataItem>();
           
                list.Add(new MenuDataItem
                {
                    Path = "/_pc/help",
                    Name = UIHelper.LocalString("帮助", "Help"),
                    Key = "help",
                    //Icon = "smile",
                    Children = new MenuDataItem[] {
                    new MenuDataItem
                    {
                        Path = "/_pc/help/urls",
                        Name =  UIHelper.LocalString("门户地址", "Portal Urls"),
                        Key = "urls"
                    } 
                }
                });
 
            return list.ToArray();
        }
        public override MenuDataItem[] GetMobileMemus()
        {
            List<MenuDataItem> list = new List<MenuDataItem>();
         
            return list.ToArray();
        }
    }
}
