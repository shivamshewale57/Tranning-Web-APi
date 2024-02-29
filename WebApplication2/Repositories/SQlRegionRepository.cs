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

        public async Task<Region?> CreateAsync(Region region)
        {
            await dbContext.Regions.AddAsync(region);
            await dbContext.SaveChangesAsync();
            return region;
        }

        public async Task<Region?> DeleteAsync(int id)
        {
            var extingRegion = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (extingRegion != null)
            {
                return null;
            }
            dbContext.Regions.Remove(extingRegion);
            await dbContext.SaveChangesAsync();
            return extingRegion;
        }

        public async Task<List<Region>> GetAllAsync()
        {
            return await dbContext.Regions.ToListAsync();
        }

        public async Task<Region?> GetByIdAsync(int id)
        {
            return await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Region?> UpdateAsync(int id, Region region)
        {
            var existingRegion = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (existingRegion == null)
            {
                return null;
            }
            existingRegion.Code = region.Code;
            existingRegion.Name = region.Name;
            existingRegion.RegionImageUrl = region.RegionImageUrl;
            await dbContext.SaveChangesAsync();
            return existingRegion;
        }


    }
}
