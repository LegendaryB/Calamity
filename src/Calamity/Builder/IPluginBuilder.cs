namespace Calamity
{
    public interface IPluginBuilder
    {
        IPluginBuilder DefineConstructorParameters(
            params object[] parameters);

        IPluginContext Build();
    }
}
