using Microsoft.EntityFrameworkCore;
using Web_Api.Data;
using WebApplication2.Models.Domain;

namespace Web_Api.Repositories
{
    public class SQLWalkRepository : IWalkRepository
    {
        private readonly WebApiDbContext dbContext;

        public SQLWalkRepository(WebApiDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Walk> CreateAsyc(Walk walk)
        {
            
            {
                await dbContext.Walks.AddAsync(walk);
                await dbContext.SaveChangesAsync();
                return walk;
            }

            
            
        }

        public async Task<List<Walk>> GetallAsync()
        {
            return await dbContext.Walks.ToListAsync();
        }
    }
}
