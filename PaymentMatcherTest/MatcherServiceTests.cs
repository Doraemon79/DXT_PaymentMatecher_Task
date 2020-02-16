using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentMatcher.Models;
using PaymentMatcher.Services;

namespace PaymentMatcherTest
{
    [TestClass()]
    public class MatcherServiceTests
    {
        private bool PaymentMatchesEqual(PaymentMatch p0, PaymentMatch p1)
        {
            return p0.Customer == p1.Customer && p0.Year == p1.Year && p0.Month == p1.Month
                   && p0.AmountDue == p1.AmountDue && p0.AmountPayed == p1.AmountPayed && p0.Difference == p1.Difference;
        }

        [TestMethod()]
        public void MatchTest()
        {
            IMatcherService matcherService = new MatcherService();

            var payments = new List<Payment>()
            {
                new Payment() {Customer = 1, Year = 2020, Month = 1, Amount = 5}
            };
            var prices = new List<ItemPrice>()
            {
                new ItemPrice() {Item = 1, Price = 10},
                new ItemPrice() {Item = 2, Price = 100}
            };
            var purchases = new List<Purchase>()
            {
                new Purchase() { Customer = 1, DateTime = new DateTime(2020, 1, 13), Items = {1,2}}
            };

            var actualMatches = matcherService.Match(payments, prices, purchases);
            var expectedMatches = new List<PaymentMatch>()
            {
                new PaymentMatch() { Customer = 1, Year = 2020, Month = 1, AmountDue = 110, AmountPayed = 5, Difference = 105}
            };

            Assert.AreEqual(expectedMatches.Count, actualMatches.Count);
            for (int i = 0; i < expectedMatches.Count; i++)
            {
                var expectedMatch = expectedMatches[i];
                var actualMatch = actualMatches[i];
                Assert.IsTrue(PaymentMatchesEqual(expectedMatch, actualMatch));
            }
        }
    }
}