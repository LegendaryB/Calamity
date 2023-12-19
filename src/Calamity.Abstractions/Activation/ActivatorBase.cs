using System.Reflection;

namespace Calamity.Activation
{
    public class ActivatorBase : IActivator
    {
        /// <inheritdoc/>
        public bool UseCache { get; set; }

        /// <inheritdoc/>
        public Func<Type, bool>? ShouldTypeBeCachedCallback { get; set; }

        /// <inheritdoc/>

        protected ActivatorBase() { }

        /// <summary>
        /// This method is only responsible for parameter checking and does <b>NOT</b> create a instance of the <paramref name="implementationType"/>.
        /// </summary>
        /// <returns>Null</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="implementationType"/> or <paramref name="parameters"/> is null.</exception>
        /// <exception cref="InvalidOperationException">Thrown when the <paramref name="implementationType"/> is a abstract or interface type.</exception>
        public virtual TInterface CreateInstance<TInterface>(
            Type implementationType,
            object[] parameters)

            where TInterface : class
        {
            ArgumentNullException.ThrowIfNull(implementationType);
            ArgumentNullException.ThrowIfNull(parameters);

            if (implementationType.IsAbstract)
            {
                throw new InvalidOperationException($"The type '{implementationType}' is marked with the abstract keyword; therefor it can't be instantiated.");
            }

            if (implementationType.IsInterface)
            {
                throw new InvalidOperationException($"The type '{implementationType}' is marked with the interface keyword; therefor it can't be instantiated.");
            }

            return null!;
        }

        protected TInterface? Instantiate<TInterface>(
            ConstructorInfo constructor,
            object[] parameters)

            where TInterface : class
        {
            return constructor.Invoke(parameters) as TInterface;
        }
    }
}
