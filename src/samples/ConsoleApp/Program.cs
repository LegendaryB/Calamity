using Calamity;

using ClassLibrary2;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ConsoleApp
{
    class Program
    {
        static void Main()
        {
            var serviceProvider = new ServiceCollection()
                .AddLogging(cfg => 
                    cfg.AddConsole()).Configure<LoggerFilterOptions>(cfg => cfg.MinLevel = LogLevel.Debug).BuildServiceProvider();

            PluginLoaderOptions.LoggerFactory = serviceProvider.GetService<ILoggerFactory>();

            var path = @"C:\Users\danie\source\repos\Calamity\ClassLibrary1\bin\Debug\netstandard2.1\ClassLibrary1.dll";

            var instance = PluginLoaderFactory
                .CreateLoaderFor<ITestPlugin>(path)
                .Build();
        }
    }
}
