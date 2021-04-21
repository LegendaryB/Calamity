using Calamity.Builder;

using System;

namespace Calamity
{
    public static class PluginBuilderFactory
    {
        public static IPluginBuilder CreateFromAssembly(string assemblyPath)
        {
            if (string.IsNullOrWhiteSpace(assemblyPath))
            {
                throw new ArgumentException(
                    $"The parameter '{assemblyPath}' can't be null or empty!");
            }

            var builder = new PluginBuilder()
                .DefineAssembly(assemblyPath);

            return builder;
        }
    }
}
