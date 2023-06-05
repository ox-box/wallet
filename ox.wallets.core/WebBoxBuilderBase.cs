using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using OX.Bapps;

namespace OX.Wallets
{
    public class WebBoxBuilderBase
    {
        static WebBoxBuilderBase()
        {
            foreach (var assembly in Bapp.Assemblies)
            {
                foreach (Type type in assembly.ExportedTypes)
                {
                    if (!type.IsSubclassOf(typeof(WebBox))) continue;
                    if (type.IsAbstract) continue;

                    ConstructorInfo constructor = type.GetConstructor(Type.EmptyTypes);
                    try
                    {
                        constructor?.Invoke(null);
                    }
                    catch (Exception ex)
                    {
                    }
                }
            }
        }       
    }
}
