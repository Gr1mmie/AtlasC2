using System.IO;
using System.Runtime.Serialization.Json;

namespace Implant.Utils
{
    public static class Extensions
    {
        public static byte[] Serialize<T>(this T data)
        {
            var serializer = new DataContractJsonSerializer(typeof(T));

            using(var stream = new MemoryStream()){
                serializer.WriteObject(stream, data);
                return stream.ToArray();
            }
        }

        public static T Deserialize<T>(this byte[] data){
            var serializer = new DataContractJsonSerializer(typeof(T));

            using (var stream = new MemoryStream(data)) { return (T) serializer.ReadObject(stream); }
        }
    }
}
