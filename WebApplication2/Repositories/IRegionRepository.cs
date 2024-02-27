using WebApplication2.Models.Domain;

namespace Web_Api.Repositories
{
    public interface IRegionRepository
    {
        Task<List<Region>> GetAllAsync();

    }
}
