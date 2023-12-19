using Microsoft.Extensions.Logging;

using System.Reflection;
using System.Runtime.Loader;

namespace Calamity
{
    internal class PluginLoadContext : AssemblyLoadContext
    {
        internal string AssemblyPath { get; private set; }

        private readonly ILogger _logger;
        private readonly AssemblyDependencyResolver _resolver;

        private readonly bool _preferAssembliesFromHost;

        internal PluginLoadContext(ILogger logger, string assemblyPath, bool preferAssembliesFromHost = false)
            : base(isCollectible: true)
        {
            AssemblyPath = assemblyPath;

            _logger = logger;
            _resolver = new AssemblyDependencyResolver(assemblyPath);
            _preferAssembliesFromHost = preferAssembliesFromHost;
        }

        protected override Assembly Load(AssemblyName assemblyName)
        {
            try
            {
                _logger.LogTrace($"Starting to load assembly '{assemblyName}'...");

                var assembly = _preferAssembliesFromHost ?
                    LoadAssemblyFromHost(assemblyName) :
                    LoadAssembly(assemblyName);

                _logger.LogTrace($"Loaded assembly '{assemblyName}'.");

                return assembly;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to load assembly '{assemblyName}'.");
                throw;
            }
        }

        private Assembly LoadAssemblyFromHost(AssemblyName assemblyName)
        {
            try
            {
                var assembly = Default.LoadFromAssemblyName(assemblyName);
                _logger.LogTrace($"Loaded assembly '{assemblyName}' from host application context.");

                return assembly;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to load assembly '{assemblyName}' from host application context.");
                throw;
            }
        }

        private Assembly LoadAssembly(AssemblyName assemblyName)
        {
            try
            {
                var assemblyPath = _resolver.ResolveAssemblyToPath(assemblyName);

                if (string.IsNullOrWhiteSpace(assemblyPath))
                {
                    throw new FileNotFoundException($"Failed to resolve path for assembly '{assemblyName.Name}'.");
                }

                var assembly = LoadFromAssemblyPath(assemblyPath);
                _logger.LogDebug($"Loaded assembly '{assemblyName}' from path: {assemblyPath}");

                return assembly;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to load assembly '{assemblyName}' from disk.");
                throw;
            }
        }

        protected override IntPtr LoadUnmanagedDll(string unmanagedDllName)
        {
            var libraryPath = _resolver.ResolveUnmanagedDllToPath(unmanagedDllName);

            if (libraryPath != null)
            {
                return LoadUnmanagedDllFromPath(libraryPath);
            }

            return IntPtr.Zero;
        }
    }
}
