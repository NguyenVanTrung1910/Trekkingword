using System.ComponentModel.DataAnnotations;

namespace TourWeb.Models
{
    public class Account
    {
        [Key] 
        [Display(Name = "AccoundID")]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string? Name { get; set; }

        [EmailAddress(ErrorMessage ="Invalid Email Address")]
        public string? Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(30,MinimumLength =8,ErrorMessage = "Password Must Be Between 8 and 30 Characters.")]
        public string? Password { get; set; }

        [DataType(DataType.PhoneNumber)]
        public long PhoneNumber { get; set; }

        [StringLength(80)]
        public string? Address { get; set; }

        public string Role { get; set; } = "User";

        [StringLength(250)]
        public string? Detail { get; set; }

        [StringLength(80)]
        public string? BankName { get; set; }
        public long BankNumber { get; set; }
        public decimal Monney { get; set;}
        public Cart? Carts {  get; set; }
        [StringLength (10)]
        public string? Sex { get; set; }
        public string? ImagePath { get; set; }
    }
}
