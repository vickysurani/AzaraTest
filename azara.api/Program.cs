namespace azara.apis;

public class Program
{
#if DEBUG
    public static string environmentName = "Debug";
#elif RELEASE
    public static string environmentName = "Release";
#elif UAT
    public static string environmentName = "Uat";
#endif

    public static void Main(string[] args)
    {
        BuildWebHost(args).Run();
    }

    public static IWebHost BuildWebHost(string[] args) =>
         WebHost.CreateDefaultBuilder(args)
            .UseEnvironment(environmentName)
            .UseStartup<Startup>()
            .Build();
}