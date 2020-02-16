using PaymentMatcher.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentMatcher
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                IPaymentReaderService paymentReaderService = new PaymentReaderService();
                IPriceReaderService priceReaderService = new PriceReaderService();
                IPurchaseReaderService purchaseReaderService = new PurchaseReaderService();
                IMatcherService matcherService = new MatcherService();

                var format = args.Length > 0 ? args[0] : "json";
                IMatchWriter matchWriter = CreateMatcher(format);

                var payments = paymentReaderService.Read(@"Data\Payments.json");
                var prices = priceReaderService.Read(@"Data\Prices.xml");
                var purchases = purchaseReaderService.Read(@"Data\Purchases.dat");

                var matches = matcherService.Match(payments, prices, purchases);

                var fileName = args.Length > 1 ? args[1] : "PaymentsNotMatched." + format;
                matchWriter.Write(fileName, matches);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }

        private static IMatchWriter CreateMatcher(string format)
        {
            return format.ToLower() switch
            {
                "json" => new JsonMatchWriter(),
                "csv" => new CsvMatchWriter(),
                "html" => new HtmlMatchWriter(),
                _ => throw new ArgumentException("Wrong writer format")
            };
        }
    }
}
