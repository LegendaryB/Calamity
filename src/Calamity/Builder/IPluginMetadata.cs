using System.Collections.Generic;
using System.Reflection;

namespace Calamity
{
    public interface IPluginMetadata
    {
        Assembly Assembly { get; }
        IReadOnlyList<object> ConstructorParameters { get; }
    }
}
