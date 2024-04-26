using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TourWeb.Models
{
    public class CartDetail
    {
        [Key]
        [Display(Name = "CartDetail Id")]
        public int Id { get; set; }
        [ForeignKey("Product")]
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public Product? Products { get; set; }
        [ForeignKey("Cart")]
        public int CartId { get; set;}
        public Cart? Carts { get; set; }
    }
}
