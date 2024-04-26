using System.ComponentModel.DataAnnotations;

namespace TourWeb.Models
{
    public class Itinerary
    {
        [Key]
        [Display(Name = "Itinerary Id")]
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Day { get; set; }
        public string? Detail { get; set; }
        public string? Accomodation { get; set; }
        public string? Meals { get; set; }
        public int TourId { get; set; }
        public Tour? Tours { get; set; }
        public int TrekkingId { get; set; }
        public Trekking? Trekkings { get; set; }
        public ICollection<Image>? Images { get; set; }
    }
}
