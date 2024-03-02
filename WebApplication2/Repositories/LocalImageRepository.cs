using Web_Api.Data;
using Web_Api.Models.Domain;

namespace Web_Api.Repositories
{
    public class LocalImageRepository : IImageRepository
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly WebApiDbContext dbContext;

        // consructor to inject web hosting enviormrent
        public LocalImageRepository(IWebHostEnvironment webHostEnvironment,IHttpContextAccessor httpContextAccessor,WebApiDbContext dbContext)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.httpContextAccessor = httpContextAccessor;
            this.dbContext = dbContext;
        }
        public async Task<Image> Upload(Image image)
        {
            // create local path variable to point our location
            var localFilePath = Path.Combine(webHostEnvironment.ContentRootPath, "Images",
               $"{image.FileName}{image.FileExtention}");

            //Upload Image to local path
            using var stream = new FileStream(localFilePath, FileMode.Create);
            await image.File.CopyToAsync(stream);

            //path : https://Localhost:1234/images/images.jpg
            var urlFilePath = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}{httpContextAccessor.HttpContext.Request.PathBase}" +
                $"/Images/{image.FileName}{image.FileName}{image.FileExtention}";

            image.FilePath = urlFilePath;   

            // add Image to the Image

            await dbContext.Images.AddAsync(image);
            await dbContext.SaveChangesAsync();

            return image;
        }
    }
}
