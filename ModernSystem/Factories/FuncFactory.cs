using System;
using ModernSystem.Contracts;

namespace ModernSystem.Factories
{
    public static class FuncFactory
    {
        public static IFactory<TTarget> WrapFunction<TTarget>(Func<TTarget> factory)
        {
            return new FuncFactory<TTarget>(factory);
        }

        public static IFactory<TTarget, TArg> WrapFunction<TTarget, TArg>(Func<TArg, TTarget> factory)
        {
            return new FuncFactory<TTarget, TArg>(factory);
        }
    }

    public class FuncFactory<TTarget> : IFactory<TTarget>
    {
        private readonly Func<TTarget> _factory;

        public FuncFactory(Func<TTarget> factory)
        {
            if (factory == null)
                throw new ArgumentNullException(nameof(factory));

            _factory = factory;
        }

        public TTarget Create()
        {
            return _factory();
        }
    }

    public class FuncFactory<TTarget, TArg> : IFactory<TTarget, TArg>
    {
        private readonly Func<TArg, TTarget> _factory;

        public FuncFactory(Func<TArg, TTarget> factory)
        {
            if (factory == null)
                throw new ArgumentNullException(nameof(factory));

            _factory = factory;
        }

        public TTarget Create(TArg argument)
        {
            return _factory(argument);
        }
    }
}
