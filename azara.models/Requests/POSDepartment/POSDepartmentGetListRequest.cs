namespace azara.models.Requests.POSDepartment
{
    public class POSDepartmentGetListRequest
    {
        public int PageNo { get; set; }

        public int PageSize { get; set; }

        public string? OrderBy { get; set; }

        public string SearchParam { get; set; }

    }
}
