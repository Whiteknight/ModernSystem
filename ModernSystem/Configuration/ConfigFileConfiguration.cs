using ModernSystem.Contracts;
using System;
using System.Configuration;
using System.Linq;

namespace ModernSystem.Configuration
{
    public class ConfigFileConfiguration : IConfiguration
    {
        public bool Exists(string name)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));

            return ConfigurationManager.AppSettings.AllKeys.Contains(name);
        }

        public string Get(string name, string defaultValue = null)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));

            return ConfigurationManager.AppSettings[name] ?? defaultValue;
        }

        public T Get<T>(string name, Func<string, T> parse, T defaultValue = default(T))
        {
            if (parse == null)
                throw new ArgumentNullException(nameof(parse));

            var s = Get(name);
            if (s == null)
                return defaultValue;
            return parse(s);
        }
    }
}
