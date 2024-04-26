using System.ComponentModel.DataAnnotations;

namespace TourWeb.Models
{
    public class OrderDetail
    {
        [Key]
        [Display(Name = "OrderDetail Id")]
        public int Id { get; set; }
        [Required]
        public int OrderId { get; set; }
        public Order? Orders { get; set; }
        [Required]
        public int ProductId { get; set; }
        public Product? Product { get; set; }
        public int Quantity { get; set; }
    }
}
