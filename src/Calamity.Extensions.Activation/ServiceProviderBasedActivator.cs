using Calamity.Activation;

namespace Calamity.Extensions.Activation
{
    public class ServiceProviderBasedActivator : ActivatorBase
    {
        private readonly IServiceProvider _serviceProvider;

        public ServiceProviderBasedActivator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public override TInterface CreateInstance<TInterface>(
            Type implementationType,
            object[] parameters)
        {
            _ = base.CreateInstance<TInterface>(implementationType, parameters);

            //ActivatorUtilities

            //ActivatorUtilities.CreateFactory

            //ActivatorUtilities.CreateFactory

            return default!;
        }

        //public TInterface CreateInstance<TInterface>(Type implementationType, object[] parameters)
        //    where TInterface : class
        //{
        //    //ConstructorLocator.TryFindConstructor(implementationType, out var ctor);

        //    //ActivatorUtilities.
        //    var instance = ActivatorUtilities.GetServiceOrCreateInstance(
        //        _serviceProvider,
        //        implementationType) as TInterface;

        //    return instance ?? throw new InvalidCastException($"Failed to cast created instance to type: {typeof(TInterface)}");
        //}
    }
}
