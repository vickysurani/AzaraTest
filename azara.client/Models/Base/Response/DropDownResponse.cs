namespace azara.client.Models.Base.Response;

public class DropDownResponse
{
    public string Id { get; set; }

    public string name { get; set; } = string.Empty;
}

public class DropDownListDetailResponse
{
    public List<DropDownResponse> details { get; set; }
}