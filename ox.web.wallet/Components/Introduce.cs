using AntDesign.ProLayout;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AntDesign;
using Microsoft.AspNetCore.Components.Authorization;
using OX.Wallets.States;
using Blazored.SessionStorage;
using OX.Wallets.Authentication;
using Nethereum.ABI.FunctionEncoding;
using Nethereum.ABI.Model;
using OX.MetaMask;
using OX.Wallets.Eths;
using Nethereum.Util;
using OX.Wallets.Messages;
using OX.Network.P2P.Payloads;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.AspNetCore.Components.Rendering;
using OX.Wallets;

namespace OX.Web.Components
{
    public partial class Introduce : Microsoft.AspNetCore.Components.ComponentBase
    {
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            var html = DNPHelper.GetDNPSetting()?.DNP_Introduce;
            if (html.IsNotNullAndEmpty())
            {
                builder.AddMarkupContent(0,html);
            }
        }
    }
}