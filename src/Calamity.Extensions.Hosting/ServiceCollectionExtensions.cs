using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Calamity
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Registers the Calamity framework into the dependency container.
        /// </summary>
        /// <exception cref="ArgumentNullException">Thrown when the <paramref name="services"/> parameter is null.</exception>
        /// <exception cref="ArgumentNullException">Thrown when the <paramref name="configureCalamity"/> parameter is null.</exception>
        public static IServiceCollection AddCalamityFramework(
            this IServiceCollection services,
            Action<CalamityConfiguration> configureCalamity)
        {
            ArgumentNullException.ThrowIfNull(services);
            ArgumentNullException.ThrowIfNull(configureCalamity);

            return AddCalamityFramework(
                services,
                (serviceProvider, configuration) => {
                    configureCalamity(configuration);
                });
        }

        /// <summary>
        /// Registers the Calamity framework into the dependency container.
        /// </summary>
        /// <exception cref="ArgumentNullException">Thrown when the <paramref name="services"/> parameter is null.</exception>
        /// <exception cref="ArgumentNullException">Thrown when the <paramref name="configureCalamity"/> parameter is null.</exception>
        public static IServiceCollection AddCalamityFramework(
            this IServiceCollection services,
            Action<IServiceProvider, CalamityConfiguration> configureCalamity)
        {
            ArgumentNullException.ThrowIfNull(services);
            ArgumentNullException.ThrowIfNull(configureCalamity);

            services.AddSingleton<IPluginLoaderFactory>((serviceProvider) =>
            {
                var configuration = new CalamityConfiguration();
                configureCalamity(serviceProvider, configuration);

                var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
                var instance = new PluginLoaderFactory(loggerFactory, configuration);

                return instance;
            });

            return services;
        }
    }
}