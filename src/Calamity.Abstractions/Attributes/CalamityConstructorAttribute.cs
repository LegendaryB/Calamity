using Calamity.Activation;

namespace Calamity.Attributes
{
    /// <summary>
    /// Attribute used to mark the constructor which should be used by the <see cref="IActivator"/> to create a instance of this type.
    /// </summary>
    [AttributeUsage(AttributeTargets.Constructor, AllowMultiple = false, Inherited = false)]
    public class CalamityConstructorAttribute : Attribute
    {
    }
}
