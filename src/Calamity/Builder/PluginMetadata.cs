using System.Collections.Generic;
using System.Reflection;

namespace Calamity.Builder
{
    internal class PluginMetadata : IPluginMetadata
    {
        public Assembly Assembly { get; }

        public IReadOnlyList<object> ConstructorParameters { get; }

        internal PluginMetadata(
            Assembly assembly,
            List<object> constructorParameters)
        {
            Assembly = assembly;

            ConstructorParameters = constructorParameters
                .AsReadOnly();
        }
    }
}
