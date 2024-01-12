using System.Reflection;

namespace Calamity.Activation
{
    internal sealed class ConstructorLocator : IConstructorLocator
    {
        public ConstructorInfo LocateApplicableConstructor(Type type, object[] args)
        {
            ArgumentNullException.ThrowIfNull(type);
            ArgumentNullException.ThrowIfNull(args);

            if (!TryGetMatchingConstructor(type, args, out var ctor))
                throw new InvalidOperationException($"Failed to locate constructor whose parameters match the types specified in the '{nameof(args)}' array.");

            return ctor!;
        }

        private static bool TryGetMatchingConstructor(Type type, object[] args, out ConstructorInfo? ctor)
        {
            var typeArray = Type.GetTypeArray(args);
            ctor = type.GetConstructor(typeArray);

            return ctor != null;
        }
    }
}
