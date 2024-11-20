using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AntDesign;
using AntDesign.ProLayout;
using Microsoft.AspNetCore.Mvc;
using OX.Bapps;

namespace OX.Wallets
{   
    public abstract class ApiBoxBuilder
    {
        public abstract string ApiBoxName { get; }
        public abstract ApiBox Build();
    }
    public abstract class ApiModuleBuilder
    {
        public abstract string ApiModuleName { get; }
        public abstract ApiModule Build();
    }
    public abstract class ApiModule
    {
        protected Dictionary<string, ApiActionBuilder> actionBuilders = new Dictionary<string, ApiActionBuilder>();
        public IActionResult ModuleGet(ControllerBase controller, string ApiActionName, string arg)
        {
            if (this.actionBuilders.TryGetValue(ApiActionName, out var actionBuilder))
            {
                var apiAction = actionBuilder.Build();
                if (apiAction.IsNull()) return controller.StatusCode(404);
                return apiAction.ActionGet(controller, arg);
            }
            return controller.StatusCode(404);
        }
        public IActionResult ModulePost(ControllerBase controller, string ApiActionName, string arg)
        {
            if (this.actionBuilders.TryGetValue(ApiActionName, out var actionBuilder))
            {
                var apiAction = actionBuilder.Build();
                if (apiAction.IsNull()) return controller.StatusCode(404);
                return apiAction.ActionPost(controller, arg);
            }
            return controller.StatusCode(404);
        }
        public void RegisterApiAction(ApiActionBuilder apiActionBuilder)
        {
            actionBuilders[apiActionBuilder.ApiActionName] = apiActionBuilder;
        }
    }

    public abstract class ApiActionBuilder
    {
        public abstract string ApiActionName { get; }
        public abstract ApiAction Build();
    }
    public abstract class ApiAction
    {
        public abstract IActionResult ActionGet(ControllerBase controller, string arg);
        public abstract IActionResult ActionPost(ControllerBase controller, string arg);
    }


    public abstract class ApiBox
    {
        protected Dictionary<string, ApiModuleBuilder> moduleBuilders = new Dictionary<string, ApiModuleBuilder>();
        public IActionResult ProcessGet(ControllerBase controller, string ApiModuleName, string ApiActionName, string arg)
        {
            if (this.moduleBuilders.TryGetValue(ApiModuleName, out var moduleBuilder))
            {
                var apiModule = moduleBuilder.Build();
                if (apiModule.IsNull()) return controller.StatusCode(404);
                return apiModule.ModuleGet(controller, ApiActionName, arg);
            }
            return controller.StatusCode(404);
        }
        public IActionResult ProcessPost(ControllerBase controller, string ApiModuleName, string ApiActionName, string arg)
        {
            if (this.moduleBuilders.TryGetValue(ApiModuleName, out var moduleBuilder))
            {
                var apiModule = moduleBuilder.Build();
                if (apiModule.IsNull()) return controller.StatusCode(404);
                return apiModule.ModulePost(controller, ApiActionName, arg);
            }
            return controller.StatusCode(404);
        }

        public void RegisterApiModule(ApiModuleBuilder apiModuleBuilder)
        {
            moduleBuilders[apiModuleBuilder.ApiModuleName] = apiModuleBuilder;
        }
    }
}
