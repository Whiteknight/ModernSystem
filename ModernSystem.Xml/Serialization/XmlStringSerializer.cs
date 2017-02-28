using ModernSystem.Contracts;
using System.IO;
using System.Xml.Serialization;

namespace ModernSystem.Xml.Serialization
{
    public class XmlStringSerializer<T> : IStringSerializer<T>
    {
        public string Serialize(T dataObject)
        {
            using (var writer = new StringWriter())
            {
                new XmlSerializer(typeof(T)).Serialize(writer, dataObject);
                return writer.ToString();
            }
        }

        public T Deserialize(string serialized)
        {
            using (var sr = new StringReader(serialized))
            {
                var xsr = new XmlSerializer(typeof(T));
                return (T)xsr.Deserialize(sr);
            }
        }
    }
}
