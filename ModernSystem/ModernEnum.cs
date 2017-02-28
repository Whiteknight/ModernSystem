using System;

namespace ModernSystem
{
    public static class ModernEnums
    {
        public static TEnum? Parse<TEnum>(string name)
            where TEnum : struct
        {
            TEnum value;
            bool ok = Enum.TryParse<TEnum>(name, true, out value);
            return ok ? (TEnum?)value : null;
        }

        public static TEnum ParseOrDefault<TEnum>(string name, TEnum defaultValue)
            where TEnum : struct
        {
            TEnum value;
            return Enum.TryParse<TEnum>(name, true, out value) ? value : defaultValue;
        }
    }
}
