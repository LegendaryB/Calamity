namespace Calamity.Activation
{
    internal class DefaultActivator : ActivatorBase
    {
        public override TInterface CreateInstance<TInterface>(
            Type implementationType,
            object[] parameters)

            where TInterface : class
        {
            return default;
            //_ = base.CreateInstance<TInterface>(implementationType, parameters);

            //if (!Cache.TryGetValue(implementationType, out var constructor))
            //{
            //    constructor = ConstructorLocator.LocateApplicableConstructor(implementationType, parameters);

            //    if (constructor == null)
            //        throw new InvalidOperationException("Failed to locate applicable constructor!");
            //}

            //Cache.TryAdd(implementationType, constructor);

            //return Instantiate<TInterface>(constructor, parameters)!;
        }
    }
}
