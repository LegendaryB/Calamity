using Calamity.Logging;

using Microsoft.Extensions.Logging;

using System;
using System.Reflection;
using System.Runtime.Loader;

namespace Calamity.Core
{
    internal class PluginLoadContext : AssemblyLoadContext
    {
        private readonly ILogger<PluginLoadContext> _logger =
            LogProvider.Create<PluginLoadContext>();

        private readonly AssemblyDependencyResolver _resolver;        

        internal PluginLoadContext(
            string assemblyPath)
        {
            _resolver = new AssemblyDependencyResolver(
                assemblyPath);
        }

        protected override Assembly Load(
            AssemblyName assemblyName)
        {
            _logger.Log($"Loading {assemblyName}");

            if (PluginLoaderOptions.PreferAssembliesFromHost)
            {
                try
                {
                    Default.LoadFromAssemblyName(assemblyName);
                    _logger.Log($"Loaded {assemblyName} from host application context.");

                    return null;
                }
                catch
                {
                    _logger.Log($"Failed to load {assemblyName} from host application context.");
                }
            }

            var assemblyPath = _resolver.ResolveAssemblyToPath(assemblyName);

            if (assemblyPath != null)
            {
                var assembly = LoadFromAssemblyPath(assemblyPath);
                _logger.Log($"Loaded {assemblyName} from {assemblyPath}");

                return assembly;
            }

            return null;
        }

        protected override IntPtr LoadUnmanagedDll(string unmanagedDllName)
        {
            var dllPath = _resolver
                .ResolveUnmanagedDllToPath(
                    unmanagedDllName);

            return string.IsNullOrWhiteSpace(dllPath) ? 
                LoadUnmanagedDllFromPath(dllPath) : 
                IntPtr.Zero;
        }
    }
}
