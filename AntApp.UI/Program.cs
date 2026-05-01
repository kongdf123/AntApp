using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Microsoft.Extensions.Configuration;

namespace AntApp.UI
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var builder = Host.CreateDefaultBuilder()
                .UseSerilog((context, services, configuration) =>
                {
                    configuration
                        .ReadFrom.Configuration(context.Configuration)
                        .ReadFrom.Services(services)
                        .Enrich.FromLogContext();
                })
                .ConfigureServices((context, services) =>
                {
                    services.AddSingleton<MainForm>();

                    ////services.AddSingleton<SqliteConnectionFactory>(sp =>
                    ////{
                    ////    var config = sp.GetRequiredService<IConfiguration>();
                    ////    var connStr = config.GetConnectionString("Default");
                    ////    return new SqliteConnectionFactory(connStr);
                    ////}); 
                });

            var host = builder.Build();

            SetupGlobalExceptionHandling(host);

            try
            {
                Log.Information("Application starting...");

                // To customize application configuration such as set high DPI settings or default font,
                // see https://aka.ms/applicationconfiguration.
                ApplicationConfiguration.Initialize();

                var mainForm = host.Services.GetRequiredService<MainForm>();
                Application.Run(mainForm);
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "App crashed");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        static void SetupGlobalExceptionHandling(IHost host)
        {
            var logger = host.Services.GetRequiredService<ILogger>();

            Application.ThreadException += (sender, args) =>
            {
                logger.Error(args.Exception, "UI Thread Exception");
            };

            AppDomain.CurrentDomain.UnhandledException += (sender, args) =>
            {
                logger.Fatal(args.ExceptionObject as Exception, "Unhandled Exception");
            };

            TaskScheduler.UnobservedTaskException += (sender, args) =>
            {
                logger.Error(args.Exception, "Unobserved Task Exception");
                args.SetObserved();
            };
        }


    }
}