using Calamity.Attributes;

using System.Reflection;

namespace Calamity.Activation
{
    internal static class ConstructorLocator
    {
        /// <summary>
        /// Tries to find a applicable constructor which will be used by the current <see cref="IActivator"/> to create the instance. <br /> <br />
        /// Lookup order: <br />
        ///     - Looking up a constructor decorated with the <see cref="CalamityConstructorAttribute"/>. <br />
        ///     - Looking up a constructor where all parameter types are matching. <br />
        ///     - Looking up a empty constructor.
        /// </summary>
        internal static ConstructorInfo? LocateApplicableConstructor(Type type, object[] parameters)
        {
            var constructors = type.GetConstructors();

            if (TryLocatePreferredConstructor(
                constructors,
                parameters,
                out var constructor))
            {
                return constructor!;
            }

            if (TryLocateMatchingConstructor(
                constructors,
                parameters,
                out constructor))
            {
                return constructor!;
            }

            if (TryLocateEmptyConstructor(
                constructors,
                out constructor))
            {
                return constructor!;
            }

            return null;
        }

        /// <summary>
        /// Tries to find a constructor where the <see cref="CalamityConstructorAttribute"/> is applied and all parameters match (order, type etc.).
        /// </summary>
        private static bool TryLocatePreferredConstructor(
            ConstructorInfo[] constructors,
            object[] parameters,
            out ConstructorInfo? constructor)
        {
            constructor = constructors
                .Where(constructorCandidate => constructorCandidate.IsDefined(typeof(CalamityConstructorAttribute), false))
                .FirstOrDefault(constructorCandidate =>
                {
                    var ctorParameters = constructorCandidate.GetParameters();
                    return AreParametersMatching(ctorParameters, parameters);
                });

            return constructor != null;
        }

        /// <summary>
        /// Tries to find a constructor where all parameters match (order, type etc.).
        /// </summary>
        private static bool TryLocateMatchingConstructor(
            ConstructorInfo[] constructors,
            object[] parameters,
            out ConstructorInfo? constructor)
        {
            constructor = constructors
                .FirstOrDefault(constructorCandidate =>
                {
                    var ctorParameters = constructorCandidate.GetParameters();
                    return AreParametersMatching(ctorParameters, parameters);
                });

            return constructor != null;
        }

        private static bool TryLocateEmptyConstructor(
            ConstructorInfo[] constructors,
            out ConstructorInfo? constructor)
        {
            constructor = constructors
                    .FirstOrDefault(ctor => ctor.GetParameters().Length == 0);

            return constructor != null;
        }

        private static bool AreParametersMatching(
            ParameterInfo[] parameterInfoArray,
            object[] parameters)
        {
            if (parameterInfoArray.Length != parameters.Length)
                return false;

            for (int i = 0; i < parameters.Length; i++)
            {
                var expectedParameterType = parameterInfoArray[i].ParameterType;
                var actualParameterType = parameters[i].GetType();

                if (!actualParameterType.Equals(expectedParameterType))
                    return false;
            }

            return true;
        }
    }
}
