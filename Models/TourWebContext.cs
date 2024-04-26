using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using TourWeb.Models;
namespace TourWeb.Models
{
    public class TourWebContext : DbContext
    {
        public TourWebContext(DbContextOptions<TourWebContext> options)
             : base(options)
        {

        }
        public DbSet<Account> Account { get; set; }
        public DbSet<New> New { get; set; }
        public DbSet<BookTour> BookTour { get; set; }
        public DbSet<Cart> Cart { get; set; }
        public DbSet<CartDetail> CartDetail { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Contact> Contact { get; set; }
        public DbSet<Itinerary> Itinerary { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderDetail> OrderDetail { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Tour> Tour { get; set; }
        public DbSet<Trekking> Trekking { get; set; }
        public DbSet<Image> Image { get; set; }
        public DbSet<OrderInfo> OrderInfo { get; set; }
        //cach them bang tu class
        //vao Package Manage Console->Add-Migration InitialCreate->Update-Database

    }
}
