namespace azara.models.Requests.PunchCard
{
    public class PunchCardInsertUpdateRequest
    {
        public string? Id { get; set; }

        public string PunchCardName { get; set; }


        public string? Image { get; set; }


        public string Description { get; set; }


        public string PunchCardPromos { get; set; }


        public DateTime? ExpiryDate { get; set; }


        public bool Active { get; set; }

        public DateTime Modified { get; set; }
    }
}
