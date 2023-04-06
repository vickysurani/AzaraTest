namespace azara.models.Requests.PointManagement
{
    public class PointManagementListRequest
    {
        public string Name { get; set; } = string.Empty;

        public bool? IsDeleted { get; set; } = false;
    }
}
