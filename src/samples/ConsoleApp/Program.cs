﻿using Calamity;

using ClassLibrary2;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ConsoleApp
{


    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .AddLogging(cfg => 
                    cfg.AddConsole()).Configure<LoggerFilterOptions>(cfg => cfg.MinLevel = LogLevel.Debug).BuildServiceProvider();

            PluginLoaderOptions.Instance.LoggerFactory = serviceProvider.GetService<ILoggerFactory>();

            var path = @"C:\Users\danie\source\repos\Calamity\ClassLibrary1\bin\Debug\netstandard2.1\ClassLibrary1.dll";

            var pluginContext = PluginBuilderFactory
                .CreateFromAssembly(path)
                .Build();

            var loader = new PluginLoader();

            var plugin = loader.Instantiate<ITestPlugin>(pluginContext);
            plugin.Test();
        }
    }
}
