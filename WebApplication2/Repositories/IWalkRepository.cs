using WebApplication2.Models.Domain;

namespace Web_Api.Repositories
{
    public interface IWalkRepository
    {
        Task<Walk> CreateAsyc(Walk walk);
        Task<List<Walk>> GetallAsync(string? filterOn = null, string? filterQuery = null,
            string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 1000);

        Task<Walk?> GetByIdAsync(int id);
        Task<Walk?> UpdateAsync(int id,Walk walk);
        Task<Walk?> DeleteAsync(int id);



    }
}
