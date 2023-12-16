namespace Calamity.Activation
{
    public interface IActivator
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TInterface"></typeparam>
        /// <param name="implementationType"></param>
        /// <returns></returns>
        TInterface CreateInstance<TInterface>(Type implementationType, object[] parameters)
            where TInterface : class;
    }
}
