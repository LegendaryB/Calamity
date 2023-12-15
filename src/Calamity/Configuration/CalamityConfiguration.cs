using Calamity.Activation;

namespace Calamity
{
    public class CalamityConfiguration
    {
        internal IActivator Activator { get; private set; }
        internal bool PreferAssembliesFromHost { get; private set; }

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
            ArgumentNullException.ThrowIfNull(activator);

            Activator = activator;
            return this;
        }

        internal static CalamityConfiguration Default => new();
    }
}