using System.IO;
using System.Runtime.Serialization;

namespace Impl.Core
{
    public static class SeializeHelper
    {
        public static object DeSerialize(byte[] value)
        {
            try
            {
                var ns = new NetDataContractSerializer();
                using (var m = new MemoryStream(value))
                    return ns.ReadObject(m);
            }
            catch
            {
                return null;
            }
            
        }

        public static byte[] Serialize(object value)
        {
            var ns = new NetDataContractSerializer();
            using (var s = new MemoryStream())
            {
                ns.WriteObject(s, value);
                return s.ToArray();
            }
        }        
    }
}