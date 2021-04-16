using System;
using System.Reflection;
using System.Runtime.Loader;

namespace Calamity.Core
{
    internal class PluginAssemblyLoadContext : AssemblyLoadContext
    {
        private readonly AssemblyDependencyResolver _resolver;

        internal PluginAssemblyLoadContext(
            string assemblyPath)
        {
            _resolver = new AssemblyDependencyResolver(
                assemblyPath);
        }

        protected override Assembly Load(AssemblyName assemblyName)
        {
            var assemblyPath = _resolver
                .ResolveAssemblyToPath(
                    assemblyName);

            if (!string.IsNullOrWhiteSpace(assemblyPath))
            {
                return LoadFromAssemblyPath(assemblyPath);
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
