using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

using System;

namespace Calamity.Logging
{
    internal static class LogProvider
    {
        internal static ILogger<T> Create<T>()
        {
            return PluginLoaderOptions.Instance.LoggerFactory == null ?
                NullLogger<T>.Instance :
                PluginLoaderOptions.Instance.LoggerFactory.CreateLogger<T>();
        }

        internal static ILogger Create(string category)
        {
            return PluginLoaderOptions.Instance.LoggerFactory == null ?
                NullLogger.Instance :
                PluginLoaderOptions.Instance.LoggerFactory.CreateLogger(category);
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
