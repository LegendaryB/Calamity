using Calamity.Attributes;

using System.Reflection;

namespace Calamity.Activation
{
    internal static class ConstructorLocator
    {
        internal static bool TryFindConstructor(
            Type type,
            object[] parameters,
            out ConstructorInfo? constructor)
        {
            constructor = null;

            var parameterTypes = parameters
                .Select(param => param.GetType());

            var constructors = type.GetConstructors();

            // try locate constructor decorated with the custom marker attribute
            foreach (var ctor in constructors)
            {
                if (!ctor.IsDefined(typeof(CalamityConstructorAttribute), false))
                    continue;

                constructor = ctor;
                break;
            }

            // try locate empty constructor
            if (constructor == null)
            {
                constructor = constructors
                    .FirstOrDefault(ctor => ctor.GetParameters().Length == 0);
            }

            return constructor != null;
        }
    }
}
