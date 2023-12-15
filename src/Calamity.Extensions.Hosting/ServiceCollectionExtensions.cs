using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Calamity
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Registers Calamity's <see cref="IPluginLoaderFactory"/> into the dependency container.
        /// </summary>
        /// <exception cref="ArgumentNullException">Thrown when the <see cref="IServiceCollection"/> parameter is null.</exception>
        public static IServiceCollection AddCalamity(this IServiceCollection services, Action<CalamityConfiguration> configureCalamity)
        {
            ArgumentNullException.ThrowIfNull(services);
            ArgumentNullException.ThrowIfNull(configureCalamity);

            return AddCalamity(
                services,
                (serviceProvider, configuration) => {
                    configureCalamity(configuration);
                });
        }

        public static IServiceCollection AddCalamity(this IServiceCollection services, Action<IServiceProvider, CalamityConfiguration> configureCalamity)
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