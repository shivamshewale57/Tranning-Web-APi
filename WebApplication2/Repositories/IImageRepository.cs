using Web_Api.Models.Domain;

namespace Web_Api.Repositories
{
    public interface IImageRepository
    {

        //// defination for upload
        Task<Image>Upload (Image image);
    }
}
