using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using PaymentMatcher.Models;

namespace PaymentMatcher.Services
{
    public class PaymentReaderService : IPaymentReaderService
    {
        public List<Payment> Read(string fileName)
        {
            return JsonConvert.DeserializeObject<List<Payment>>(File.ReadAllText(fileName));
        }
    }
}
