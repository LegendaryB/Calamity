using Calamity.Activation;

namespace Calamity
{
    public interface IPluginLoader<TPluginInterface>
        where TPluginInterface : class
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="activator">The <see cref="IActivator"/> to use to create the plugin instance.</param>
        /// <returns>A instance of the plugin.</returns>
        /// <exception cref="TypeLoadException">Thrown when type fails.</exception>
        /// <exception cref="InvalidOperationException">Thrown when instance creation fails.</exception>
        TPluginInterface LoadPlugin();

        /// <summary>
        /// 
        /// </summary>
        Task Unload();
    }
}
