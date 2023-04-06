namespace azara.api.Helpers;

public class EmailHelpers
{
    #region Object Declarations And Constructor

    private IStringLocalizer<BaseController> Localizer { get; set; }

    private IMessages Messages { get; set; }

    private EmailConfigs emailConfigs { get; set; }

    public EmailHelpers(IStringLocalizer<BaseController> Localizer, IMessages Messages)
    {
        this.Localizer = Localizer;
        this.Messages = Messages;
    }

    #endregion Object Declarations And Constructor

    #region Send Account related mail

    //public void SendAdminSeedEmail(string email, string name, string url)
    //{
    //    SendEmail(email, $"{Localizer["email_admin_invite"].Value}", DesignEmail($"{Localizer["email_admin_invite"].Value}", $@"<tr><td style=""font-weight: 500; color: #292929; font-family: 'Nunito', sans-serif;"">Hi {name},</td></tr><tr><td style="" line-height: 0px;"" height=""16""></td></tr><tr><td style=""font-family: 'Nunito', sans-serif;"">You are invited to manage the {Localizer["app_name"].Value}. Click login button to manage {Localizer["app_name"].Value}</td></tr><tr><td style="" line-height: 0px;"" height=""32""></td></tr><tr align=""center""><td><a href=""{url}"" style=""font-family: 'Nunito', sans-serif; padding: 12px; min-width: 216px; display: inline-block; background: #F5B041; border-radius: 4px; font-size: 14px; line-height: 20px; color: #fff;"">Login</a></td></tr><tr><td style="" line-height: 0px;"" height=""32""></td></tr>"));
    //}

    //public void SendAdminInviteEmail(string email, string name, string username, string password, string url)
    //{
    //    SendEmail(email, $"{Localizer["email_admin_invite"].Value}", DesignEmail($"{Localizer["email_admin_invite"].Value}", $@"<tr><td style=""font-weight: 500; color: #292929; font-family: 'Nunito', sans-serif;"">Hi {name},</td></tr><tr><td style="" line-height: 0px;"" height=""16""></td></tr><tr><td style=""line-height: 0px;"" height=""16"">Username: <b>{username}</b></td></tr><tr><td style=""line-height: 0px;"" height=""16"">Password: <b>{password}</b></td></tr><tr><td style=""font-family: 'Nunito', sans-serif;"">You are invited to manage the {Localizer["app_name"].Value}. Click login button to manage {Localizer["app_name"].Value}</td></tr><tr><td style="" line-height: 0px;"" height=""32""></td></tr><tr align=""center""><td><a href=""{url}"" style=""font-family: 'Nunito', sans-serif; padding: 12px; min-width: 216px; display: inline-block; background: #F5B041; border-radius: 4px; font-size: 14px; line-height: 20px; color: #fff;"">Login</a></td></tr><tr><td style="" line-height: 0px;"" height=""32""></td></tr>"));
    //}

    public void SendSubAdminAddEmail(string email, string username, string name, string password, string LoginUrl)
    {
        SendEmail(email, "Invitation to Login", DesignEmail("Invitation to Login", $@"<tr><td style=""font-weight: 500; color: #292929; font-family: 'Nunito', sans-serif;"">Hi {name},</td></tr><tr><td style="" line-height: 0px;"" height=""16""></td></tr><tr><td style=""line-height: 0px;"" height=""16"">Username: <b>{username}</b></td></tr><tr><td style=""line-height: 0px;"" height=""16"">Password: <b>{password}</b></td></tr><tr><td style=""font-family: 'Nunito', sans-serif;"">You are invited to manage the {Localizer["app_name"].Value}. Click login button to manage {Localizer["app_name"].Value}</td></tr><tr><td style="" line-height: 0px;"" height=""32""></td></tr><tr align=""center""><td><a href=""{LoginUrl}"" style=""font-family: 'Nunito', sans-serif; padding: 12px; min-width: 216px; display: inline-block; background: #F5B041; border-radius: 4px; font-size: 14px; line-height: 20px; color: #fff;"">Login</a></td></tr><tr><td style="" line-height: 0px;"" height=""32""></td></tr>"));
    }

    public void SendUserChangePasswordEmail(string email, string username)
    {
        SendEmail(email, $"{Localizer["email_sub_change_password"].Value}", DesignEmail($"{Localizer["email_sub_change_password"].Value}", $@"<tr><td style=""font-weight: 500; color: #292929; font-family: 'Nunito', sans-serif;"">Hi {username},</td></tr><tr><td style="" line-height: 0px;"" height=""16""></td></tr><tr><td style=""font-family: 'Nunito', sans-serif;"">Your password for the {Localizer["app_name"].Value} account has been updated recently. Ignore this email if you have made this changes, contact Azara PWA if you have not made this changes.</td></tr>"));
    }

