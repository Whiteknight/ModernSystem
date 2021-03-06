﻿using ModernSystem.Contracts;

namespace ModernSystem.Factories
{
    public class InstanceFactory<TTarget> : IFactory<TTarget>
    {
        private readonly TTarget _instance;

        public InstanceFactory(TTarget instance)
        {
            _instance = instance;
        }

        public TTarget Create()
        {
            return _instance;
        }
    }
}
