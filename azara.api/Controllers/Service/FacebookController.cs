namespace azara.api.Controllers.Service;

public class FacebookController : BaseController
{
    #region Object Declaration And Constructor

    public FacebookController(
        IConfiguration configrations,
        IStringLocalizer<BaseController> Localizer,
        ICrypto Crypto,
        AzaraContext DbContext,
        IMapper Mapper,
        IFileManagerLogic FileManagerLogic,
        ICSVService CSVService) : base(
            Localizer,
            Crypto,
            DbContext,
            Mapper,
            FileManagerLogic,
            CSVService)
    {
    }

    #endregion Object Declaration And Constructor
    [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Client)]
    [HttpGet(ActionsConsts.FaceBook.FacebookLogin)]
    public async Task<IActionResult> FacebookSignIn(string returnURL)
    {
        //await HttpContext.ChallengeAsync(FacebookDefaults.AuthenticationScheme, new AuthenticationProperties { RedirectUri = "https://google.com" });
        return Challenge(
                new AuthenticationProperties()
                {
                    RedirectUri = Url.Action(nameof(FacebookLoginCallback), new { returnURL })
                },
                FacebookDefaults.AuthenticationScheme
            );
    }

    [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Client)]
    [HttpGet]
    [Route("facebook-login-callback")]
    public async Task<IActionResult> FacebookLoginCallback(string returnURL)
    {
        var authenticationResult = await HttpContext
        .AuthenticateAsync(FacebookDefaults.AuthenticationScheme);
        if (authenticationResult.Succeeded)
        {
            return Redirect($"{returnURL}?externalauth=true");
        }
        return Redirect($"{returnURL}?externalauth=false");
    }
}