namespace PaymentMatcher.Models
{
    public class PaymentMatch
    {
        public int Customer { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public decimal AmountDue { get; set; }
        public decimal AmountPayed { get; set; }
        public decimal Difference { get; set; }
    }
}