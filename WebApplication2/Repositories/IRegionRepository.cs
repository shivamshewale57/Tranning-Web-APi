﻿using WebApplication2.Models.Domain;

namespace Web_Api.Repositories
{
    public interface IRegionRepository
    {
        Task<List<Region>> GetAllAsync();
        Task<Region?> GetByIdAsync(int id);
        Task<Region?> CreateAsync(Region region);

        Task<Region?>UpdateAsync(int id,Region region);

        Task<Region?> DeleteAsync(int id);

    }
}