    public void SendForgotPasswordEmail(string email, string username, string otp, bool isAdmin = false, bool isSignUp = false, bool isUser = false)
    {
        string appName = Localizer["app_name"].Value;
        string emailBody;
        if (isAdmin)
            emailBody = $"<tr><td style='line-height: 0px;' height='16'></td></tr><tr><td style='font-family: 'Nunito', sans-serif;'>A request has been received to change the password for your {appName} account. Please <b style='font - size:20px;'>{otp}</b>.</td></tr>";
        else if (isUser)
            emailBody = $"<tr><td style='line-height: 0px;' height='16'></td></tr><tr><td style='font-family: 'Nunito', sans-serif;'>A request has been received to change the password for your {appName} account. Please <b style='font - size:20px;'>{otp}</b>.</td></tr>";
        else if (isSignUp)
            emailBody = $"<tr><td style='line-height: 0px;' height='16'></td></tr><tr><td style='font-family: 'Nunito', sans-serif;'>Please verify your {email} account with {appName} portal. Please <b style='font - size:20px;'>{otp}</b>.</td></tr>";
        else
            emailBody = $"<tr><td style='line-height: 0px;' height='16'></td></tr><tr><td style='font-family: 'Nunito', sans-serif;'>Your verification code is <b style='font - size:20px;'>{otp}</b>.</td></tr><tr><td>Enter this code in our website or app to activate your {appName} account.</td></tr><tr><td style='line-height: 0px;' height='32'></td></tr>";

        SendEmail(email, $"{Localizer["email_sub_forgot_password"].Value}", DesignEmail($"{Localizer["email_sub_forgot_password"].Value}", $@"<tr><td style=""font-weight: 500; color: #292929; font-family: 'Nunito', sans-serif;"">Hi {username},</td></tr>{emailBody}"));
    }

    public void SendAdminEmailVerify(string email, string username, string otp)
    {
        SendEmail(email, $"{Localizer["email_admin_verify"].Value}", DesignEmail($"{Localizer["email_admin_verify"].Value}", $@"<tr><td style=""font-weight: 500; color: #292929; font-family: 'Nunito', sans-serif;"">Hi {username},</td></tr><tr><td style="" line-height: 0px;"" height=""16""></td></tr><tr><td style=""font-family: 'Nunito', sans-serif;"">Your verification code is <b style=""font - size:20px; "">{otp}</b>.</td></tr><tr><td>Enter this code in our website or app to activate your {Localizer["app_name"].Value} account.</td></tr><tr><td style="" line-height: 0px;"" height=""32""></td></tr>"));
    }

    public void SendProductRequestMail(string email, string userName, string productName, string productImage, string productDescription)
    {
        SendEmail(email, $"{Localizer["email_product_request"].Value}", DesignEmail($"{Localizer["email_product_request"].Value}", $@"<tr><td style=""font-weight: 500; color: #292929; font-family: 'Nunito', sans-serif;"">Hello Admin! ,</td></tr><tr><td style="" line-height: 0px;"" height=""16""></td></tr><tr><td style=""font-family: 'Nunito', sans-serif;""><b>Email subject :</b> Product Request from <b>{userName}</b>. <br>This User is request for <b>{productName}</b>.</td></tr><tr><td>Here is product Image {productImage}.</td></tr><tr><td><br><b>Request Description</b>: {productDescription}</td></tr>"));
    }

    #endregion Send Account related mail

    #region Send Email

    private void SendEmail(string email, string subject, string body) => Messages.SendEmail(email, subject, body);

    #endregion Send Email

    #region Design Email

