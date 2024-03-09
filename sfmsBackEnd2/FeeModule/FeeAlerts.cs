namespace sfmsBackEnd2.FeeModule
{
    public class FeeAlerts
    {
        public Guid id { get; set; }

        public DateTime fee_alert_date { get; set; }

        public string fee_alert_status { get; set; } = "";

        public Guid student_id { get; set; }

        public int identity_ref { get; set; }
    }
}