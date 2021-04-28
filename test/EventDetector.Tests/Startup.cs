using EventDetector.Interfaces;
using EventDetector.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.IO;

namespace EventDetector.Tests
{
    public static class Startup
    {
        public static IConfiguration BuildConfiguration()
        {
            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
        }

        public static IServiceProvider ConfigureServices(IConfiguration configuration)
        {
            var logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .WriteTo.Console()
                .CreateLogger();

            return new ServiceCollection()
                .AddLogging(builder =>
                {
                    builder.SetMinimumLevel(LogLevel.Debug); // ILogger safety filter - Allows setting Serilog:MinimumLevel:Default to Debug
                    builder.AddSerilog(logger: logger, dispose: true);
                })
                .AddSingleton<IEventDetectorService, HashEventDetectorService>()
                .AddSingleton<IEventDetectorService, LinqEventDetectorService>()
                .BuildServiceProvider();
        }
    }
}