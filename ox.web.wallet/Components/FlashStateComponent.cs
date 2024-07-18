using AntDesign.ProLayout;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AntDesign;
using Microsoft.AspNetCore.Components.Authorization;
using OX.Wallets.States;
using Blazored.LocalStorage;
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
using Markdig;
using System;

namespace OX.Web.Components
{
    public partial class FlashStateComponent : Microsoft.AspNetCore.Components.ComponentBase
    {
        [Parameter]
        public FlashStateRecord StateRecord { get; set; }
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            var html = "";
            var pipeline = new MarkdownPipelineBuilder().UseAdvancedExtensions().Build();
            if (StateRecord.FlashState.TextData.IsNotNullAndEmpty() && StateRecord.FlashState.TextData.Length > 1)
            {
                html += "<li>";
                var str = System.Text.Encoding.UTF8.GetString(StateRecord.FlashState.TextData);
                if (str.IsNotNullAndEmpty())
                    html += Markdown.ToHtml(str, pipeline);
                html += "</li>";
            }

            if (StateRecord.FlashState.ImageData.IsNotNullAndEmpty() && StateRecord.FlashState.ImageData.Length > 1)
            {
                var base64String = Convert.ToBase64String(StateRecord.FlashState.ImageData);
                html += $"<li><img src='data:image/jpg;base64,{base64String}'/></li>";
            }
            if (html.IsNotNullAndEmpty())
            {
                var str = Markdown.ToHtml(html, pipeline);
                if (str.IsNotNullAndEmpty())
                {
                    builder.AddMarkupContent(0, str);
                }
            }
        }
    }
}