namespace sfmsBackEnd2.FeeModule
{
    public class FeeAnalytics
    {
        public int id { get; set; }
        public int student_id { get; set; }
        public double total_fee { get; set; }
        public double paid_fee { get; set; }
        public double unpaid_fee { get; set; }
    }
}
