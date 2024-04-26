using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TourWeb.Models
{
    public class BookTour
    {
        [Key]
        [Display(Name = "BookTour Id")]
        public int Id { get; set; }
        [StringLength(50)]
        public string? Country { get; set; }
        [DisplayFormat(DataFormatString = "{0:h:mm tt}")]
        public DateTime Time { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Date { get; set; }
        public int GuestNumber { get; set; }
        public int ChildrenNumber {  get; set; }
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal? TotalMoney { get; set; }
        public int? TourId { get; set; }
        public Tour? Tour { get; set; }
        public int? TrekkingId { get; set; }
        public Trekking? Trekkings { get; set; }
        public int AccountId { get; set; }
        public Account? Accounts { get; set; }
    }
}
