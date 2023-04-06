namespace azara.client.Helpers;

public class CommonHelpers
{
    private readonly IFileUpload _fileUpload;

    public CommonHelpers(IFileUpload fileUpload) => _fileUpload = fileUpload;

    #region Delete Image

    internal string DeleteImage(string imagePath)
    {
        _fileUpload.DeleteFile(imagePath);
        return string.Empty;
    }

    #endregion Delete Image

    #region MyReward Insert

    internal static async Task<dynamic> MyRewardAddApi(MyRewardInsertRequest request)
    {
        var url = ApiEndPointConsts.MyReward.AddMyReward;
        var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        var response = await Service.PostAPIWithToken(url, stringContent, TokenResponse.Token);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    #endregion MyReward Insert

    #region Myreward Get by Id
    internal static async Task<dynamic> MyrewardsbyID(BaseIdRequest request)
    {
        var url = $"{ApiEndPointConsts.MyReward.MyRewardGetById}";
        var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        var response = await Service.PostAPIWithoutToken(url, stringContent);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }
    #endregion

    #region MyReward List

    internal static async Task<dynamic> MyRewardListApi()
    {
        var url = ApiEndPointConsts.MyReward.MyRewardList;
        var response = await Service.GetAPIWithToken(url, TokenResponse.Token);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    #endregion MyReward List

    #region My Reward Get by Id

    #endregion

    #region Use MyReward

    internal static async Task<dynamic> UseMyReward(UseMyRewardRequest request)
    {
        var url = ApiEndPointConsts.MyReward.UseMyReward;
        var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        var response = await Service.PostAPIWithToken(url, stringContent, TokenResponse.Token);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    #endregion Use MyReward



    //#region Coupons add in MyReward List

    //internal static async Task<dynamic> MyCouponsAdd()
    //{
    //    var url = ApiEndPointConsts.MyReward.MyCouponsdetails;
    //    var response = await Service.GetAPIWithToken(url, TokenResponse.Token);

    //    return response.meta.statusCode != StatusCodeConsts.Success
    //        ? new CallAPIList() { meta = response.meta }
    //        : new CallAPI() { meta = response.meta, data = response.data };
    //}

    //#endregion MyReward List

    #region MyReward detils

    internal static async Task<dynamic> MyRewardDetailsApi(RewardDetailsGetById request)
    {
        var url = ApiEndPointConsts.MyReward.MyRewarddetails;
        var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        var response = await Service.PostAPIWithToken(url, stringContent, TokenResponse.Token);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    #endregion MyReward detils

    #region Upload File API

    internal static async Task<dynamic> UploadFileApi(FileInsertUploadRequest request)
    {
        var url = $"{ApiEndPointConsts.FileConst.UploadFile}";
        var response = await Services.Service.PostUploadFile(url, request.FileName, request.OldFileName, request.File, null, request.FolderName);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    #endregion Upload File API

    #region Upload File API

    internal static async Task<dynamic> UploadNewFileApi(FileNewInsertUploadRequest request)
    {
        var url = $"{ApiEndPointConsts.FileConst.UploadFile}";
        var response = await Service.PostNewUploadFile(url, request.FileName, request.OldFileName, request.File, null, request.FolderName);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    #endregion Upload File API

    #region Banner Image List
    internal static async Task<dynamic> BannerList(ListRequest request)
    {
        var url = $"{ApiEndPointConsts.Banner.BannerImageList}";
        var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        var response = await Service.PostAPIWithToken(url, stringContent, TokenResponse.Token);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }
    #endregion


    # region PrivacyPolicyList
    internal static async Task<dynamic> PrivacyPolicyList(PrivacyPolicyGetListRequest request)
    {
        var url = $"{ApiEndPointConsts.Account.PrivacyPolicyList}";
        var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        var response = await Service.PostAPIWithToken(url, stringContent, TokenResponse.Token);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }
    #endregion
}