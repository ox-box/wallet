using OX.Network.P2P.Payloads;
using OX.SmartContract;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace OX.Wallets
{
    public static class FormUIHelper
    {
        private static Dictionary<Type, Form> tool_forms = new Dictionary<Type, Form>();

        private static void Helper_FormClosing(object sender, FormClosingEventArgs e)
        {
            tool_forms.Remove(sender.GetType());
        }

        public static T Show<T>(this Module module) where T : Form, IModuleComponent, new()
        {
            Type t = typeof(T);
            if (!tool_forms.ContainsKey(t))
            {
                tool_forms.Add(t, new T());
                tool_forms[t].FormClosing += Helper_FormClosing;
            }
            T instance = tool_forms[t] as T;
            instance.Module = module;
            instance.Show();
            instance.Activate();
            return instance;
        }
        public static T ShowDialog<T>(this Module module, Action<T> action = default) where T : Form, IModuleComponent, INotecaseTrigger, new()
        {
            Type t = typeof(T);
            if (!tool_forms.ContainsKey(t))
            {
                tool_forms.Add(t, new T());
                tool_forms[t].FormClosing += Helper_FormClosing;
            }
            T instance = tool_forms[t] as T;
            instance.Module = module;
            if (action != default)
            {
                action(instance);
            }
            instance.ShowDialog();
            instance.Activate();
            return instance;
        }


        public static void DoInvoke(this Control control, Action action)
        {
            try
            {
                if (control.InvokeRequired)
                {
                    control.BeginInvoke(new Action(() =>
                    {
                        action();
                    }));
                }
                else
                {
                    action();
                }
            }
            catch
            {

            }
        }
    }
}
