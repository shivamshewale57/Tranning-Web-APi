using System.ComponentModel.DataAnnotations;

namespace Web_Api.Models.DTO
{
    public class UpdateRegionRequestDto
    {
        [Required]
        [MinLength(3, ErrorMessage = " Code has to be minimum of 3 caracters")]
        [MaxLength(3, ErrorMessage = " Code has to be maximum  of 3 caracters")]
        public string Code { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = " Name has to be minimum of 100 caracters")]
        public string Name { get; set; }
        public string? RegionImageUrl { get; set; }
    }
}
