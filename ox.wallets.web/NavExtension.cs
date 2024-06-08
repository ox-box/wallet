using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace OX.Wallets
{
    public static class NavExtension
    {
        public static string QueryString(this NavigationManager nav, string paramName)
        {
            var uri = nav.ToAbsoluteUri(nav.Uri);
            string paramValue = HttpUtility.ParseQueryString(uri.Query).Get(paramName);
            return paramValue ?? "";
        }
    }
}
