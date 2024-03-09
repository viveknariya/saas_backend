using System.ComponentModel.DataAnnotations;

namespace sfmsbackend2.StudentModule
{
    public class Student
    {
        [Key]
        public int id { get; set; }
        public string last_name { get; set; } = "";
        public string first_name { get; set; } = "";
        public string standard { get; set; } = "";
        public string parents_name { get; set; } = "";
        public string parents_mobile { get; set; } = "";
        public string? comment { get; set; }
        public string? school_name { get; set; }
        public DateTime date_of_admission { get; set; }
        public DateTime date_of_birth { get; set; }
        public string gender { get; set; } = "";
    }
}


