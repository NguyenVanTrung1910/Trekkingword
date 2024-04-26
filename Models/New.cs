using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TourWeb.Models
{
    public class New
    {
        [Key]
        [Display(Name = "BookTour Id")]
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Content {  get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Date { get; set; }
        public string? ImageSpecail { get; set; } = null;
        [ForeignKey("Account")]
        public int AccountId { get; set; }
        public Account? Accounts { get; set; }
        public ICollection<Image>? Images { get; set; }

    }
}
