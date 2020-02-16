using System.Collections.Generic;
using System.Globalization;
using System.IO;
using CsvHelper;
using PaymentMatcher.Models;

namespace PaymentMatcher.Services
{
    public class CsvMatchWriter : IMatchWriter
    {
        public void Write(string fileName, IEnumerable<PaymentMatch> matches)
        {
            using var writer = new StreamWriter(fileName);
            using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
            csv.WriteRecords(matches);
        }
    }
}