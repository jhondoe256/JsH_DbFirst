using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DTOs
{
    public class LocationEdit
    {
        [Required]
        [MaxLength(100)]
        public string Address { get; set; } = null!;

        [Required]
        [DisplayName("Phone Number")]
        [MaxLength(15)]
        public string PhoneNumber { get; set; } = null!;

        [Required]
        [DisplayName("Grand Opening")]
        public DateTime GrandOpening { get; set; }
    }
}