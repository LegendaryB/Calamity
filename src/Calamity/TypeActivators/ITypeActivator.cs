using System;

namespace Calamity.TypeActivators
{
    public interface ITypeActivator
    {
        TInterface CreateInstance<TInterface>(Type implementationType, object[] args)
            where TInterface : class;
    }
}
