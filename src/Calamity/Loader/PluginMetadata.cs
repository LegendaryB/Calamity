using System;
using System.Collections.Generic;
using System.Reflection;

namespace Calamity
{
    internal class PluginMetadata
    {
        internal string AssemblyPath { get; set; }

        internal Assembly Assembly { get; set; }

        internal Type  Type { get; set; }

        internal List<object> ConstructorParameters { get; }

        internal PluginMetadata(string assemblyPath)
        {
            AssemblyPath = assemblyPath;
            ConstructorParameters = new List<object>();
        }
    }
}
