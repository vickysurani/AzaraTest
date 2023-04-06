namespace azara.models.Requests.Base
{
    public class BaseGetByIdRequest
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public int PageNo { get; set; }

        [Required]
        public int PageSize { get; set; }
    }
}
