using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DTOs
{
    public class MenuItemEdit
    {
        [Required]
        [DisplayName("Meal Name")]
        [MaxLength(150, ErrorMessage = "Meal Name cannot Exceed 150 Characters")]
        public string MealName { get; set; } = null!;

        [Required]
        [DisplayName("Meal Name")]
        [MaxLength(150, ErrorMessage = "Meal Description cannot Exceed 150 Characters")]
        public string MealDescription { get; set; } = null!;

        [Required]
        public decimal Price { get; set; }
    }
}