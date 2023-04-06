using System.Net;

namespace azara.models.Responses.BlueCheck;

public class BlueCheckVerificationResponse
{
    public string result { get; set; }

    public string orderToken { get; set; }
}

public class BlueCheckResponse
{
    public BlueCheckVerificationResponse BlueCheckData { get; set; }

    public HttpStatusCode StatusCode { get; set; }
}