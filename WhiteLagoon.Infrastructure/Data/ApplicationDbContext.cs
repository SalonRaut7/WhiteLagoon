using Microsoft.EntityFrameworkCore;
using WhiteLagoon.Domain.Entities;

namespace WhiteLagoon.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        //constructor : Whatever parameters(DbContextOptions<ApplicationDbContext> options) we pass to the constructor, it will be passed to the base class constructor
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
             
        }
        public DbSet<Villa> Villas { get; set; }
        public DbSet<VillaNumber> VillaNumbers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // base.OnModelCreating(modelBuilder);
            //seeding data to the database
            modelBuilder.Entity<Villa>().HasData(
                new Villa
                {
                    Id = 1,
                    Name = "Royal Villa",
                    Description = "This is the Royal Villa with all the luxurious amenities and breathtaking views.",
                    ImageUrl = "https://placehold.co/600x400",
                    Occupancy = 4,
                    Price = 200,
                    Sqft = 550,
                },
                new Villa
                {
                    Id = 2,
                    Name = "Beachfront Villa",
                    Description = "Experience the ultimate beachfront living in this stunning villa with direct access to the beach.",
                    ImageUrl = "https://placehold.co/600x401",
                    Occupancy = 4,
                    Price = 300,
                    Sqft = 550,
                },
                new Villa
                {
                    Id = 3,
                    Name = "Luxury Pool Villa",
                    Description = "Indulge in the lap of luxury at this magnificent pool villa, complete with a private pool and stunning views.",
                    ImageUrl = "https://placehold.co/600x402",
                    Occupancy = 4,
                    Price = 400,
                    Sqft = 750,
                });
            modelBuilder.Entity<VillaNumber>().HasData(
                new VillaNumber
                {
                    Villa_Number = 101,
                    VillaId = 1,
                    SpecialDetails = "This villa number is located on the ground floor and has a beautiful garden view."
                },
                new VillaNumber
                {
                    Villa_Number = 102,
                    VillaId = 1,
                    SpecialDetails = "This villa number is located on the first floor and offers stunning ocean views."
                },
                new VillaNumber
                {
                    Villa_Number = 201,
                    VillaId = 2,
                    SpecialDetails = "This villa number is located on the ground floor and has direct access to the beach."
                },
                new VillaNumber
                {
                    Villa_Number = 202,
                    VillaId = 2,
                    SpecialDetails = "This villa number is located on the first floor and offers panoramic ocean views."
                },
                new VillaNumber
                {
                    Villa_Number = 301,
                    VillaId = 3,
                    SpecialDetails = "This villa number is located on the ground floor and features a private pool with a stunning view."
                },
                new VillaNumber
                {
                    Villa_Number = 302,
                    VillaId = 3,
                    SpecialDetails = "This villa number is located on the first floor and offers breathtaking views of the surrounding landscape."   
                }
            );
        }
    }
}