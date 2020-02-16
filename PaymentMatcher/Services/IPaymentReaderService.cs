using System.Collections.Generic;
using PaymentMatcher.Models;

namespace PaymentMatcher.Services
{
    interface IPaymentReaderService
    {
        List<Payment> Read(string fileName);
    }
}
