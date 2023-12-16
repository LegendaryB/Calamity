using Calamity.Activation;

namespace Calamity.Attributes
{
    /// <summary>
    /// Attribute used to mark the constructor which should be used by the <see cref="IActivator"/> of the Calamity framework to create the instance.
    /// </summary>
    [AttributeUsage(AttributeTargets.Constructor, AllowMultiple = false, Inherited = false)]
    public class CalamityConstructorAttribute : Attribute
    {
    }
}
