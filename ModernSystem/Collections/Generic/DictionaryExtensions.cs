using System;
using System.Collections.Generic;

namespace ModernSystem.Collections.Generic
{
    public static class DictionaryExtensions
    {
        public static void AlwaysAdd<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue value)
        {
            if (dictionary == null)
                throw new ArgumentNullException(nameof(dictionary));

            if (dictionary.ContainsKey(key))
                dictionary[key] = value;
            else
                dictionary.Add(key, value);
        }

        public static void AddIfMissing<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue value)
        {
            if (dictionary == null)
                throw new ArgumentNullException(nameof(dictionary));

            if (!dictionary.ContainsKey(key))
                dictionary.Add(key, value);
        }

        public static void Add<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue value, Func<TValue, TValue, TValue> resolveConflict)
        {
            if (dictionary == null)
                throw new ArgumentNullException(nameof(dictionary));
            if (resolveConflict == null)
                throw new ArgumentNullException(nameof(resolveConflict));

            if (dictionary.ContainsKey(key))
            {
                var resolvedValue = resolveConflict(dictionary[key], value);
                dictionary[key] = resolvedValue;
            }
            else
                dictionary.Add(key, value);
        }

        public static TValue TryGet<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
        {
            return TryGet(dictionary, key, default(TValue));
        }

        public static TValue TryGet<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue defaultValue)
        {
            if (dictionary == null)
                throw new ArgumentNullException(nameof(dictionary));

            return dictionary.ContainsKey(key) ? dictionary[key] : defaultValue;
        }

        public static TValue TryGet<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key, Func<TKey, TValue> getDefaultValue)
        {
            if (dict == null)
                throw new ArgumentNullException(nameof(dict));

            if (dict.ContainsKey(key))
                return dict[key];

            if (getDefaultValue == null)
                throw new ArgumentNullException(nameof(getDefaultValue));
            return getDefaultValue(key);
        }

        public static TValue TryGet<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> dictionary, TKey key)
        {
            return TryGet(dictionary, key, default(TValue));
        }

        public static TValue TryGet<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> dictionary, TKey key, TValue defaultValue)
        {
            if (dictionary == null)
                throw new ArgumentNullException(nameof(dictionary));

            return dictionary.ContainsKey(key) ? dictionary[key] : defaultValue;
        }

        public static TValue TryGet<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> dictionary, TKey key, Func<TKey, TValue> getDefaultValue)
        {
            if (dictionary == null)
                throw new ArgumentNullException(nameof(dictionary));

            if (dictionary.ContainsKey(key))
                return dictionary[key];

            if (getDefaultValue == null)
                throw new ArgumentNullException(nameof(getDefaultValue));
            return getDefaultValue(key);
        }

        public static Dictionary<TKey, TValue> ToDictionarySafe<TData, TKey, TValue>(this IEnumerable<TData> src, Func<TData, TKey> getKey, Func<TData, TValue> getValue, Func<TValue, TValue, TValue> resolveConflict)
        {
            var dictionary = new Dictionary<TKey, TValue>();
            if (src == null)
                return dictionary;

            if (getKey == null)
                throw new ArgumentNullException(nameof(getKey));
            if (getValue == null)
                throw new ArgumentNullException(nameof(getValue));
            if (resolveConflict == null)
                throw new ArgumentNullException(nameof(resolveConflict));

            foreach (TData data in src)
            {
                var key = getKey(data);
                var value = getValue(data);
                dictionary.Add(key, value, resolveConflict);
            }
            return dictionary;
        }
    }
}
