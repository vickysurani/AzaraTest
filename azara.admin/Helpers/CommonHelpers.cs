using azara.admin.Models.PrivacyPolicy.Request;
using azara.admin.Models.Sub_admin.Response;
using azara.models.Entities;
using azara.models.Requests.Blog;
using azara.models.Requests.PrivacyPolicy;
using azara.models.Responses.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace azara.admin.Helpers;

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

    #region Dashboard API

    internal static async Task<dynamic> Dashboard()
    {
        var url = $"{ApiEndPointConsts.Dashboard.Dashboarddata}";
        var response = await Service.GetAPIWithToken(url, TokenResponse.Token);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    #endregion Dashboard API

    #region Upload File API

    internal static async Task<dynamic> UploadFileApi(FileInsertUploadRequest request)
    {
        var url = $"{ApiEndPointConsts.FileConst.UploadFile}";
        var response = await Service.PostUploadFile(url, request.FileName, request.File, TokenResponse.Token);

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

    #region PrivacyPolicyList
    internal static async Task<dynamic> PrivacyPolicyList(PrivacyPolicyListRequest request)
    {
        var url = $"{ApiEndPointConsts.Admin.PrivacyPolicyList}";
        var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        var response = await Service.PostAPIWithToken(url, stringContent, TokenResponse.Token);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }
    #endregion


    #region PrivacyPolicy Delete

    internal static async Task<dynamic> DeletePrivacyPolicy(BaseIdRequest request)
    {
        var url = $"{ApiEndPointConsts.Admin.PrivacyPolicyDelete}";
        var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        var response = await Service.PostAPIWithToken(url, stringContent, TokenResponse.Token);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    #endregion PrivacyPolicy Delete

    #region PrivacyPolicy Insert-Update

    internal static async Task<dynamic> PrivacyPolicyAddUpdateApi(PrivacyPolicyInsertUpdateRequest request)
    {
        var url = string.IsNullOrWhiteSpace(request.Id) ? $"{ApiEndPointConsts.Admin.PrivacyPolicyInsert}" : $"{ApiEndPointConsts.Admin.AdminUpdate}";
        var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        var response = await Service.PostAPIWithToken(url, stringContent, TokenResponse.Token);

        return response.meta.statusCode != StatusCodeConsts.Success
            ? new CallAPIList() { meta = response.meta }
            : new CallAPI() { meta = response.meta, data = response.data };
    }

    #endregion PrivacyPolicy Insert-Update

}