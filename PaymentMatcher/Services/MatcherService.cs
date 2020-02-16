using System;
using System.Collections.Generic;
using System.Linq;
using PaymentMatcher.Models;

namespace PaymentMatcher.Services
{
    public class MatcherService : IMatcherService
    {
        private struct PurchaseKey
        {
            public PurchaseKey(int customer, int year, int month)
            {
                Customer = customer;
                Year = year;
                Month = month;
            }

            public int Customer { get; }
            public int Year { get; }
            public int Month { get; }
        }

        public List<PaymentMatch> Match(IEnumerable<Payment> payments, IEnumerable<ItemPrice> prices, IEnumerable<Purchase> purchases)
        {
            var itemPrices = prices.ToDictionary(p => p.Item, p => p.Price);

            var duePayments = purchases
                .GroupBy(p => new PurchaseKey(p.Customer, p.DateTime.Year, p.DateTime.Month))
                .Select(p => new { p.Key, Cost = p.Sum(t => t.Items.Sum(r => itemPrices[r])) })
                .ToDictionary(p => p.Key, p => p.Cost);
            var payedPayments = payments
                .ToDictionary(p => new PurchaseKey(p.Customer, p.Year, p.Month), p => p.Amount);

            foreach (var payment in payedPayments)
            {
                if (!duePayments.ContainsKey(payment.Key))
                    duePayments.Add(payment.Key, 0);
            }

            return duePayments
                .Select(p =>
                {
                    payedPayments.TryGetValue(p.Key, out decimal payed);

                    return new PaymentMatch()
                    {
                        Customer = p.Key.Customer,
                        Year = p.Key.Year,
                        Month = p.Key.Month,
                        AmountDue = p.Value,
                        AmountPayed = payed,
                        Difference = p.Value - payed
                    };
                })
                .Where(p => p.Difference != 0)
                .OrderByDescending(p => Math.Abs(p.Difference))
                .ToList();
        }
    }
}