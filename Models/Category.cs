using System.ComponentModel.DataAnnotations;

namespace TourWeb.Models
{
    public class Category
    {
        [Key]
        [Display(Name = "Category Id")]
        public int Id { get; set; }
        [Required]
        [StringLength(20)]
        public string? Name { get; set; }

        public ICollection<Product>? Products { get; set;}
    }
}
