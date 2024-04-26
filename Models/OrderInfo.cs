using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace TourWeb.Models
{
    public class OrderInfo
    {
        [Key]
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Company {  get; set; }
        public string? StreetAddress { get; set; }
        public string? City { get; set; } 
        public string? Country { get; set; }
        public int Zip {  get; set; }
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string ? Email {  get; set; }
        [DataType(DataType.PhoneNumber)]
        public long Phone {  get; set; }
        public Order? Orders { get; set; }
    }
}
