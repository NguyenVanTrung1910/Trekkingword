using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TourWeb.Models
{
    public class Cart
    {
        [Key]
        [Display(Name = "Cart Id")]
        public int Id { get; set; }
        [DisplayFormat(DataFormatString = "{0:C}")]
        
        public decimal Total { get; set; }
        public int AccountId { get; set; }
        public Account? Accounts { get; set; }
        public ICollection<CartDetail>? CartDetails { get; set; }
    }
}
