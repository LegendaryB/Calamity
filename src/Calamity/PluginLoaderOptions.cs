using Microsoft.Extensions.Logging;

namespace Calamity
{
    public class PluginLoaderOptions
    {
        /// <summary>
        /// Factory to provide a logger for the internal library types.
        /// </summary>
        public ILoggerFactory LoggerFactory { get; set; }



        /// <summary>
        /// Flag to indicate if the plugin should use 
        /// </summary>
        public bool PreferAssembliesFromHost { get; set; }
    }
}
