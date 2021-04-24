using Calamity.TypeActivators;

namespace Calamity
{
    public interface IPluginLoader<TPlugin>
        where TPlugin : class
    {
        IPluginLoader<TPlugin> AddConstructorParameters(
            params object[] parameters);

        TPlugin Build();
        TPlugin Build(ITypeActivator typeActivator);
    }
}
