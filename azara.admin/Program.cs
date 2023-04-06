using Syncfusion.Blazor;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NTkyMTQ5QDMxMzkyZTM0MmUzMEVXUE9pUll5VTdDZ1o4YXhuVWdLY2NuVlZXWnhmTnZJdzR1NEt3V1JaUEE9");
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<AccountHelper>();
builder.Services.AddScoped<ProductHelpers>();
builder.Services.AddScoped<IFileUpload, FileUpload>();
builder.Services.AddScoped<IUploadManager, UploadManager>();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddSyncfusionBlazor(options => { options.IgnoreScriptIsolation = true; });
builder.Services.AddSignalR();
builder.Services.AddServerSideBlazor().AddCircuitOptions(e =>
{
    e.DetailedErrors = true;
    e.DisconnectedCircuitMaxRetained = 100;
    e.DisconnectedCircuitRetentionPeriod = TimeSpan.FromMinutes(3);
    e.JSInteropDefaultCallTimeout = TimeSpan.FromMinutes(1);
    e.MaxBufferedUnacknowledgedRenderBatches = 10;
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();