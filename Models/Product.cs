using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TourWeb.Models
{
    public class Product
    {
        [Key]
        [Display(Name ="Product Id")]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string? Name { get; set; }

        [StringLength(150)]
        public string? Detail { get; set; }
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal Price { get; set; }= 0;
        
        public int Quantity { get; set; }
        public string? Description { get; set; }
        public decimal Review { get; set; }
        public string? Feature { get; set; }
        public string? ImagesSpecial { get; set; }
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Category? Categorys { get; set; }
        public ICollection<CartDetail>? CartDetails { get; set;}
        public ICollection<Image>? Images { get; set; }

    }
}
