using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace Catrobat.Core.Misc.JSON
{
    public static class JSONClassDeserializer
    {
        public static T Deserialise<T>(string json)
        {
            var obj = Activator.CreateInstance<T>();
            using (var memoryStream = new MemoryStream(Encoding.Unicode.GetBytes(json)))
            {
                var serializer = new DataContractJsonSerializer(obj.GetType());
                obj = (T) serializer.ReadObject(memoryStream);
                return obj;
            }
        }
    }
}