using Calamity.Attributes;

using System.Reflection;

namespace Calamity.Activation
{
    internal static class ConstructorLocator
    {
        internal static bool TryFindConstructor(Type type, out ConstructorInfo? constructor)
        {
            constructor = null;
            var constructors = type.GetConstructors();

            foreach (var ctor in constructors)
            {
                if (!ctor.IsDefined(typeof(CalamityConstructorAttribute), false))
                    continue;

                constructor = ctor;
                break;
            }

            return constructor != null;
        }
    }
}
