using System.Collections.Generic;
using PaymentMatcher.Models;

namespace PaymentMatcher.Services
{
    public interface IPurchaseReaderService
    {
        List<Purchase> Read(string fileName);
    }
}