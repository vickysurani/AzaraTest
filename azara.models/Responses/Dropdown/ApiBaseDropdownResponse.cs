namespace azara.models.Responses.Dropdown;

public class ApiBaseDropdownResponse
{
    public string Id { get; set; }

    public string Name { get; set; }

    public string? Image { get; set; }
}

public class ApiBaseDropdownDetailResponse
{
    public List<ApiBaseDropdownResponse> details { get; set; }
}