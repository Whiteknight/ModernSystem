using ModernSystem.Contracts;
using System;

namespace ModernSystem
{
    public class DefaultConstructorFactory<TTarget> : IFactory<TTarget>
        where TTarget : new()
    {
        public TTarget Create()
        {
            return new TTarget();
        }
    }
}
