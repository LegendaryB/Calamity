namespace Calamity
{
    public interface IPluginLoaderFactory
    {
        /// <summary>
        /// Creates a loader instance for the given plugin type.
        /// </summary>
        /// <typeparam name="TPluginInterface">The given plugin type.</typeparam>
        /// <param name="assemblyPath">The path to the assembly which contains a type which implements the given plugin type."/></param>
        /// <returns>A <see cref="IPluginLoader{TPluginInterface}>"/> instance.</returns>
        IPluginLoader<TPluginInterface> CreateLoaderFor<TPluginInterface>(string assemblyPath)
            where TPluginInterface : class;
    }
}
