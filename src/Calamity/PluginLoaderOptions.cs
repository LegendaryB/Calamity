using Calamity.TypeActivators;

using Microsoft.Extensions.Logging;

namespace Calamity
{
    public class PluginLoaderOptions
    {
        /// <summary>
        /// Factory to provide a logger for the internal library types.
        /// </summary>
        public static ILoggerFactory LoggerFactory { get; set; }

        /// <summary>
        /// The default <see cref="ITypeActivator"/> which is used to create object instances. 
        /// Default is <see cref="FrameworkActivator"/>.
        /// </summary>
        public static ITypeActivator TypeActivator { get; set; } = new FrameworkActivator();

        /// <summary>
        /// Flag to indicate if the plugin should prefer assemblies from the host.
        /// Default is <see cref=""/>
        /// </summary>
        public static bool PreferAssembliesFromHost { get; set; } = true;
    }
}
