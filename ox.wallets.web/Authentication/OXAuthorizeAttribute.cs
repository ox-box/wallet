using System;
using System.Linq;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components;

namespace OX.Wallets.Authentication
{

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class OXAuthorizeAttribute : Attribute, IAsyncAuthorizationFilter, IActionFilter, IAsyncActionFilter
    {

        public OXAuthorizeAttribute()
        {
        }


        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;
            if (user.IsNotNull())
            {
            }
            await Task.CompletedTask;
        }
        void _buildPrincipal(ActionExecutingContext context)
        {

        }
        public virtual void OnActionExecuting(ActionExecutingContext context)
        {
            _buildPrincipal(context);
        }
        public virtual void OnActionExecuted(ActionExecutedContext context)
        {
        }

        //
        public virtual async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            if (next == null)
            {
                throw new ArgumentNullException("next");
            }
            OnActionExecuting(context);
            if (context.Result == null)
            {
                OnActionExecuted(await next());
            }
        }
    }

}
