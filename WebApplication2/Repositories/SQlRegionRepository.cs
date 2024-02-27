using Microsoft.EntityFrameworkCore;
using Web_Api.Data;
using WebApplication2.Models.Domain;

namespace Web_Api.Repositories
{
    public class SQlRegionRepository : IRegionRepository
    {
        private readonly WebApiDbContext dbContext;

        public SQlRegionRepository(WebApiDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<Region>> GetAllAsync()
        {
           return  await dbContext.Regions.ToListAsync();
        }
    }
}
