using Calamity.Builder;
using Calamity.Logging;

using Microsoft.Extensions.Logging;

using System;

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
                    $"The parameter '{assemblyPath}' can't be null or empty!");
            }

            var builder = new PluginBuilder()
                .DefineAssembly(assemblyPath);

            _logger.Log($"Created {nameof(IPluginBuilder)} instance for assembly in path: {assemblyPath}");

            return builder;
        }
    }
}
