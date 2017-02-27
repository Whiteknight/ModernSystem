using ModernSystem.Contracts;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace ModernSystem.Configuration
{

    public class NamespacedConfigurationDecorator : IConfiguration
    {
        private readonly string _namespaceName;
        private readonly IConfiguration _inner;

        public NamespacedConfigurationDecorator(string namespaceName, IConfiguration inner)
        {
            if (string.IsNullOrEmpty(namespaceName))
                throw new ArgumentNullException(nameof(namespaceName));

            if (inner == null)
                throw new ArgumentNullException(nameof(inner));

            _namespaceName = namespaceName;
            _inner = inner;
        }

        public bool Exists(string name)
        {
            string fullName = CreateFullName(_namespaceName, name);
            return _inner.Exists(fullName);
        }

        public string Get(string name, string defaultValue = null)
        {
            string fullName = CreateFullName(_namespaceName, name);
            return _inner.Get(fullName, defaultValue);
        }

        public T Get<T>(string name, Func<string, T> parse, T defaultValue = default(T))
        {
            string fullName = CreateFullName(_namespaceName, name);
            return _inner.Get(fullName, parse, defaultValue);
        }

        protected virtual string CreateFullName(string namespaceName, string name)
        {
            return namespaceName + ":" + name;
        }
    }
}
