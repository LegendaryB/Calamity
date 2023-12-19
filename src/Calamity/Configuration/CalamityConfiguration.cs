using Calamity.Activation;

namespace Calamity.Configuration
{
    public class CalamityConfiguration
    {
        public bool PreferAssembliesFromHost { get; private set; }

        public IActivator Activator { get; private set; }

        internal CalamityConfiguration()
        {
            Activator = new DefaultActivator();
        }

        public CalamityConfiguration ShouldPreferAssembliesFromHost(bool value)
        {
            PreferAssembliesFromHost = value;
            return this;
        }

        public CalamityConfiguration UseActivator(IActivator activator)
        {
            return UseActivator(activator, (activator) => {});
        }

        public CalamityConfiguration UseActivator(IActivator activator, Action<IActivator> configureActivator)
        {
            ArgumentNullException.ThrowIfNull(activator);
            ArgumentNullException.ThrowIfNull(configureActivator);

            Activator = activator;
            configureActivator(Activator);

            return this;
        }

        internal static CalamityConfiguration Default => new();
    }
}