using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using PaymentMatcher.Models;

namespace PaymentMatcher.Services
{
    public class PriceReaderService : IPriceReaderService
    {
        public List<ItemPrice> Read(string fileName)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ItemPricesRoot));
            using Stream stream = new FileStream(fileName, FileMode.Open);
            using StreamReader reader = new StreamReader(stream, Encoding.UTF8);
            return ((ItemPricesRoot)serializer.Deserialize(reader)).ItemPrices;
        }
    }
}