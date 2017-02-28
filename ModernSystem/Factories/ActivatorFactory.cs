using ModernSystem.Contracts;
using System;

namespace ModernSystem.Factories
{
    public class ActivatorFactory<TTarget> : IFactory<TTarget>
    {
        public TTarget Create()
        {
            return Activator.CreateInstance<TTarget>();
        }

        public TTarget CreateWithParameters(params object[] args)
        {
            return (TTarget)Activator.CreateInstance(typeof(TTarget), args ?? new object[0]);
        }

        // TODO: Expose ActivationContext somehow, for increased flexibility
    }
}
