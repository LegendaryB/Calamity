using System;

namespace Calamity.TypeActivators
{
    /// <summary>
    /// Uses the default <see cref="Activator"/> to create object instances of the framework.
    /// </summary>
    public class FrameworkActivator : ITypeActivator
    {
        public TInterface CreateInstance<TInterface>(Type implementationType) 
            where TInterface : class
        {
            return Activator.CreateInstance(implementationType) as TInterface;
        }

        public TInterface CreateInstance<TInterface>(Type implementationType, object[] args) 
            where TInterface : class
        {
            return Activator.CreateInstance(implementationType, args) as TInterface;
        }
    }
}
