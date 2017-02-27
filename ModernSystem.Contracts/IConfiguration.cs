using System;

namespace ModernSystem.Contracts
{

    public interface IConfiguration
    {
        bool Exists(string name);
        string Get(string name, string defaultValue = null);
        T Get<T>(string name, Func<string, T> parse, T defaultValue = default(T));
    }
}
