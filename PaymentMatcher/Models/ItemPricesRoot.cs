using System.Collections.Generic;
using System.Xml.Serialization;

namespace PaymentMatcher.Models
{
    public class ItemPricesRoot
    {
        [XmlArray("ItemPricesList")]
        public List<ItemPrice> ItemPrices { get; set; }
    }
}
