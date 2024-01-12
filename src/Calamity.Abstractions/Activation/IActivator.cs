namespace Calamity.Activation
{
    public interface IActivator
    {
        /// <summary>
        /// The constructor locator used by this <see cref="IActivator"/> instance. If not set by the user the DefaultConstructorLocator is used."/>
        /// </summary>
        IConstructorLocator? ConstructorLocator { get; set; }

        /// <summary>
        /// Optional callback to decide if a certain type should be cached.
        /// </summary>
        Func<Type, bool>? ShouldTypeBeCachedCallback { get; set; }

        /// <summary>
        /// Tries to create a instance of the given <paramref name="type"/> and cast it to the <typeparamref name="TInterface"/>
        /// </summary>
        /// <typeparam name="TInterface"></typeparam>
        /// <param name="type">The type to instantiate. Must implement <typeparamref name="TInterface"/>.</param>
        /// <param name="args">The parameters used to instantiate the given type.</param>
        /// <returns> true if the instance was created successfully; otherwise, false.</returns>
        TInterface? CreateInstance<TInterface>(Type type, object[] args)
            where TInterface : class;
    }
}
