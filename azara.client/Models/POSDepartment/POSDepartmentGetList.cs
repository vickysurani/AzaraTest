namespace azara.client.Models.POSDepartment
{
    public class POSDepartmentGetList
    {
        public int Id { get; set; }
        public string? ListId { get; set; }
        public List<DepartmentListResponse> Details { get; set; }
    }
    public class DepartmentListResponse
    {
        public string? ListId { get; set; }
        public string? DepartmentName { get; set; }
        public string? Image { get; set; }
    }
}
