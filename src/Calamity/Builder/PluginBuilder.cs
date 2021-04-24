using Calamity.Core;
using Calamity.Logging;

using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Reflection;

namespace Calamity.Builder
{
    public class PluginBuilder : IPluginBuilder
    {
        private readonly ILogger<PluginBuilder> _logger =
            LogProvider.Create<PluginBuilder>();

        private string _assemblyPath;

        private readonly List<object> _constructorParameters =
            new List<object>();

        public IPluginMetadata Build()
        {
            var assembly = LoadAssembly();

            return new PluginMetadata(
                assembly,
                _constructorParameters);
        }

        public IPluginBuilder DefineConstructorParameters(
            params object[] parameters)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException(
                    nameof(parameters),
                    $"The parameter {nameof(parameters)} can't be null!");
            }

            _constructorParameters.AddRange(
                parameters);

            _logger.Log($"Added {_constructorParameters.Count} constructor parameters.");

            return this;
        }

        internal PluginBuilder DefineAssembly(string assemblyPath)
        {
            _assemblyPath = assemblyPath;

            _logger.Log($"Assembly path parameter was set to: '{assemblyPath}'");

            return this;
        }

        private Assembly LoadAssembly()
        {
            var assemblyLoadContext = new PluginLoadContext(_assemblyPath);

            var assembly = assemblyLoadContext
                .LoadFromAssemblyPath(_assemblyPath);

            _logger.Log($"Loaded assembly: '{assembly.FullName}'");

            return assembly;
        }
    }
}
