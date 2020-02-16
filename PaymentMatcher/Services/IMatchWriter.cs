using System.Collections.Generic;
using PaymentMatcher.Models;

namespace PaymentMatcher.Services
{
    public interface IMatchWriter
    {
        void Write(string fileName, IEnumerable<PaymentMatch> matches);
    }
}