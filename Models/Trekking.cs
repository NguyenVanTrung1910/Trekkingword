﻿using System.ComponentModel.DataAnnotations;

namespace TourWeb.Models
{
    public class Trekking
    {
        [Key]
        [Display(Name = "BookTour Id")]
        public int Id { get; set; }
        public string? Name { get; set; }
        [StringLength(50)]
        public string? Description { get; set; }
        public string? Detail { get; set; }
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal Price { get; set; }
        public decimal Review { get; set; }
        [StringLength(10)]
        public string? Address { get; set; }
        [DataType(DataType.Duration)]
        public string? Duration { get; set; }
        public string? Feature { get; set; }
        public decimal  Height { get; set; }
        public string? Difficulty { get; set; }
        public string? ImageSpecial { get; set; }
        public string? Area { get; set; }
        public ICollection<BookTour>? BookTours { get; set; }
        public ICollection<Itinerary>? Itineraries { get; set; }
        public ICollection<Image>? ImageGarelly { get; set; }

    }
}
