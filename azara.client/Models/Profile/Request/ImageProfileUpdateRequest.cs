namespace azara.client.Models.Profile.Request;

public class ImageProfileUpdateRequest
{
    [Required(ErrorMessageResourceName = "bad_response_select_file", ErrorMessageResourceType = typeof(Massage))]
    public IBrowserFile File { get; set; }

    [Required]
    public string FileName { get; set; }

    public string? OldFileName { get; set; }
}