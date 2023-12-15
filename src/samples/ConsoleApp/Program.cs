using Calamity;

using ClassLibrary2;

using System.Threading.Tasks;

namespace ConsoleApp
{
    class Program
    {
        static async Task Main()
        {
            var path = @"C:\Users\danie\source\repos\Calamity\ClassLibrary1\bin\Debug\netstandard2.1\ClassLibrary1.dll";

            var factory = new PluginLoaderFactory();

            var loader = factory
                .CreateLoaderFor<ITestPlugin>(path);

            var instance = loader.LoadPlugin();
            instance.Test();

            await loader.Unload();
        }
    }
}
