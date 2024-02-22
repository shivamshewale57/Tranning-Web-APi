namespace WebApplication2.Models.Domain
{
    public class Walk
    {
        public Guid Id { get; set; }
        public string Name{ get; set; }
        public string Description { get; set; }
        public double LengthInKm{ get; set; }
        public string? WalkImageUrl{ get; set; }

        // make realtionship between these models 
        public Guid DefficultyId { get; set; }
        public Guid RegionId { get; set; }

        //navigation

        public Difficulty Difficulty { get; set; }
        public Region Region { get; set; }

    }
}
