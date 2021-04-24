using Calamity.Logging;

using Microsoft.Extensions.Logging;

using System;
using System.IO;

namespace Calamity
{
    public static class PluginLoaderFactory
    {
        private static readonly ILogger _logger =
            LogProvider.Create(nameof(PluginLoaderFactory));

        public static IPluginLoader<TPlugin> CreateLoaderFor<TPlugin>(string assemblyPath)
            where TPlugin : class
        {
            if (string.IsNullOrWhiteSpace(assemblyPath))
            {
                throw new ArgumentException(
                    $"The parameter '{nameof(assemblyPath)}' is required.");
            }

            if (!File.Exists(assemblyPath))
            {
                throw new FileNotFoundException(
                    "Could not find file at given location.",
                    assemblyPath);
            }

            var instance = new PluginLoader<TPlugin>(assemblyPath);

            _logger.Log($"Created plugin loader for type '{typeof(TPlugin)}' from assembly: '{assemblyPath}'.");

            return instance;
        }
    }
}
