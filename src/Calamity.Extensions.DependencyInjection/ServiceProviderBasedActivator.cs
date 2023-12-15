using Calamity.Activation;

using Microsoft.Extensions.DependencyInjection;

namespace Calamity.Extensions.DependencyInjection
{
    public class ServiceProviderBasedActivator : IActivator
    {
        private readonly IServiceProvider _serviceProvider;

        public ServiceProviderBasedActivator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public TInterface CreateInstance<TInterface>(Type implementationType)
            where TInterface : class
        {
            ConstructorLocator.TryFindConstructor(implementationType, out var ctor);

            var instance = ActivatorUtilities.GetServiceOrCreateInstance(
                _serviceProvider,
                implementationType) as TInterface;

            return instance ?? throw new InvalidCastException($"Failed to cast created instance to type: {typeof(TInterface)}");
        }
    }
}
