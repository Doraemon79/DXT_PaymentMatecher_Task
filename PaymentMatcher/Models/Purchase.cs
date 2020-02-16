using System;
using System.Collections.Generic;

namespace PaymentMatcher.Models
{
    public class Purchase
    {
        public int Customer { get; set; }
        public DateTime DateTime { get; set; }
        public List<int> Items { get; set; } = new List<int>();
    }
}
