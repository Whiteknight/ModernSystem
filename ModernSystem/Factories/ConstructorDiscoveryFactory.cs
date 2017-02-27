using ModernSystem.Contracts;
using System;

namespace ModernSystem.Factories
{
    public class ConstructorDiscoveryFactory<TTarget, TArg> : IFactory<TTarget, TArg>
    {
        private readonly ScoredConstructor _constructor;

        public ConstructorDiscoveryFactory()
        {
            _constructor = ConstructorDiscovery.GetBestConstructor(typeof(TTarget), new[] { typeof(TArg) });
            if (_constructor == null)
                throw new InvalidOperationException("The type " + typeof(TTarget).FullName + "Has no valid constructors");
        }

        public TTarget Create(TArg argument)
        {
            return (TTarget)_constructor.Construct(new object[] { argument });
        }
    }

    public class ConstructorDiscoveryFactory<TTarget> : IFactory<TTarget, object[]>
        where TTarget : class
    {
        public TTarget Create(params object[] argument)
        {
            return ConstructorDiscovery.CreateInstance<TTarget>(argument);
        }
    }
}
