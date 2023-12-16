using Calamity.Activation;

using Microsoft.Extensions.DependencyInjection;

namespace Calamity.Extensions.Activation
{
    public class ServiceProviderBasedActivator : IActivator
    {
        private readonly IServiceProvider _serviceProvider;

        public ServiceProviderBasedActivator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public TInterface CreateInstance<TInterface>(Type implementationType, object[] parameters)
            where TInterface : class
        {
            //ConstructorLocator.TryFindConstructor(implementationType, out var ctor);

            //ActivatorUtilities.
            var instance = ActivatorUtilities.GetServiceOrCreateInstance(
                _serviceProvider,
                implementationType) as TInterface;

            return instance ?? throw new InvalidCastException($"Failed to cast created instance to type: {typeof(TInterface)}");
        }
    }
}
