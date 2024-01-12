using Microsoft.Extensions.DependencyInjection;

using System.Collections.Concurrent;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Calamity.Activation
{
    internal sealed class Activator : ActivatorBase
    {
        private delegate object TypeActivationDelegate(params object[] args);

        private readonly ConcurrentDictionary<Type, TypeActivationDelegate> _cache = new();
        private readonly Lazy<ConditionalWeakTable<Type, TypeActivationDelegate>> _collectibleCache = new();

        public override TInterface? CreateInstance<TInterface>(
            Type type,
            object[] args)

            where TInterface : class
        {
            _ = base.CreateInstance<TInterface>(type, args);

            var ctor = ConstructorLocator!.LocateApplicableConstructor(type, args);

            if (ShouldTypeBeCachedCallback?.Invoke(type) == null || false)
                return Instantiate<TInterface>(ctor, args);

            var typeActivationDelegate = GetOrAddTypeActivationDelegate(type, ctor) ??
                throw new InvalidOperationException("");

            //ActivatorUtilities.CreateInstance

            return args.Length == 0 ?
                typeActivationDelegate.Invoke() as TInterface :
                typeActivationDelegate.Invoke(args) as TInterface;
        }

        private TypeActivationDelegate? GetOrAddTypeActivationDelegate(Type type, ConstructorInfo ctor)
        {
            if (!type.Assembly.IsCollectible)
            {
                return _cache.GetOrAdd(type, CreateTypeActivationDelegate(ctor));
            }

            var y = _collectibleCache.Value.TryGetValue(type, out var t);
            var x = ReferenceEquals(t, ctor);

            if (_collectibleCache.Value.TryGetValue(type, out var typeActivationDelegate))
            {
                return typeActivationDelegate;
            }

            typeActivationDelegate = CreateTypeActivationDelegate(ctor);

            _collectibleCache.Value.AddOrUpdate(type, typeActivationDelegate);

            return typeActivationDelegate;
        }

        private static TypeActivationDelegate CreateTypeActivationDelegate(ConstructorInfo ctor)
        {
            var parameters = ctor.GetParameters();

            var param = Expression.Parameter(
                typeof(object[]),
                "args");

            var argsExp = new Expression[parameters.Length];

            for (var i = 0; i < parameters.Length; i++)
            {
                var index = Expression.Constant(i);
                var paramType = parameters[i].ParameterType;

                var paramAccessorExp = Expression.ArrayIndex(
                    param,
                    index);

                var paramCastExp = Expression.Convert(
                    paramAccessorExp,
                    paramType);

                argsExp[i] = paramCastExp;
            }

            var newExp = Expression.New(
                ctor,
                argsExp);

            var lambda = Expression.Lambda<TypeActivationDelegate>(newExp, param);

            return lambda.Compile();
        }
    }
}
