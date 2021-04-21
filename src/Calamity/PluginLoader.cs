﻿using Calamity.Logging;

using Microsoft.Extensions.Logging;

using System;
using System.Linq;
using System.Reflection;

namespace Calamity
{
    public class PluginLoader
    {
        public static readonly PluginLoaderOptions Options =
            new PluginLoaderOptions();

        private readonly ILogger<PluginLoader> _logger =
            LogProvider.Create<PluginLoader>();

        public TPlugin Instantiate<TPlugin>(
            IPluginContext context)

            where TPlugin : class
        {
            try
            {
                _logger.Log(LogConstants.ENTER);

                return Instantiate(
                    context,
                    CreateInstance<TPlugin>);
            }
            finally
            {
                _logger.Log(LogConstants.LEAVE);
            }
        }

        public TPlugin Instantiate<TPlugin>(
            IPluginContext context,
            Func<Type, IPluginContext, TPlugin> factory)

            where TPlugin : class
        {
            try
            {
                _logger.Log(LogConstants.ENTER);

                if (context == null)
                    throw new ArgumentNullException(nameof(context));

                if (factory == null)
                    throw new ArgumentNullException(nameof(factory));

                if (!TryResolveTypeFromAssembly<TPlugin>(context.Assembly, out Type implementationType))
                    return null;

                return factory.Invoke(
                    implementationType,
                    context);
            }
            finally
            {
                _logger.Log(LogConstants.LEAVE);
            }
        }

        private TPlugin CreateInstance<TPlugin>(
            Type implementationType,
            IPluginContext definition)

            where TPlugin : class
        {
            try
            {
                _logger.Log(LogConstants.ENTER);

                return Activator.CreateInstance(
                    implementationType,
                    definition.ConstructorParameters.ToArray()) as TPlugin;
            }
            finally
            {
                _logger.Log(LogConstants.LEAVE);
            }
        }

        private bool TryResolveTypeFromAssembly<T>(
            Assembly assembly,
            out Type type)
        {
            try
            {
                _logger.Log(LogConstants.ENTER);

                type = assembly
                    .GetTypes()
                    .FirstOrDefault(type => 
                        !type.IsInterface &&
                        !type.IsAbstract &&
                        typeof(T).IsAssignableFrom(type));

                if (type != null)
                {
                    _logger.Log($"Resolved type: '{type.FullName}' from assembly '{assembly.FullName}'");
                }

                return type != null;
            }
            finally
            {
                _logger.Log(LogConstants.LEAVE);
            }
        }
    }
}