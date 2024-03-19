using System.ComponentModel.DataAnnotations;

namespace sfmsbackend2.StudentModule
{
    public class Student
    {
        [Key]
        public int id { get; set; }
        [Required(ErrorMessage = "First name is required")]
        [RegularExpression("[a-zA-Z]{3,20}", ErrorMessage = "Last name must be between 3 and 20 characters and contain only letters")]
        public string last_name { get; set; } = "";
        [Required(ErrorMessage = "First name is required")]
        [RegularExpression("[a-zA-Z]{3,20}", ErrorMessage = "First name must be between 3 and 20 characters and contain only letters")]
        public string first_name { get; set; } = "";
        public string standard { get; set; } = "";
        [Required(ErrorMessage = "First name is required")]
        [RegularExpression("[a-zA-Z]{3,20}", ErrorMessage = "Parents name must be between 3 and 20 characters and contain only letters")]
        public string parents_name { get; set; } = "";
        public string parents_mobile { get; set; } = "";
        public string? comment { get; set; }
        public string? school_name { get; set; }
        public DateTime date_of_admission { get; set; }
        public DateTime date_of_birth { get; set; }
        public string gender { get; set; } = "";
        public int fee_structure_id { get; set; }
    }
}


