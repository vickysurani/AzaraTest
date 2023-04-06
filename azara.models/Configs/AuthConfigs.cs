namespace azara.models.Configs;

public class AuthConfigs
{
    public string Key { get; set; }

    public string Issuer { get; set; }

    public string Audiance { get; set; }

    public int AccessExpireDays { get; set; }

    public int RefreshExpireDays { get; set; }

    public string AuthTokenType { get; set; }
}