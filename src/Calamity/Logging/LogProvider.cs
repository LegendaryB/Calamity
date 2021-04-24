using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

using System;

namespace Calamity.Logging
{
    internal static class LogProvider
    {
        internal static ILogger<T> Create<T>()
        {
            return PluginLoaderOptions.LoggerFactory == null ?
                NullLogger<T>.Instance :
                PluginLoaderOptions.LoggerFactory.CreateLogger<T>();
        }

        internal static ILogger Create(string category)
        {
            return PluginLoaderOptions.LoggerFactory == null ?
                NullLogger.Instance :
                PluginLoaderOptions.LoggerFactory.CreateLogger(category);
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
