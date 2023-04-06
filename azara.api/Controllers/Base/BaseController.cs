namespace azara.api.Controllers.Base;

[ServiceFilter(typeof(ExceptionFilter))]
[Route(ActionsConsts.ApiVersion)]
[Produces("application/json")]
[EnableCors("CorsPolicy")]
[ApiController]
public class BaseController : Controller
{
    #region Constructor and Object Declaration

    public IRepository<AzaraContext, UserEntity> UserEntityRepository;

    protected IStringLocalizer<BaseController> Localizer { get; set; }

    protected ICrypto Crypto { get; set; }

    public AzaraContext DbContext { get; set; }

    public IMapper Mapper { get; set; }

    public IFileManagerLogic FileManagerLogic { get; set; }

    public ICSVService CSVService { get; set; }

    public BaseController(
       IStringLocalizer<BaseController> Localizer,
       ICrypto Crypto,
       AzaraContext DbContext,
       IMapper Mapper,
       IFileManagerLogic fileManagerLogic,
       ICSVService cSVService)
    {
        this.Localizer = Localizer;
        this.Crypto = Crypto;
        this.DbContext = DbContext;
        this.Mapper = Mapper;
        FileManagerLogic = fileManagerLogic;
        CSVService = cSVService;
    }

    #endregion Constructor and Object Declaration

    #region 1.Get current user's data using Token

    protected bool IsLoggedIn(ClaimsPrincipal User) => User.Identity.IsAuthenticated;

    protected Guid GetUserId(ClaimsPrincipal User)
    {
        var user_id = User.Claims.Where(x => x.Type == JwtRegisteredClaimNames.Sid)?.FirstOrDefault().Value;

        if (string.IsNullOrEmpty(user_id)) return Guid.Empty;  // Early return

        return Guid.Parse(Crypto.Decrypt(user_id));
    }

    protected string GetUserEmail(ClaimsPrincipal User)
    {
        var email = User.Claims.Where(x => x.Type == ClaimTypes.Email)?.FirstOrDefault().Value;

        if (string.IsNullOrEmpty(email)) return String.Empty;  // Early return

        return Crypto.Decrypt(email);
    }

    protected string GetUserName(ClaimsPrincipal User)
    {
        var email = User.Claims.Where(x => x.Type == ClaimTypes.GivenName)?.FirstOrDefault().Value;

        if (string.IsNullOrEmpty(email)) return String.Empty;  // Early return

        return Crypto.Decrypt(email);
    }

    protected string GetUniqueId(ClaimsPrincipal User)
    {
        var uniqueId = User.Claims.Where(x => x.Type == JwtRegisteredClaimNames.Jti)?.FirstOrDefault().Value;

        if (string.IsNullOrEmpty(uniqueId)) return String.Empty;  // Early return

        return Crypto.Decrypt(uniqueId);
    }

    protected string GetUserFullName(ClaimsPrincipal User)
    {
        var username = User.Claims.Where(x => x.Type.Equals(ClaimTypes.GivenName))?.FirstOrDefault().Value;

        return Crypto.Decrypt(username);
    }

    protected string GetUserRole(ClaimsPrincipal User)
    {
        var role = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value;

        if (string.IsNullOrEmpty(role)) return String.Empty;  // Early return

        return Crypto.Decrypt(role);
    }

    #endregion 1.Get current user's data using Token

    #region 2. OkResponse

    protected IActionResult OkResponse() => Ok(new
    {
        meta = new
        {
            message = Localizer["ok_response_successful"].Value,
            statusCode = 200
        }
    });

    protected IActionResult OkResponse(string message) => Ok(new
    {
        meta = new { message = Localizer[message].Value, statusCode = 200 }
    });

    protected IActionResult OkResponse(string message, object data) => Ok(new
    {
        meta = new { message = Localizer[message].Value, statusCode = 200 },
        data = data
    });

    protected IActionResult OkResponse(object data) => Ok(new
    {
        meta = new { message = Localizer["ok_response_successful"].Value, statusCode = 200 },
        data = data
    });

    protected IActionResult OkResponse(IEnumerable<dynamic> details)
    {
        var data = new { details };
        return Ok(new
        {
            meta = new { message = Localizer["ok_response_successful"].Value, statusCode = 200 },
            data
        });
    }

    #endregion 2. OkResponse

    #region 3. Failed Response

    protected IActionResult ErrorResponse() => throw new ApiException("error_something_went_wrong", 400);

    protected IActionResult ErrorResponse(string message) => throw new ApiException(message, 400);

    protected IActionResult ErrorResponse(string message, int statusCode) => throw new ApiException(message, statusCode);

    protected IActionResult ErrorResponse(Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary ModelState)
    {
        string message = string.Join("; ", ModelState.SelectMany(x => x.Value.Errors).Select(x => Localizer[x.ErrorMessage].Value));
        throw new ApiException(message, 400);
    }

    protected IActionResult ErrorResponse(Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary ModelState, int statusCode)
    {
        string message = string.Join("; ", ModelState.SelectMany(x => x.Value.Errors).Select(x => Localizer[x.ErrorMessage].Value));
        throw new ApiException(message, statusCode);
    }

    #endregion 3. Failed Response

    #region Common Methods

    protected void CheckHelperResponse(BaseResponse response)
    {
        if (response is null) ErrorResponse();
        if (!response.IsSuccess) ErrorResponse(response.ErrorMessage);
    }

    #endregion Common Methods

    #region Validate Image Extension

    protected void ValidateImageExtension(string filename)
    {
        filename = Path.GetExtension(filename);
        string[] supportedTypes = new[] { "jpg", "jpeg", "png", "gif" };

        for (int i = 0; i < supportedTypes.Length; i++)
        {
            if (filename == "." + supportedTypes[i]) return;
        }
        ErrorResponse("bad_response_image_extension_error_access");
    }

    protected void ValidateGifExtension(string filename)
    {
        if (filename.Length >= 1024)
        {
            ErrorResponse("bad_response_gif_size_extension_error_access");
        }

        filename = Path.GetExtension(filename);

        if (filename == ".gif") return;
        ErrorResponse("bad_response_gif_extension_error_access");
    }

    #endregion Validate Image Extension
}