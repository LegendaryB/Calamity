using System.Reflection;

namespace Calamity.Activation
{
    public interface IConstructorLocator
    {
        ConstructorInfo LocateApplicableConstructor(
            Type type,
            object[] args);
    }
}
