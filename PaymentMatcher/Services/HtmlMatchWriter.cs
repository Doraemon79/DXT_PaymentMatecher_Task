using System.Collections.Generic;
using System.IO;
using System.Text;
using PaymentMatcher.Models;

namespace PaymentMatcher.Services
{
    public class HtmlMatchWriter : IMatchWriter
    {
        public void Write(string fileName, IEnumerable<PaymentMatch> matches)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(@"<html><body><table>");

            sb.AppendLine(@"
<style>
table, th, td {
  border: 1px solid black;
  border-collapse: collapse;
  padding: 5px;
}
</style>
");

            sb.AppendLine(@"<tr>");
            var propertyInfos = typeof(PaymentMatch).GetProperties();
            foreach (var propertyInfo in propertyInfos)
            {
                sb.AppendLine($@"<th>{propertyInfo.Name}</th>");
            }
            sb.AppendLine(@"</tr>");

            foreach (var paymentMatch in matches)
            {
                sb.AppendLine(@"<tr>");
                foreach (var propertyInfo in propertyInfos)
                {
                    sb.AppendLine($@"<td>{propertyInfo.GetValue(paymentMatch)}</td>");
                }
                sb.AppendLine(@"</tr>");
            }

            sb.AppendLine(@"</table></body></html>");

            File.WriteAllText(fileName, sb.ToString());
        }
    }
}