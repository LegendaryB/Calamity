using Calamity.Configuration;

using Microsoft.Extensions.Logging;

namespace Calamity
{
    internal class PluginLoader<TPluginInterface> : IPluginLoader<TPluginInterface>
        where TPluginInterface : class
    {
        private readonly ILogger<PluginLoader<TPluginInterface>> _logger;
        private readonly CalamityConfiguration _configuration;
        private readonly PluginLoadContext _context;

        private TPluginInterface? _pluginInstance;

        internal PluginLoader(
            ILogger<PluginLoader<TPluginInterface>> logger,
            string assemblyPath,
            CalamityConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;

            _context = new PluginLoadContext(
                _logger,
                assemblyPath,
                _configuration.PreferAssembliesFromHost);
        }

        public TPluginInterface LoadPlugin(params object[] parameters)
        {
            try
            {

                if (_pluginInstance != null)
                {
                    _logger.LogTrace("A plugin instance was already activated. Returning it.");
                    return _pluginInstance;
                }

                var activator = _configuration.Activator;
                var interfaceType = typeof(TPluginInterface);

                if (!TryResolvePluginTypeFromAssembly(out var implementationType))
                {
                    var msg = $"Failed to resolve a type which implements the plugins interface type '{interfaceType}' from the assembly at path: {_context.AssemblyPath}.";

                    _logger.LogError(msg);
                    throw new TypeLoadException(msg);
                }

                _logger.LogTrace($"Resolved type '{implementationType}' as the implementation type for the given plugin interface '{interfaceType}' from the assembly at path: {_context.AssemblyPath}.");

                var instance = activator.CreateInstance<TPluginInterface>(implementationType!, parameters);

                if (instance == null)
                {
                    var msg = $"Failed to create a instance of the implementation type '{implementationType}'.";

                    _logger.LogError(msg);
                    throw new InvalidOperationException(msg);
                }

                _logger.LogTrace($"Created instance of the implementation type '{implementationType}'.");

                _pluginInstance = instance;

                return instance!;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "");
                throw;
            }
        }

        public async Task Unload()
        {
            if (_pluginInstance == null)
                return;

            if (_pluginInstance is IDisposable disposable)
                disposable.Dispose();

            if (_pluginInstance is IAsyncDisposable asyncDisposable)
                await asyncDisposable.DisposeAsync();

            _pluginInstance = null;
            _context.Unload();
        }

        private bool TryResolvePluginTypeFromAssembly(out Type? implementationType)
        {
            implementationType = null;

            try
            {
                var assembly = _context.LoadFromAssemblyPath(_context.AssemblyPath);

                if (assembly == null)
                    return false;

                implementationType = assembly
                    .GetTypes()
                    .FirstOrDefault(assemblyType =>
                        !assemblyType.IsInterface &&
                        !assemblyType.IsAbstract &&
                        typeof(TPluginInterface).IsAssignableFrom(assemblyType));

                return implementationType != null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "");
                return false;
            }
        }
    }
}
