using Microsoft.EntityFrameworkCore;
using System.Linq;
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

        public async Task<List<Walk>> GetallAsync(string? filterOn = null, string? filterQuery = null, 
            string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 1000)
        {
            //return await dbContext.Walks.Include("Difficulty").Include("Region").ToListAsync();
            var walks =  dbContext.Walks.Include("Difficulty").Include("Region").AsQueryable();

            // filtering
            if (string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery)==false)
            {
                if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = walks.Where(x => x.Name.Contains(filterQuery));

                }
               
            }
            //sorting 
            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks= isAscending?walks.OrderBy(x => x.Name):walks.OrderByDescending(x => x.Name);
                }
                else if (sortBy.Equals("Length", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending ? walks.OrderBy(x => x.LengthInKm) : walks.OrderByDescending(x => x.LengthInKm);
                }
            }

            //Pagination
            var skipResult=(pageNumber -1) * pageSize;

            
            return await walks.Skip(skipResult ).Take(pageSize).ToListAsync();
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
