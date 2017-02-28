using ModernSystem.Contracts;
using System;

namespace ModernSystem.Serialization
{
    public class Base64StringSerializer<T> : IStringSerializer<T>
    {
        private readonly ISerializer<T> _serializer;

        public Base64StringSerializer(ISerializer<T> serializer)
        {
            if (serializer == null)
                throw new ArgumentNullException(nameof(serializer));
            _serializer = serializer;
        }

        public string Serialize(T dataObject)
        {
            var bytes = _serializer.Serialize(dataObject);
            return Convert.ToBase64String(bytes);
        }

        public T Deserialize(string serialized)
        {
            if (string.IsNullOrEmpty(serialized))
                return default(T);

            var bytes = Convert.FromBase64String(serialized);
            return _serializer.Deserialize(bytes);
        }
    }
}
