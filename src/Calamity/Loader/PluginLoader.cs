using Calamity.Core;
using Calamity.Logging;
using Calamity.TypeActivators;

using Microsoft.Extensions.Logging;

using System;
using System.Linq;
using System.Reflection;

namespace Calamity
{
    internal class PluginLoader<TPlugin> : IPluginLoader<TPlugin>
        where TPlugin : class
    {
        private readonly ILogger<PluginLoader<TPlugin>> _logger =
            LogProvider.Create<PluginLoader<TPlugin>>();

        private readonly PluginMetadata _pluginMetadata;

        internal PluginLoader(string assemblyPath)
        {
            _pluginMetadata = new PluginMetadata(
                assemblyPath);
        }

        public TPlugin Build()
        {
            return Build(PluginLoaderOptions.TypeActivator);
        }

        public TPlugin Build(ITypeActivator typeActivator)
        {
            if (typeActivator == null)
            {
                throw new ArgumentNullException(
                    nameof(typeActivator),
                    $"The '{nameof(typeActivator)}' parameter is required.");
            }

            if (!TryResolveTypeFromAssembly<TPlugin>(out var type))
            {
                throw new TypeLoadException(
                    $"Could not resolve a type which is assignable from {typeof(TPlugin)}.");
            }

            _logger.LogDebug($"Resolved type: {type} from assembly '{_pluginMetadata.Assembly}'.");

            _pluginMetadata.Type = type;

            var instance = typeActivator
                .CreateInstance<TPlugin>(
                    _pluginMetadata.Type,
                    _pluginMetadata.ConstructorParameters.ToArray());

            if (instance != null)
                _logger.LogDebug($"Created instance of type '{instance.GetType()}'.");

            return instance;
        }

        public IPluginLoader<TPlugin> AddConstructorParameters(
            params object[] parameters)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException(
                    nameof(parameters),
                    $"The parameter {nameof(parameters)} can't be null!");
            }

            _pluginMetadata.ConstructorParameters.AddRange(
                parameters);

            return this;
        }      

        private bool TryResolveTypeFromAssembly<T>(out Type type)
        {
            _pluginMetadata.Assembly = LoadAssembly();            

            type = _pluginMetadata.Assembly
                .GetTypes()
                .FirstOrDefault(assemblyType =>
                    !assemblyType.IsInterface &&
                        !assemblyType.IsAbstract &&
                        typeof(T).IsAssignableFrom(assemblyType));

            return type != null;
        }

        private Assembly LoadAssembly()
        {
            var assemblyLoadContext = new PluginLoadContext(
                _pluginMetadata.AssemblyPath);

            var assembly = assemblyLoadContext
                .LoadFromAssemblyPath(_pluginMetadata.AssemblyPath);

            return assembly;
        }
    }
}
