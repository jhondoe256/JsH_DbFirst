using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DTOs
{
    public class CustomerOrderEdit
    {
        [Required]
        [DisplayName("Order Date")]
        public DateTime OrderDate { get; set; }

        [Required]
        [DisplayName("Customer Id")]
        public int CustomerId { get; set; }

        [Required]
        [DisplayName("Menu Item Id")]
        public int MenuItemId { get; set; }
    }
}