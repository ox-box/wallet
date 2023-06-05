using OX.Bapps;
using System;
using System.Collections.Generic;
using System.Linq;
namespace OX.Wallets
{
    public static class ModuleManager
    {
        public static void LoadModule(IModuleContainer container)
        {
            List<Module> ms = new List<Module>();

            foreach (var module in Bapp.AllUIModules())
            {
                if (module.Value is Module m)
                {
                    ms.Add(m);
                }
            }
            foreach (var module in ms.OrderBy(n => n.Index))
            {
                module.Init(container);
            };
        }
        public static void BroadCastAction(Action<Module> action)
        {
            foreach (var ui in Bapp.AllBappUis())
            {
                foreach (var module in ui.Modules)
                {
                    if (module is Module m)
                    {
                        action(m);
                    }
                }
            }
        }
    }
}
