using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using PaymentMatcher.Models;

namespace PaymentMatcher.Services
{
    public class JsonMatchWriter : IMatchWriter
    {
        public void Write(string fileName, IEnumerable<PaymentMatch> matches)
        {
            using StreamWriter sw = new StreamWriter(fileName);
            using JsonWriter writer = new JsonTextWriter(sw);
            writer.Formatting = Formatting.Indented;
            JsonSerializer serializer = new JsonSerializer();
            serializer.Serialize(writer, matches);
        }
    }
}