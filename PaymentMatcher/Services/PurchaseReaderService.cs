using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using PaymentMatcher.Models;

namespace PaymentMatcher.Services
{
    public class PurchaseReaderService : IPurchaseReaderService
    {

        //this is used for the file Purchase.dat formatted as specified in the requirements you emailed me
        //private const string CustomerPattern = @"(CUST(?<customerNumber>\d{8}))";
        //private const string DatePattern = @"(DATE(?<day>\d{2})(?<month>\d{2})(?<year>\d{4})(?<hour>\d{2})(?<minute>\d{2}))";
        //private const string ItemPattern = @"(ITEM(?<itemNumber>\d{8}))";
        //private const string SentinelPattern = @"(.)";

        //this works for the file Purchase.Dat you actually sent me
        private const string CustomerPattern = @"(CUST(?<customerNumber>\d{6}))";
        private const string DatePattern = @"(DATE(?<day>\d{2})(?<month>\d{2})(?<year>\d{4})(?<hour>\d{2})(?<minute>\d{2}))";
        private const string ItemPattern = @"(ITEM(?<itemNumber>\d{6}))";


        static readonly Regex Regex = new Regex($@"^({CustomerPattern}|{DatePattern}|{ItemPattern})$",
            RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.ExplicitCapture);

        public List<Purchase> Read(string fileName)
        {
            List<Purchase> res = new List<Purchase>();

            Purchase purchase = null;

            var lines = File.ReadAllLines(fileName);

            foreach (var line in lines)
            {
                Match match = Regex.Match(line);
                if (match.Success)
                {
                    if (match.Groups["customerNumber"].Success)
                    {
                        purchase = new Purchase();
                        res.Add(purchase);
                        purchase.Customer = int.Parse(match.Groups["customerNumber"].Value);
                    }
                    else if (match.Groups["itemNumber"].Success)
                    {
                        Debug.Assert(purchase != null, nameof(purchase) + " != null");
                        purchase.Items.Add(int.Parse(match.Groups["itemNumber"].Value));
                    }
                    else if (match.Groups["year"].Success)
                    {
                        Debug.Assert(purchase != null, nameof(purchase) + " != null");
                        purchase.DateTime = new DateTime(int.Parse(match.Groups["year"].Value), int.Parse(match.Groups["month"].Value),
                            int.Parse(match.Groups["day"].Value), int.Parse(match.Groups["hour"].Value), int.Parse(match.Groups["minute"].Value), 0);
                    }
                }
                else
                {
                    throw new FormatException("Wrong file format");
                }
            }

            //var input = File.ReadAllText(fileName);
            //foreach (Match match in Regex.Matches(input))
            //{
            //    if (match.Groups["customerNumber"].Success)
            //    {
            //        purchase = new Purchase();
            //        res.Add(purchase);
            //        purchase.Customer = int.Parse(match.Groups["customerNumber"].Value);
            //    }
            //    else if (match.Groups["itemNumber"].Success)
            //    {
            //        Debug.Assert(purchase != null, nameof(purchase) + " != null");
            //        purchase.Items.Add(int.Parse(match.Groups["itemNumber"].Value));
            //    }
            //    else if (match.Groups["year"].Success)
            //    {
            //        Debug.Assert(purchase != null, nameof(purchase) + " != null");
            //        purchase.DateTime = new DateTime(int.Parse(match.Groups["year"].Value), int.Parse(match.Groups["month"].Value),
            //            int.Parse(match.Groups["day"].Value), int.Parse(match.Groups["hour"].Value), int.Parse(match.Groups["minute"].Value), 0);
            //    }
            //    else
            //    {
            //        throw new FormatException("Wrong file format");
            //    }
            //}

            return res;
        }
    }
}