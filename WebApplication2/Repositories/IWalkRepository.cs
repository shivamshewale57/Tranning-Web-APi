using WebApplication2.Models.Domain;

namespace Web_Api.Repositories
{
    public interface IWalkRepository
    {
        Task<Walk> CreateAsyc(Walk walk);
        Task<List<Walk>> GetallAsync();

    }
}
