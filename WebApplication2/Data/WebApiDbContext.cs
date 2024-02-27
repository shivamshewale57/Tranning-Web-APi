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
        public DbSet<Walk> walks { get; set; }
    }
}