using Calamity.Activation;

using Activator = Calamity.Activation.Activator;

namespace Calamity.Configuration
{
    public class CalamityConfiguration
    {
        public IActivator Activator { get; private set; }

        public bool PreferAssembliesFromHost { get; private set; }


        internal CalamityConfiguration() 
        {
            Activator = new Activator
            {
                ShouldTypeBeCachedCallback = (_) => true,
                ConstructorLocator = new ConstructorLocator()
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configureActivator"></param>
        /// <returns></returns>
        public CalamityConfiguration UseDefaultActivator(Action<IActivator> configureActivator)
        {
            return UseActivator(Activator, configureActivator);
        }

        /// <summary>
        /// Responsible for setting the <see cref="IActivator"/>
        /// </summary>
        /// <param name="activator"></param>
        /// <returns></returns>
        public CalamityConfiguration UseActivator(IActivator activator)
        {
            return UseActivator(activator, (activator) => {});
        }

        /// <summary>
        /// sss
        /// </summary>
        /// <param name="activator"></param>
        /// <param name="configureActivator"></param>
        /// <returns></returns>
        public CalamityConfiguration UseActivator(
            IActivator activator,
            Action<IActivator> configureActivator)
        {
            ArgumentNullException.ThrowIfNull(activator);
            ArgumentNullException.ThrowIfNull(configureActivator);

            Activator = activator;
            configureActivator(Activator);

            Activator.ConstructorLocator = Activator.ConstructorLocator ?? new ConstructorLocator();
            Activator.ShouldTypeBeCachedCallback = Activator.ShouldTypeBeCachedCallback ?? new Func<Type, bool>((type) => true);

            return this;
        }

        public CalamityConfiguration ShouldPreferAssembliesFromHost(bool value)
        {
            PreferAssembliesFromHost = value;
            return this;
        }
    }
}