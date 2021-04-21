using Microsoft.Extensions.Logging;

namespace Calamity
{
    public class PluginLoaderOptions
    {
        private static PluginLoaderOptions _options;

        public static PluginLoaderOptions Instance
        {
            get
            {
                if (_options == null)
                    _options = new PluginLoaderOptions();

                return _options;
            }
        }

        /// <summary>
        /// Factory to provide a logger for the internal library types.
        /// </summary>
        public ILoggerFactory LoggerFactory { get; set; }

        /// <summary>
        /// Flag to indicate if the plugin should use 
        /// </summary>
        public bool PreferAssembliesFromHost { get; set; }

        private PluginLoaderOptions()
        {
        }
    }
}
