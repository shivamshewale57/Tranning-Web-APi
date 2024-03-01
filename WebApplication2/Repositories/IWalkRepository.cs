using WebApplication2.Models.Domain;

namespace Web_Api.Repositories
{
    public interface IWalkRepository
    {
        Task<Walk> CreateAsyc(Walk walk);
        Task<List<Walk>> GetallAsync();

        Task<Walk?> GetByIdAsync(int id);
        Task<Walk?> UpdateAsync(int id,Walk walk);
        Task<Walk?> DeleteAsync(int id);



    }
}
