namespace azara.models.Configs;

public class PushConfigs
{
    public Android Android { get; set; }

    public Apple Apple { get; set; }
}

public class Android
{
    public string HostName { get; set; }

    public string ApplicationId { get; set; }

    public string SenderId { get; set; }

    public string AppName { get; set; }

    public string Icon { get; set; }

    public string ClickAction { get; set; }
}

public class Apple
{
    public string CertificatePath { get; set; }

    public string Password { get; set; }

    public string HostName { get; set; }
}