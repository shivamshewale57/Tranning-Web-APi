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

        public async Task<Walk?> DeleteAsync(int id)
        {
           var existingWalk = await dbContext.Walks.FirstOrDefaultAsync(x=>x.Id==id);
            if (existingWalk == null)
            {
                return null;
            }

            dbContext.Walks.Remove(existingWalk);
            await dbContext.SaveChangesAsync();
            return existingWalk;
        }

        public async Task<List<Walk>> GetallAsync()
        {
            return await dbContext.Walks.Include("Difficulty").Include("Region").ToListAsync();
        }

        public async Task<Walk?> GetByIdAsync(int id)
        {
            return await dbContext.Walks.
                Include("Difficulty").
                Include("Region").
                FirstOrDefaultAsync(x => x.Id == id);

        }

        public async Task<Walk?> UpdateAsync(int id, Walk walk)
        {
          var existingWalk= await dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
            if (existingWalk == null)
            {
                return null;
            }
            existingWalk .Name = walk.Name;
            existingWalk.Description = walk.Description;
            existingWalk.LengthInKm = walk.LengthInKm;
            existingWalk.WalkImageUrl = walk.WalkImageUrl;
            existingWalk.DifficultyId = walk.DifficultyId;
            existingWalk.RegionId = walk.RegionId;

            await dbContext.SaveChangesAsync();
            return existingWalk;



        }
    } 
}
