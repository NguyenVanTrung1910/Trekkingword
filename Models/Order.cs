using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TourWeb.Models
{
    public class Order
    {
        [Key]
        [Display(Name = "Order Id")]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal Total { get; set; } 
        [StringLength(20)]
        public string? State { get; set; } 
        [ForeignKey("Account")]
        public int AccountId { get; set; }
        public Account? Accounts { get; set; }
        public int OrderInfoId { get; set; }
        public OrderInfo? OrderInfos { get; set; }
    }
}
