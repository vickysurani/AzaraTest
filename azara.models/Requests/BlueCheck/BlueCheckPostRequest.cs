namespace azara.models.Requests.BlueCheck;

public class BlueCheckPostRequest
{
    public string first_name { get; set; }

    public string last_name { get; set; }

    public string address { get; set; }

    public string email { get; set; }

    public string? country { get; set; } = "US";

    public string dob { get; set; } = DateTime.UtcNow.AddYears(-18).ToString("yyyy-MM-DD");

    public int? ZipCode { get; set; }
}

public class DocumentFiles
{
    public string id_front { get; set; }

    public string? id_selfie { get; set; }
}

public class BlueCheckVerifyModel
{
    public BlueCheckPostRequest userData { get; set; }

    public DocumentFiles files { get; set; }
}

public class BlueCheckVerifyBindingModel
{
    public string version { get; set; }

    public string domainToken { get; set; }

    public string verificationType { get; set; }

    public BlueCheckPostRequest userData { get; set; }

    public DocumentFiles files { get; set; }
}



public class MultipartFormDataModel
{
    public string Name { get; set; }
    public ByteArrayContent Stream { get; set; }
}