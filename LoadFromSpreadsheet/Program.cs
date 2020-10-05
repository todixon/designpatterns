using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;


namespace LoadFromSpreadsheet
{
    class Program
    {
        private static IServiceProvider _serviceProvider;

        static void Main()
        {
            RegisterServices();
            IServiceScope scope = _serviceProvider.CreateScope();
            scope.ServiceProvider.GetRequiredService<ParseApplication>().Run();
            DisposeServices();

        }

        private static void RegisterServices()
        {
            var services = new ServiceCollection();
            services.AddSingleton<IParser, Parser>();
            services.AddSingleton<ParseApplication>();
            
            string envtype = System.Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");       // get system environment variable 'Development'
            string logpath = AppDomain.CurrentDomain.BaseDirectory;

            var timestring = DateTime.Now.ToUniversalTime().ToString("yyyy'_'MM'_'dd'T'HH'_'mm'_'ss'_'fff'Z'");
            string downloadlog = $@"import_{timestring}.log";


            var builder = new ConfigurationBuilder()
                .SetBasePath(logpath)
                .AddJsonFile(Path.Combine(logpath, "appsettings.json"), optional: false, reloadOnChange: true)      // load primary app settings
                .AddJsonFile(Path.Combine(logpath, $"appsettings.{envtype}.json"), optional: false);        // overload with settings based on environment



            var config = builder.Build();

            var appsettings = config.GetSection("appsettings").Get<Application>();
            services.AddSingleton<Application>(appsettings);


            Log.Logger = new LoggerConfiguration()
                .WriteTo.File(downloadlog)
                .WriteTo.Console()
                .Enrich.FromLogContext()
                .Enrich.WithMachineName()
                .Enrich.WithProperty("Application", "Spreadsheet Import")
                .WriteTo.Seq(appsettings.SeqServer.ToString())
                .CreateLogger();

            _serviceProvider = services.BuildServiceProvider(true);
        }

        private static void DisposeServices()
        {
            if (_serviceProvider == null)
            {
                return;
            }
            if (_serviceProvider is IDisposable)
            {
                ((IDisposable)_serviceProvider).Dispose();
            }
        }
    }
}
