namespace Calamity.Activation
{
    public enum CacheStrategy
    {
        /// <summary>
        /// Nothing is cached.
        /// </summary>
        None,
        Classic,
        Expression
    }

    public interface IActivator
    {
        /// <summary>
        /// Flag to control if this <see cref="IActivator"/> instance should use caching internally or not.
        /// </summary>
        bool UseCache { get; set; }

        //CacheStrategy CacheStrategy { get; set; }

        /// <summary>
        /// Optional callback to decide if a certain type should be cached.
        /// </summary>
        Func<Type, bool>? ShouldTypeBeCachedCallback { get; set; }

        //Func<Type, bool>? TypeCacheStrategySelector { get; set; }

        /// <summary>
        /// Tries to create a instance of the given <paramref name="implementationType"/> and cast it to the <typeparamref name="TInterface"/>
        /// </summary>
        /// <typeparam name="TInterface"></typeparam>
        /// <param name="implementationType">The type to instantiate. Must implement <typeparamref name="TInterface"/>.</param>
        /// <param name="parameters">The parameters used to instantiate the given type.</param>
        /// <returns> true if the instance was created successfully; otherwise, false.</returns>
        TInterface CreateInstance<TInterface>(Type implementationType, object[] parameters)
            where TInterface : class;
    }
}
