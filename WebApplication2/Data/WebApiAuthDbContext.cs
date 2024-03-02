using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Web_Api.Data
{
    public class WebApiAuthDbContext : IdentityDbContext
    {
        public WebApiAuthDbContext(DbContextOptions <WebApiAuthDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            var redearRoleID ="1";
            var writerRoleId = "1";
            



            var roles = new List<IdentityRole>
            {

                new IdentityRole()
                {
                    Id =redearRoleID,
                    ConcurrencyStamp=redearRoleID,
                    Name="Reader",
                    NormalizedName="Reader".ToUpper()

                },
                new IdentityRole()
                {
                    Id=writerRoleId,
                    ConcurrencyStamp=writerRoleId,
                    Name="Writer",
                    NormalizedName="Writer".ToUpper(),
                }
            };
            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
