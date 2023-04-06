namespace azara.admin.Models.Rewards.Response
{
    public class RewardDropdownResponse
    {
        public List<DropdownDetail> details { get; set; }
    }

    public class DropdownDetail
    {
        public string id { get; set; }
        public string name { get; set; }
        public object image { get; set; }
    }
}
