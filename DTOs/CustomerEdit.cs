using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DTOs
{
    public class CustomerEdit
    {
        [Required]
        [DisplayName("First Name")]
        public string FirstName { get; set; } = null!;

        [Required]
        [DisplayName("Last Name")]
        public string LastName { get; set; } = null!;
    }
}