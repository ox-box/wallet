using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using OX.Notecase.Pages;
using OX.Wallets;

namespace OX.Notecase
{
    static class Program
    {
        private static NotecaseApp app;
        [STAThread]
        static void Main()
        {
            OXRunTime.RunMode = RunMode.Node;
            OXRunTime.RunState = RunStatus.Started;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            app = NotecaseApp.Instance;
            Application.Run(app.SyncForm = new SyncForm());
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
        }
    }
}
