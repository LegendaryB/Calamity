using System.Collections.Generic;
using System.Reflection;

namespace Calamity
{
    public interface IPluginContext
    {
        Assembly Assembly { get; }
        IReadOnlyList<object> ConstructorParameters { get; }
    }
}
