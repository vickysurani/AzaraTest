namespace azara.api.Helpers;

public class UserTokenHelpers : IDisposable
{
    #region Object Declarations and Constructor

    private ICrypto Crypto { get; set; }

    public UserTokenHelpers(ICrypto Crypto) => this.Crypto = Crypto;

    #endregion Object Declarations and Constructor

    #region Get Access Token

    public string GetAccessToken(AuthConfigs AuthConfig,
        UserSignInResponse response)
    {
        Claim[] claims = new[] {
            new Claim(JwtRegisteredClaimNames.Sid, Crypto.Encrypt(response.Id)),
            new Claim(JwtRegisteredClaimNames.Email, Crypto.Encrypt(response.EmailId)),
            new Claim(JwtRegisteredClaimNames.GivenName, Crypto.Encrypt(response.FirstName)),
            //new Claim("Username", Crypto.Encrypt(response.FirstName)),
            new Claim(JwtRegisteredClaimNames.Jti, Crypto.Encrypt(response.UniqueId)),
        };
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AuthConfig.Key));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(AuthConfig.Issuer, AuthConfig.Audiance, expires: DateTime.Now.AddDays(AuthConfig.AccessExpireDays), signingCredentials: creds, claims: claims);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    #endregion Get Access Token

    #region Dispose

    public void Dispose() => GC.SuppressFinalize(this);

    #endregion Dispose
}