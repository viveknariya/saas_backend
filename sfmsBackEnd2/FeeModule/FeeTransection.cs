namespace sfmsBackEnd2.FeeModule
{
    public class FeeTransection
    {
        public Guid Id { get; set; }
        public double amount { get; set; }
        public Guid student_id { get; set; }
        public string comment { get; set; }
        public DateTime date_of_transection { get; set; }
        public string mode_of_transection { get; set; }
    }
}