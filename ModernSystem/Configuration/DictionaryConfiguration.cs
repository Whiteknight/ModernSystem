using ModernSystem.Contracts;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace ModernSystem.Configuration
{

    public class DictionaryConfiguration : IConfiguration
    {
        private readonly Dictionary<string, string> _values;

        public DictionaryConfiguration(Dictionary<string, string> overrides)
        {
            if (overrides == null)
                throw new ArgumentNullException(nameof(overrides));

            _values = overrides;
        }

        public bool Exists(string name)
        {
            return _values.ContainsKey(name);
        }

        public string Get(string name, string defaultValue = null)
        {
            if (_values.ContainsKey(name) && _values[name] != null)
                return _values[name];
            return defaultValue;
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