    private static string DesignEmail(string subject, string body)
    {
        // Generate response
        var response = @$"<table style=""font-family: 'Montserrat', sans-serif;"" cellpadding=""0"" cellspacing=""0"" bgcolor=""#f1f1f1"" width=""100%"">
	<tr>
		<td>
			<table width=""100%"" border-collapse=""collapse"" border-spacing=""0"" cellpadding=""0"" cellspacing=""0"" style=""color: #212121; min-width: 100%"" bgcolor=""#fff"" align=""center"">
				<tr>
					<td>
						<table border-collapse=""collapse"" border-spacing=""0"" width=""100%"" cellpadding=""0"" cellspacing=""0"" bgcolor=""#fff"" style=""min-width: 100%;"">
							<tr>
								<td style=""height: 40px""></td>
							</tr>
							<tr>
								<td>
									<table width=""552px"" cellpadding=""0"" cellspacing=""0"" align=""center"" bgcolor=""#212121"" style=""min-width: 600px; border-radius: 4px; padding: 20px 10px;"">
										<tr>
											<td>
												<img src=""http://webprojects.cloud/html/azara/images/azara-logo.svg"" style=""width: 130px; max-width: 250px; margin: auto; display: table;"">
											</td>
										</tr>
									</table>
								</td>
							</tr>
							<tr>
								<td style=""height: 10px""></td>
							</tr>
							<tr>
								<td>
									<table width=""552px"" cellpadding=""0"" cellspacing=""0"" align=""center"" bgcolor=""#ebebeb"" style=""min-width: 600px; border-radius: 4px 4px 0px 0px;"">
										<tr>
											<td style=""height:24px;""></td>
										</tr>
										<tr>
											<td style=""margin: 0px; text-align: left; font-size: 20px; line-height: 32px; color:  #000000;;text-align: center; font-weight: 600;"">
												Contact
											</td>
										</tr>
										<tr>
											<td style=""height:12px;""></td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td>
						<table width=""100%"" cellspacing=""0"" cellpadding=""0"">
							<tr>
								<td bgcolor=""#fff"">
									<table width=""600px"" align=""center"" cellpadding=""0"" cellspacing=""0"" bgcolor=""#ebebeb"" style=""border-radius: 0px 0px 4px 4px;"">
										<tr>
											<td>
												<table width=""90%"" align=""center"" cellpadding=""0"" cellspacing=""0"" bgcolor=""#ebebeb"">
													<tr>
														<td style=""height: 10px;""></td>
													</tr>
													<tr>
														<td style=""height: 24px""></td>
													</tr>
													<tr>
														<td style=""text-align: left;font-size: 16px; margin: 0; line-height: 24px;  color: #212121;"">
															{body}
														</td>
													</tr>
													<tr>
														<td style=""height: 42px;""></td>
													</tr>
													<tr>
														<td style=""text-align: left; font-size: 16px; line-height: 24px; color: #212121;"">
															Best,
														</td>
													</tr>
													<tr>
														<td style=""text-align: left; font-size: 16px; font-weight: normal; line-height: 24px; color: #212121;"">
															Azara
														</td>
													</tr>
													<tr>
														<td style=""height: 52px;""></td>
													</tr>
												</table>
											</td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td bgcolor=""#fff"" style=""height:24px""></td>
				</tr>
				<tr>
					<td bgcolor=""#fff"">
						<table width=""600px"" cellpadding=""0"" cellspacing=""0"" style=""margin: 0 auto; background: #212121; border-radius: 4px;"">
							<tr>
								<td style=""height:32px""></td>
							</tr>
							<tr>
								<td>
									<table width=""90%"" align=""center"" cellpadding=""0"" cellspacing=""0"">
										<tr>
											<td style=""width: 50%; flex-shrink: 0px; font-size: 16px; line-height: 24px; color: #ffffff;"">
												Need more help?</td>
											<td style=""width: 50%; flex-shrink: 0px; text-align: right;""><a href=""http://20.69.56.199:8087/homelanding"" style=""padding: 16px 30px; border-radius: 4px; display: inline-block; color: #212121; font-weight: 500; background:#8BF406; text-decoration: none;"">Go
													to Website</a></td>
										</tr>
									</table>
								</td>
							</tr>
							<tr>
								<td style=""height:32px""></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td bgcolor=""#fff"" style=""height:56px""></td>
				</tr>
				<tr>
					<td bgcolor=""#fff"">
						<table width=""100%"" cellpadding=""0"" cellspacing=""0"" align=""center"" style="" background: transparent; border-radius: 4px;"">

							<tr>
								<td style=""text-align: center; font-size: 16px; line-height: 24px; color: #212121;"">
									© Copyright 2022 Azara. All Rights Reserved.
								</td>
							</tr>
							<tr>
								<td style=""height: 32px;""></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td bgcolor=""#fff"" style=""height:68px""></td>
				</tr>
			</table>
		</td>
	</tr>
</table>";

        return response;
    }

    #endregion Design Email

    #region Dispose

    public void Dispose() => GC.SuppressFinalize(this);

    #endregion Dispose
}