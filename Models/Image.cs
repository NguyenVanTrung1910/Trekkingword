using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace TourWeb.Models
{
    public class Image
    {
        [Key]
        [Display(Name = "Image Id")]
        public int Id { get; set; }
        [Required]
        public string? ImagePath { get; set; }
        public int? NewId { get; set; }
        public New? News { get; set; }
        public int? ProductId { get; set; }
        public Product? Products { get; set; }
        public int? TourId { get; set; }
        public Tour? Tours { get; set; }
        public int? TrekkingId { get; set; }
        public Trekking? Trekkings { get; set; }
        public int? ItineraryId { get; set; }
        public Itinerary? Itinerarys { get; set; }
    }
}
