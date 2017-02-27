using ModernSystem.Contracts;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace ModernSystem.Configuration
{

    public class OverrideableConfigurationDecorator : IConfiguration
    {
        private readonly IConfiguration _inner;
        private readonly Dictionary<string, string> _overrides;

        public OverrideableConfigurationDecorator(IConfiguration inner, Dictionary<string, string> overrides)
        {
            if (inner == null)
                throw new ArgumentNullException(nameof(inner));

            if (overrides == null)
                throw new ArgumentNullException(nameof(overrides));

            _inner = inner;
            _overrides = overrides;
        }

        public bool Exists(string name)
        {
            return _overrides.ContainsKey(name) || _inner.Exists(name);
        }

        public string Get(string name, string defaultValue = null)
        {
            if (_overrides.ContainsKey(name) && _overrides[name] != null)
                return _overrides[name];
            return _inner.Get(name, defaultValue);
        }

        public T Get<T>(string name, Func<string, T> parse, T defaultValue = default(T))
        {
            string s = Get(name);
            if (s == null)
                return defaultValue;
            return parse(s);
        }
    }
}
