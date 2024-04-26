using System.ComponentModel.DataAnnotations;

namespace TourWeb.Models
{
    public class Contact
    {
        [Key]
        [Display(Name ="Contact Id")]
        public int Id { get; set; }
        [StringLength(250,MinimumLength =10,ErrorMessage ="Length Message Between 10 to 250")]
        public string? Message { get; set; }
        [StringLength(250, MinimumLength = 10, ErrorMessage = "Length Respone Between 10 to 250")]
        public string? Respone { get; set; }
        public string? TypeAction { get; set; }
        public string? NameUser { get; set; }
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }
        [DataType(DataType.PhoneNumber)]
        public long PhoneNumber { get; set; }
        [StringLength(50, MinimumLength = 10, ErrorMessage = "Length Message Between 10 to 50")]

        public string? Subject { get; set;}
    }
}
