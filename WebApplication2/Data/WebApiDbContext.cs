using Microsoft.EntityFrameworkCore;
using WebApplication2.Models.Domain;

namespace Web_Api.Data
{
    public class WebApiDbContext : DbContext
    {
        public WebApiDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {



        }
        //create property of each
        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }

        /// implementing seed
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // seed data for Defficulties
            // Easy, Medium, Hard

            var difficulties = new List<Difficulty>();

            {
                new Difficulty()
                {
                    Id = 1,
                    Name = "Easy"
                };
                new Difficulty()
                {
                    Id = 2,
                    Name = "Medium"
                };
                new Difficulty()
                {
                    Id = 3,
                    Name = "Hard"
                };
            };

            //seed dificulties to database
            modelBuilder.Entity<Difficulty>().HasData(difficulties);

            // seed data for Regions
            var regions = new List<Region>
            {

                 new Region
                {
                    Id = 1,
                    Name = "Auckland",
                    Code = "AKL",
                    RegionImageUrl = "https://images.pexels.com/photos/5169056/pexels-photo-5169056.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
                },
                new Region
                {
                    Id = 2,
                    Name = "Northland",
                    Code = "NTL",
                    RegionImageUrl = null
                },
                new Region
                {
                    Id = 3,
                    Name = "Bay Of Plenty",
                    Code = "BOP",
                    RegionImageUrl = null
                },
                new Region
                {
                    Id = 4,
                    Name = "Wellington",
                    Code = "WGN",
                    RegionImageUrl = "https://images.pexels.com/photos/4350631/pexels-photo-4350631.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
                },
                new Region
                {
                    Id = 5,
                    Name = "Nelson",
                    Code = "NSN",
                    RegionImageUrl = "https://images.pexels.com/photos/13918194/pexels-photo-13918194.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
                },
                new Region
                {
                    Id = 6,
                    Name = "Southland",
                    Code = "STL",
                    RegionImageUrl = null
                },
            };
            modelBuilder.Entity<Region>().HasData(regions);

        }

    }
}