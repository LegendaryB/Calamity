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
            var assemblyPath = _resolver.ResolveAssemblyToPath(
                assemblyName);

            Default.LoadFromAssemblyName(assemblyName);

            //if (!string.IsNullOrWhiteSpace(assemblyPath))
            //{
            //    return LoadFromAssemblyPath(assemblyPath);
            //}

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
