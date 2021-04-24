namespace Calamity
{
    public interface IPluginBuilder
    {
        IPluginBuilder DefineConstructorParameters(
            params object[] parameters);

        IPluginMetadata Build();
    }
}
