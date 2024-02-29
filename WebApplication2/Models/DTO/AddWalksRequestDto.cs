namespace Web_Api.Models.DTO
{
    public class AddWalksRequestDto
    {

        public string Name { get; set; }
        public string Description { get; set; }
        public double LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }


        public int DifficultyId { get; set; }
        public int RegionId { get; set; }
    }
}
