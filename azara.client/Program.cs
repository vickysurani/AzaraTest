
using Syncfusion.Blazor;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");


Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NTkyMTQ5QDMxMzkyZTM0MmUzMEVXUE9pUll5VTdDZ1o4YXhuVWdLY2NuVlZXWnhmTnZJdzR1NEt3V1JaUEE9");
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
//builder.Services.AddWMBSC(false);
builder.Services.AddBlazoredLocalStorage();
//builder.Services.AddScoped<IDataService, DataService>();
builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();
builder.Services.AddLocalization();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSyncfusionBlazor(options => { options.IgnoreScriptIsolation = true; });
builder.Services.AddScoped<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<IFileUpload, FileUpload>();
await builder.Build().RunAsync();