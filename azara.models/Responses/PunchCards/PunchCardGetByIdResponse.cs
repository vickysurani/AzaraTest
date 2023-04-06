namespace azara.models.Responses.PunchCards
{
    public class PunchCardGetByIdResponse
    {
        public string PunchCardName { get; set; }


        public string? Image { get; set; }


        public string Description { get; set; }


        public string PunchCardPromos { get; set; }


        public DateTime? ExpiryDate { get; set; }


        public bool Active { get; set; }
    }
}
