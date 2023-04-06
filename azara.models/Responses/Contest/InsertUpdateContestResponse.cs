namespace azara.models.Responses.Contest
{
    public class InsertUpdateContestResponse
    {
        public Guid? Id { get; set; }

        [Required]
        public string ContestName { get; set; }

        [Required]
        public string Image { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string? ContestType { get; set; }

        [Required]
        public bool Active { get; set; }

        public string? Latitude { get; set; }

        public string? Longitude { get; set; }
    }
}
