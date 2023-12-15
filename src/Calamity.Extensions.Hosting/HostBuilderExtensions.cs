using Microsoft.Extensions.Hosting;

namespace Calamity
{
    public static class HostBuilderExtensions
    {
        /// <summary>
        /// Registers Calamity's <see cref="IPluginLoaderFactory"/> into the dependency container.
        /// </summary>
        /// <exception cref="ArgumentNullException">Thrown when the <see cref="IHostBuilder"/> parameter is null.</exception>
        public static IHostBuilder UseCalamity(this IHostBuilder builder)
        {
            ArgumentNullException.ThrowIfNull(builder);

            //builder.ConfigureServices((ctx, services) =>
            //{
            //    services.AddCalamity();
            //});

            return builder;
        }
    }
}
