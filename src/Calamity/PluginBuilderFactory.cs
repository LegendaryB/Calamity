using Calamity.Builder;
using Calamity.Logging;

using Microsoft.Extensions.Logging;

using System;
using System.IO;

namespace Calamity
{
    public static class PluginBuilderFactory
    {
        private static readonly ILogger _logger =
            LogProvider.Create(nameof(PluginBuilderFactory));

        public static IPluginBuilder CreateFromAssembly(string assemblyPath)
        {
            if (string.IsNullOrWhiteSpace(assemblyPath))
            {
                throw new ArgumentException(
                    $"Parameter '{assemblyPath}' can't be null or empty!");
            }

            if (!File.Exists(assemblyPath))
            {
                throw new FileNotFoundException(
                    $"Lookup of assembly at path: '{assemblyPath}' failed.");
            }

            var builder = new PluginBuilder()
                .DefineAssembly(assemblyPath);

            _logger.Log($"Created plugin builder for assembly: '{assemblyPath}'");

            return builder;
        }
    }
}
