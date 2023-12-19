namespace Calamity.Activation
{
    public static class ActivatorCacheFactory
    {
        public static IActivatorCache Create<TCacheValue>()
            where TCacheValue : class
        {
            var cache = new ActivatorCache();

            return cache;
        }
    }
}
