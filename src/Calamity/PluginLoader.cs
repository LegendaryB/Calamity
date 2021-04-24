using Calamity.Logging;

using Microsoft.Extensions.Logging;

using System;
using System.Linq;
using System.Reflection;

namespace Calamity
{
    public class PluginLoader
    {
        private readonly ILogger<PluginLoader> _logger =
            LogProvider.Create<PluginLoader>();

        public TPlugin Create<TPlugin>(
            IPluginMetadata context)

            where TPlugin : class
        {
            return Create(
                context,
                CreateInstance<TPlugin>);
        }



        public TPlugin Create<TPlugin>(
            IPluginMetadata context,
            Func<Type, IPluginMetadata, TPlugin> factory)

            where TPlugin : class
        {
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

        private TPlugin CreateInstance<TPlugin>(
            Type implementationType,
            IPluginMetadata definition)

            where TPlugin : class
        {
            var instance = Activator.CreateInstance(
                implementationType,
                definition.ConstructorParameters.ToArray()) as TPlugin;

            _logger.Log($"Created instance of type '{implementationType}'");

            return instance;
        }

        private bool TryResolveTypeFromAssembly<T>(
            Assembly assembly,
            out Type type)
        {
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
            else
            {
                _logger.Log($"Unable to resolve type");
            }

            return type != null;
        }
    }
}
