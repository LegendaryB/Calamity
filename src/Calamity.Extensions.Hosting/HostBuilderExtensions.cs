using Calamity.Configuration;

using Microsoft.Extensions.Hosting;

namespace Calamity
{
    public static class HostBuilderExtensions
    {
        /// <summary>
        /// Registers the Calamity framework into the dependency container.
        /// </summary>
        /// <exception cref="ArgumentNullException">Thrown when the <see cref="IHostBuilder"/> parameter is null.</exception>
        public static IHostBuilder UseCalamityFramework(
            this IHostBuilder builder,
            Action<CalamityConfiguration> configureCalamity)
        {
            ArgumentNullException.ThrowIfNull(builder);
            ArgumentNullException.ThrowIfNull(configureCalamity);

            return builder.UseCalamityFramework(
                (serviceProvider, configuration) =>
                {
                    configureCalamity(configuration);
                });
        }

        /// <summary>
        /// Registers the Calamity framework into the dependency container.
        /// </summary>
        /// <exception cref="ArgumentNullException">Thrown when the <paramref name="services"/> parameter is null.</exception>
        /// <exception cref="ArgumentNullException">Thrown when the <paramref name="configureCalamity"/> parameter is null.</exception>
        public static IHostBuilder UseCalamityFramework(
            this IHostBuilder builder,
            Action<IServiceProvider, CalamityConfiguration> configureCalamity)
        {
            ArgumentNullException.ThrowIfNull(builder);
            ArgumentNullException.ThrowIfNull(configureCalamity);

            builder.ConfigureServices((context, services) =>
            {
                services.AddCalamityFramework(configureCalamity);
            });

            return builder;
        }
    }
}
