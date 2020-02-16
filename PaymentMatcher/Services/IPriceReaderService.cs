using System.Collections.Generic;
using PaymentMatcher.Models;

namespace PaymentMatcher.Services
{
    public interface IPriceReaderService
    {
        List<ItemPrice> Read(string fileName);
    }
}