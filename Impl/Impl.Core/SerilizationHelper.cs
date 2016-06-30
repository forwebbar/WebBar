using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace Impl.Core
{
    public static class SerilizationHelper
    {
        public static void SerializeObject(this List<string> list, string fileName) {
          var serializer = new XmlSerializer(typeof(List<string>));
          using ( var stream = File.OpenWrite(fileName)) {
            serializer.Serialize(stream, list);
          }
        }

        public static void Deserialize(this List<string> list, string fileName)
        {
          var serializer = new XmlSerializer(typeof(List<string>));
          using ( var stream = File.OpenRead(fileName) ){
            var other = (List<string>)(serializer.Deserialize(stream));
            list.Clear();
            list.AddRange(other);
          }
        }
    }

    public static class SerilizationHelper<T>
    {
        public static void SerializeObject(List<T> list, string fileName)
        {
            var serializer = new XmlSerializer(typeof(List<T>));
            using (var stream = File.OpenWrite(fileName))
            {
                serializer.Serialize(stream, list);
            }
        }

        public static void Deserialize(List<T> list, string fileName)
        {
            var serializer = new XmlSerializer(typeof(List<T>));
            using (var stream = File.OpenRead(fileName))
            {
                var other = (List<T>)(serializer.Deserialize(stream));
                list.Clear();
                list.AddRange(other);
            }
        }
    }
    
}
