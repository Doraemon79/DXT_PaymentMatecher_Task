namespace PaymentMatcher.Models
{
    public class Payment
    {
        public int Customer { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public decimal Amount { get; set; }
    }
}
