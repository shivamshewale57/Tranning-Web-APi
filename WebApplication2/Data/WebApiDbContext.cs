using Microsoft.EntityFrameworkCore;
using WebApplication2.Models.Domain;

namespace Web_Api.Data
{
    public class WebApiDbContext : DbContext
    {
        public WebApiDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {



        }
        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> regions { get; set; }
        public DbSet<Walk> walks { get; set; }
    }
}