using System.Reflection;

namespace Calamity.Activation
{
    public class ActivatorBase : IActivator
    {
        /// <inheritdoc/>
        public IConstructorLocator? ConstructorLocator { get; set; }

        /// <inheritdoc/>
        public Func<Type, bool>? ShouldTypeBeCachedCallback { get; set; }

        /// <inheritdoc/>

        protected ActivatorBase() { }

        /// <summary>
        /// This method is only responsible for parameter checking and does <b>NOT</b> create a instance of the <paramref name="type"/>.
        /// </summary>
        /// <returns>Null</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="type"/> or <paramref name="args"/> is null.</exception>
        /// <exception cref="InvalidOperationException">Thrown when the <paramref name="type"/> is a abstract or interface type.</exception>
        public virtual TInterface? CreateInstance<TInterface>(
            Type type,
            object[] args)

            where TInterface : class
        {
            ArgumentNullException.ThrowIfNull(type);
            ArgumentNullException.ThrowIfNull(args);

            if (type.IsAbstract)
            {
                throw new InvalidOperationException($"The type '{type}' is marked with the abstract keyword; therefor it can't be instantiated.");
            }

            if (type.IsInterface)
            {
                throw new InvalidOperationException($"The type '{type}' is marked with the interface keyword; therefor it can't be instantiated.");
            }

            return null;
        }

        protected TInterface? Instantiate<TInterface>(
            ConstructorInfo constructor,
            object[] args)

            where TInterface : class
        {
            return constructor.Invoke(args) as TInterface;
        }
    }
}
