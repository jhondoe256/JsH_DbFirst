using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DTOs
{
    public class EmployeeEdit
    {
        [Required]
        [DisplayName("First Name")]
        [MaxLength(150)]
        public string FirstName { get; set; } = null!;

        [Required]
        [DisplayName("Last Name")]
        [MaxLength(150)]
        public string LastName { get; set; } = null!;

        [DisplayName("Application Submission Date")]
        public DateTime? ApplicationSubmissionDate { get; set; }

        [Required]
        [DisplayName("Hire Date")]
        public DateTime HireDate { get; set; }

        [Required]
        [DisplayName("Location Id")]
        public int? LocationId { get; set; }
    }
}