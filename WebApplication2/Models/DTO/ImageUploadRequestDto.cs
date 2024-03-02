using System.ComponentModel.DataAnnotations;

namespace Web_Api.Models.DTO
{
    public class ImageUploadRequestDto
    {
        public int Id{ get; set; }
        [Required]
        public IFormFile  File { get; set; }
        [Required]

        public string FileName{ get; set; }
        public string? FileDescription { get; set; }

    }
}
