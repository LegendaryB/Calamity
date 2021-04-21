using System.Collections.Generic;
using System.Reflection;

namespace Calamity.Builder
{
    internal class PluginContext : IPluginContext
    {
        public Assembly Assembly { get; }

        public IReadOnlyList<object> ConstructorParameters { get; }

        internal PluginContext(
            Assembly assembly,
            List<object> constructorParameters)
        {
            Assembly = assembly;

            ConstructorParameters = constructorParameters
                .AsReadOnly();
        }
    }
}
