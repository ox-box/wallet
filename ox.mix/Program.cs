using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Windows.Forms;
using OX.Notecase;
using OX.Notecase.Pages;
using OX.Wallets;

namespace OX.Mix
{
    public class Program
    {
        private static NotecaseApp app;
        [STAThread]
        public static void Main(string[] args)
        {
            OXRunTime.RunMode = RunMode.Mix;
            OXRunTime.RunState = RunStatus.Started;
            ApplicationConfiguration.Initialize();
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            app = NotecaseApp.Instance;
            app.OnHeartBeat += App_OnHeartBeat;
            WebStarter.Instance.WebStartAction = () =>
            {
                Task.Factory.StartNew(() =>
                {
                    CreateHostBuilder(args).Build().Run();
                });
            };
            Application.Run(app.SyncForm = new SyncForm());
        }

        private static void App_OnHeartBeat(Wallets.HeartBeatContext obj)
        {

        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    OXRunTime.Port = Settings.Default.P2P.ApiPort;
                    webBuilder.UseUrls($"http://*:{Settings.Default.P2P.ApiPort}");
                    webBuilder.UseSetting(WebHostDefaults.DetailedErrorsKey, "true");
                    webBuilder.UseStartup<Startup>();
                });
    }
}
