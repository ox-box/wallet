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
using System.Reflection;
using System.Security.Cryptography.X509Certificates;

namespace OX.Mix
{
    public class Program
    {
        static bool createNew;
        private static NotecaseApp app;
        [STAThread]
        public static void Main(string[] args)
        {
            using (Mutex mutex = new Mutex(true, Application.ProductName, out createNew))
            {
                if (createNew)
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
                else
                {
                    MessageBox.Show(UIHelper.LocalString("OX娱乐程序已经在运行中...", " OX casino program is already running..."));
                    System.Threading.Thread.Sleep(1000);
                    System.Environment.Exit(1);
                }
            }
        }

        private static void App_OnHeartBeat(Wallets.HeartBeatContext obj)
        {

        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            using (FileStream fs = new FileStream("error.log", FileMode.Create, FileAccess.Write, FileShare.None))
            using (StreamWriter w = new StreamWriter(fs))
                if (e.ExceptionObject is Exception ex)
                {
                    PrintErrorLogs(w, ex);
                }
                else
                {
                    w.WriteLine(e.ExceptionObject.GetType());
                    w.WriteLine(e.ExceptionObject);
                }
        }
        private static void PrintErrorLogs(StreamWriter writer, Exception ex)
        {
            writer.WriteLine(ex.GetType());
            writer.WriteLine(ex.Message);
            writer.WriteLine(ex.StackTrace);
            if (ex is AggregateException ex2)
            {
                foreach (Exception inner in ex2.InnerExceptions)
                {
                    writer.WriteLine();
                    PrintErrorLogs(writer, inner);
                }
            }
            else if (ex.InnerException != null)
            {
                writer.WriteLine();
                PrintErrorLogs(writer, ex.InnerException);
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
           Host.CreateDefaultBuilder(args)
               .ConfigureWebHostDefaults(webBuilder =>
               {

                   OXRunTime.Port = Settings.Default.P2P.ApiPort;
                   //webBuilder.UseUrls($"http://*:{Settings.Default.P2P.ApiPort}");
                   webBuilder.UseSetting(WebHostDefaults.DetailedErrorsKey, "true");
                   webBuilder.UseStartup<Startup>();
                   webBuilder.UseKestrel(options =>
                   {
                       options.ListenAnyIP(Settings.Default.P2P.ApiPort);
                       var path = $"{Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)}\\obw.pfx";
                       bool ok = true;
                       X509Certificate2 cert = default;
                       try
                       {
                           cert = new X509Certificate2(path, "qazqwe");
                       }
                       catch (Exception e)
                       {
                           ok = false;
                       }
                       if (ok)
                       {
                           options.Listen(System.Net.IPAddress.Any, 443, listenOptions =>
                           {
                               listenOptions.UseHttps(cert);
                           });
                       }
                   });

               });
    }
}
