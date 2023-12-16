namespace Calamity.Activation
{
    internal class DefaultActivator : IActivator
    {
        public TInterface CreateInstance<TInterface>(Type implementationType, object[] parameters)
            where TInterface : class
        {
            var instance = Activator.CreateInstance(implementationType) as TInterface;



            return instance ?? throw new InvalidCastException($"Failed to cast created instance to type: {typeof(TInterface)}");
        }
    }
}
