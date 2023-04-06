using azara.api.Helpers.Schedular;
using azara.api.Helpers.Service.POSService;
using azara.models.Responses.Notification.Response;
using CorePush.Apple;
using CorePush.Google;

namespace azara.api;

public class Startup
{
    public IConfiguration Configuration { get; }

    public static string CurrentLanguage { get; set; }
    public static string WebRoot { get; set; }

    public Startup(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
    {
        Configuration = configuration;
        WebRoot = webHostEnvironment.WebRootPath;

    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContextPool<AzaraContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DatabaseConnection")));

        services.AddControllers()
        .ConfigureApiBehaviorOptions(options =>
        {
            options.SuppressModelStateInvalidFilter = true;
            options.SuppressConsumesConstraintForFormFileParameters = true;
            options.SuppressInferBindingSourcesForParameters = true;
            options.SuppressMapClientErrors = true;
        });
        services.AddMvc();
        #region Notification Section
        services.AddTransient<INotificationService, NotificationService>();
        services.AddHttpClient<FcmSender>();
        services.AddHttpClient<ApnSender>();

        // Configure strongly typed settings objects
        var appSettingsSection = Configuration.GetSection("FcmNotification");
        services.Configure<FcmNotificationSetting>(appSettingsSection);
        #endregion Notification Section

        #region JWT Authentication

        var key = Encoding.ASCII.GetBytes(Configuration["AuthConfigs:Key"]);
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = Configuration["AuthConfigs:Issuer"],
                ValidAudience = Configuration["AuthConfigs:Audiance"],
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ClockSkew = TimeSpan.Zero
            };
        });

        #endregion JWT Authentication

        #region Initialization of Configs.

        services.Configure<EmailConfigs>(Configuration.GetSection("EmailConfigs"));
        services.Configure<AuthConfigs>(Configuration.GetSection("AuthConfigs"));
        services.Configure<AdminConfigs>(Configuration.GetSection("AdminConfigs"));
        services.Configure<ConnectionConfigs>(Configuration.GetSection("ConnectionStrings"));
        services.Configure<BaseUrlConfigs>(Configuration.GetSection("BaseUrlConfig"));

        #endregion Initialization of Configs.

        #region Swagger Configuration

        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Azara",
                Version = "v1",
                Description = "Azara API Gateway"
            });

            //options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            //{
            //    Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n
            //      Enter 'Bearer' [space] and then your token in the text input below.
            //      \r\n\r\nExample: 'Bearer 12345abcdef'",
            //    Name = "Authorization",
            //    In = ParameterLocation.Header,
            //    Type = SecuritySchemeType.ApiKey,
            //    Scheme = "Bearer"
            //});
        });

        #endregion Swagger Configuration


        #region Schedular Code
        //Cron Jobs for the App calling         
        services.AddHostedService<SchedularCaller>();
        #endregion Schedular code

        services.AddEndpointsApiExplorer();

        // services.AddMvc(option => option.EnableEndpointRouting = false);
        services.AddHttpContextAccessor();
        services.AddSession();
        services.AddOptions();
        services.AddSignalR();

        #region Language Setup

        services.AddLocalization(options => options.ResourcesPath = "Resources");
        services.Configure<RequestLocalizationOptions>(options =>
        {
            var supportedCultures = new[] { new CultureInfo(LanguageConsts.English) };
            options.DefaultRequestCulture = new RequestCulture(culture: LanguageConsts.English, uiCulture: LanguageConsts.English);
            options.SupportedCultures = supportedCultures;
            options.SupportedUICultures = supportedCultures;
            options.RequestCultureProviders.Insert(0, new CustomRequestCultureProvider(async context =>
            {
                var userLangs = context.Request.Headers["Accept-Language"].ToString();
                var firstLang = userLangs.Split(',').FirstOrDefault();
                return await Task.FromResult(new ProviderCultureResult(firstLang, firstLang));
            }));
        });

        #endregion Language Setup

        services.Configure<IISServerOptions>(options =>
        {
            options.AutomaticAuthentication = false;
            options.MaxRequestBodySize = 300000000;
        });

        #region Cores Policy


        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy",
                builder => builder
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed((host) => true)
                .AllowCredentials());
        });


        #endregion Cores Policy

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        });

        services.AddMvc();

        #region Registering Dependency Injections.

        services.AddScoped<ILogManager, LogManager>();
        services.AddSingleton<ICrypto, Crypto>();
        services.AddScoped<IMessages, Messages>();
        services.AddSingleton<ExceptionFilter>();
        services.AddScoped<SeedHelpers>();
        services.AddScoped<IUploadManager, UploadManager>();
        services.AddScoped<IFileManagerLogic, FileManagerLogic>();
        services.AddScoped<ICSVService, CSVService>();
        services.AddScoped<IService, CustomerService>();

        #endregion Registering Dependency Injections.

        services.AddAuthentication().AddFacebook(options =>
        {
            options.AppId = Configuration["Authentication:Facebook:AppId"];
            options.AppSecret = Configuration["Authentication:Facebook:AppSecret"];
        });

        #region Mapper Setup

        services.AddAutoMapper(typeof(MapperProfile));

        #endregion Mapper Setup
    }

    #region Configure Method

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    // CTRL + k,s

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, SeedHelpers seed)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();

        }

        var localizationOption = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
        app.UseRequestLocalization(localizationOption.Value);

        seed.Seed().Wait(); // Call Seed Method in Startup

        app.UseSwagger().UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint($"/swagger/v1/swagger.json", "Azara API Gateway");
        });

        app.UseRouting();
        app.UseCors("CorsPolicy");
        // app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapHub<SignalRHelpers>("/azara_api_notifications");
        });
    }

    #endregion Configure Method
}