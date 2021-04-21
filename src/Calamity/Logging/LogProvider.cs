using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

using System;

namespace Calamity.Logging
{
    internal static class LogProvider
    {
        internal static ILogger<T> Create<T>()
        {
            return PluginLoader.Options.LoggerFactory == null ?
                NullLogger<T>.Instance :
                PluginLoader.Options.LoggerFactory.CreateLogger<T>();
        }

        internal static void Log(
            this ILogger logger,
            string message,
            LogLevel level = LogLevel.Debug,
            Exception exception = null)
        {
            logger.Log(level, exception, message);
        }
    }
}
