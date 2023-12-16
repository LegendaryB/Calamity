using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace Calamity
{
    public class PluginLoaderFactory : IPluginLoaderFactory
    {
        private readonly ILoggerFactory _loggerFactory;
        private readonly CalamityConfiguration _configuration;

        private readonly ILogger<PluginLoaderFactory> _logger;

        public PluginLoaderFactory(
            ILoggerFactory? loggerFactory = null,
            CalamityConfiguration? configuration = null)
        {
            _loggerFactory = loggerFactory ?? new NullLoggerFactory();
            _configuration = configuration ?? CalamityConfiguration.Default;

            _logger = _loggerFactory.CreateLogger<PluginLoaderFactory>();
        }

        /// <summary>
        /// Creates a loader instance for the given plugin type.
        /// </summary>
        /// <typeparam name="TPluginInterface"></typeparam>
        /// <param name="assemblyPath"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="FileNotFoundException"></exception>
        public IPluginLoader<TPluginInterface> CreateLoaderFor<TPluginInterface>(string assemblyPath)
            where TPluginInterface : class
        {
            if (string.IsNullOrWhiteSpace(assemblyPath))
            {
                var msg = $"The parameter '{nameof(assemblyPath)}' is required and therefor can't be empty/whitespace or null.";

                _logger.LogError(msg);
                throw new ArgumentException(msg);
            }

            if (!File.Exists(assemblyPath))
            {
                var msg = $"Could not find assembly at path: {assemblyPath}.";

                _logger.LogError(msg);
                throw new FileNotFoundException(
                    msg,
                    assemblyPath);
            }

            return new PluginLoader<TPluginInterface>(
                _loggerFactory.CreateLogger<PluginLoader<TPluginInterface>>(),
                assemblyPath,
                _configuration);
        }
    }
}
