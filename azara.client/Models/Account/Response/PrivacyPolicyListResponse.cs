namespace azara.client.Models.Account.Response
{
    public class PrivacyPolicyListResponse
    {
        public List<PrivacyPolicyListData> details { get; set; }
    }

    public class PrivacyPolicyListData
    {

        public string id { get; set; }
        public string description { get; set; }
        //public DateTime modified { get; set; }
        //ublic DateTime created { get; set; }
        public bool active { get; set; }
    }
}
