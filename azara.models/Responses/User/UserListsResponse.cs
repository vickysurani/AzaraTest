namespace azara.models.Responses.User
{
    public class UserListsResponse
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string EmailId { get; set; }

        public bool EmailVerified { get; set; }

        public string MobileNumber { get; set; }

        public string Image { get; set; }

        public Guid CreatedBy { get; set; }

        public Guid ModifiedBy { get; set; }

        public DateTime Created { get; set; }

        public DateTime Modified { get; set; }

        public int Count { get; set; }

        public string ReferraCode { get; set; }

        public bool Active { get; set; }

        public int? Points { get; set; }

    }
}
