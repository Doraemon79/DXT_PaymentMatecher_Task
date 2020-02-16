using System.Collections.Generic;
using PaymentMatcher.Models;

namespace PaymentMatcher.Services
{
    public interface IMatcherService
    {
        List<PaymentMatch> Match(IEnumerable<Payment> payments, IEnumerable<ItemPrice> prices, IEnumerable<Purchase> purchases);
    }
}