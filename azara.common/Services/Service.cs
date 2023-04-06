﻿namespace azara.common.Services;

public class Service
{
    private static readonly HttpClient client = new HttpClient();

    #region Post API Without Token

    public static async Task<dynamic> PostAPIWithoutToken(string url, StringContent stringContent)
    {
        try
        {
            var httpResponseMessage = await client.PostAsync(url, stringContent);
            var response = await httpResponseMessage.Content.ReadAsStringAsync();
            var statusCode = Convert.ToInt32(httpResponseMessage.StatusCode);
            if (statusCode != StatusCodeConsts.Success)
            {
                var JsonResponseList = JsonConvert.DeserializeObject<CallAPIList>(response);
                return new CallAPIList
                {
                    meta = new ResponseMetaListCallAPI
                    {
                        message = JsonResponseList.meta.message,
                        statusCode = JsonResponseList.meta.statusCode
                    }
                };
            }
            var JsonResponse = JsonConvert.DeserializeObject<CallAPI>(response);
            return new CallAPI
            {
                data = JsonResponse.GetJsonData(),
                meta = new ResponseMetaCallAPI
                {
                    message = JsonResponse.meta.message,
                    statusCode = JsonResponse.meta.statusCode
                }
            };
        }
        catch (Exception ex)
        {
            return new CallAPI
            {
                meta = new ResponseMetaCallAPI
                {
                    message = ex.Message,
                    statusCode = 1
                }
            };
        }
    }

    #endregion Post API Without Token

    #region Post API With Token

    public static async Task<dynamic> PostAPIWithToken(string url, StringContent stringContent = null, string Token = "")
    {
        try
        {
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Token);
            var httpResponseMessage = await client.PostAsync(url, stringContent);
            var response = await httpResponseMessage.Content.ReadAsStringAsync();
            var statusCode = Convert.ToInt32(httpResponseMessage.StatusCode);
            if (statusCode != StatusCodeConsts.Success)
            {
                var JsonResponseList = JsonConvert.DeserializeObject<CallAPIList>(response);
                if (JsonResponseList == null)
                {
                    return new CallAPIList
                    {
                        meta = new ResponseMetaListCallAPI
                        {
                            message = null,
                            statusCode = statusCode
                        }
                    };
                }
                else
                {
                    return new CallAPIList
                    {
                        meta = new ResponseMetaListCallAPI
                        {
                            message = JsonResponseList.meta.message,
                            statusCode = JsonResponseList.meta.statusCode
                        }
                    };
                }
            }
            var JsonResponse = JsonConvert.DeserializeObject<CallAPI>(response);
            return new CallAPI
            {
                data = JsonResponse.GetJsonData(),
                meta = new ResponseMetaCallAPI
                {
                    message = JsonResponse.meta.message,
                    statusCode = JsonResponse.meta.statusCode
                }
            };
        }
        catch (Exception)
        {
            return new CallAPI
            {
                meta = new ResponseMetaCallAPI
                {
                    message = "Exception!",
                    statusCode = 1
                }
            };
        }
    }

    #endregion Post API With Token

    #region Post Upload Image

    public static async Task<dynamic> PostUploadFile(string url, string name, IBrowserFile formDataContent = null, string Token = "")
    {
        try
        {
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Token);

            MultipartFormDataContent multiContent = new MultipartFormDataContent();

            var fileContent = new StreamContent(formDataContent.OpenReadStream(15728640));

            fileContent.Headers.ContentType = new MediaTypeHeaderValue(formDataContent.ContentType);

            multiContent.Add(content: fileContent, name: "\"File\"", fileName: formDataContent.Name);
            multiContent.Add(new StringContent(name), "FileName");

            var httpResponseMessage = await client.PostAsync(url, multiContent);
            var response = await httpResponseMessage.Content.ReadAsStringAsync();
            var statusCode = Convert.ToInt32(httpResponseMessage.StatusCode);
            if (statusCode != StatusCodeConsts.Success)
            {
                var JsonResponseList = JsonConvert.DeserializeObject<CallAPIList>(response);
                if (JsonResponseList == null)
                {
                    return new CallAPIList
                    {
                        meta = new ResponseMetaListCallAPI
                        {
                            message = null,
                            statusCode = statusCode
                        }
                    };
                }
                else
                {
                    return new CallAPIList
                    {
                        meta = new ResponseMetaListCallAPI
                        {
                            message = JsonResponseList.meta.message,
                            statusCode = JsonResponseList.meta.statusCode
                        }
                    };
                }
            }
            var JsonResponse = JsonConvert.DeserializeObject<CallAPI>(response);
            return new CallAPI
            {
                data = JsonResponse.GetJsonData(),
                meta = new ResponseMetaCallAPI
                {
                    message = JsonResponse.meta.message,
                    statusCode = JsonResponse.meta.statusCode
                }
            };
        }
        catch (Exception ex)
        {
            return new CallAPI
            {
                meta = new ResponseMetaCallAPI
                {
                    message = "Exception!",
                    statusCode = 1
                }
            };
        }
    }

    #endregion Post Upload Image

    #region Get API Without Token

    public static async Task<dynamic> GetAPIWithoutToken(string url)
    {
        try
        {
            var httpResponseMessage = await client.GetAsync(url);
            var response = await httpResponseMessage.Content.ReadAsStringAsync();
            var JsonResponse = JsonConvert.DeserializeObject<CallAPI>(response);
            return new CallAPI
            {
                data = JsonResponse.GetJsonData(),
                meta = new ResponseMetaCallAPI
                {
                    message = JsonResponse.meta.message,
                    statusCode = JsonResponse.meta.statusCode
                }
            };
        }
        catch (Exception ex)
        {
            return new CallAPI
            {
                meta = new ResponseMetaCallAPI
                {
                    message = "Exception!",
                    statusCode = 1
                }
            };
        }
    }

    #endregion Get API Without Token

    #region Get API With Token

    public static async Task<dynamic> GetAPIWithToken(string url, string Token = "")
    {
        try
        {
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Token);
            var httpResponseMessage = await client.GetAsync(url);
            var response = await httpResponseMessage.Content.ReadAsStringAsync();
            var statusCode = Convert.ToInt32(httpResponseMessage.StatusCode);
            if (statusCode != StatusCodeConsts.Success)
            {
                var JsonResponseList = JsonConvert.DeserializeObject<CallAPIList>(response);
                if (JsonResponseList == null)
                {
                    return new CallAPIList
                    {
                        meta = new ResponseMetaListCallAPI
                        {
                            message = null,
                            statusCode = statusCode
                        }
                    };
                }
                else
                {
                    return new CallAPIList
                    {
                        meta = new ResponseMetaListCallAPI
                        {
                            message = JsonResponseList.meta.message,
                            statusCode = JsonResponseList.meta.statusCode
                        }
                    };
                }
            }
            var JsonResponse = JsonConvert.DeserializeObject<CallAPI>(response);
            return new CallAPI
            {
                data = JsonResponse.GetJsonData(),
                meta = new ResponseMetaCallAPI
                {
                    message = JsonResponse.meta.message,
                    statusCode = JsonResponse.meta.statusCode
                }
            };
        }
        catch (Exception ex)
        {
            return new CallAPI
            {
                meta = new ResponseMetaCallAPI
                {
                    message = "Exception!",
                    statusCode = 1
                }
            };
        }
    }

    #endregion Get API With Token

    public static async Task<dynamic> PostUploadCSVFile(string url, string name, string oldFileName = "", IBrowserFile formDataContent = null, string Token = "")
    {
        try
        {
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Token);

            MultipartFormDataContent multiContent = new MultipartFormDataContent();

            var fileContent = new StreamContent(formDataContent.OpenReadStream(15728640));

            fileContent.Headers.ContentType = new MediaTypeHeaderValue(formDataContent.ContentType);

            multiContent.Add(content: fileContent, name: "\"File\"", fileName: formDataContent.Name);
            multiContent.Add(new StringContent(name), "FileName");
            if (oldFileName != null)
            {
                multiContent.Add(new StringContent(oldFileName), "OldFileName");
            }

            var httpResponseMessage = await client.PostAsync(url, multiContent);
            var response = await httpResponseMessage.Content.ReadAsStringAsync();
            var statusCode = Convert.ToInt32(httpResponseMessage.StatusCode);
            if (statusCode != StatusCodeConsts.Success)
            {
                var JsonResponseList = JsonConvert.DeserializeObject<CallAPIList>(response);
                return new CallAPIList
                {
                    meta = new ResponseMetaListCallAPI
                    {
                        message = JsonResponseList.meta.message,
                        statusCode = JsonResponseList.meta.statusCode
                    }
                };
            }
            var JsonResponse = JsonConvert.DeserializeObject<CallAPI>(response);
            return new CallAPI
            {
                data = JsonResponse.GetJsonData(),
                meta = new ResponseMetaCallAPI
                {
                    message = JsonResponse.meta.message,
                    statusCode = JsonResponse.meta.statusCode
                }
            };
        }
        catch (Exception)
        {
            return new CallAPI
            {
                meta = new ResponseMetaCallAPI
                {
                    message = "Exception!",
                    statusCode = 1
                }
            };
        }
    }


    #region Post Upload Image

    public static async Task<dynamic> PostNewUploadFile(string url, List<string> name, List<string> oldFileName, List<IBrowserFile> formDataContent = null, string Token = "", string folderName = "")
    {
        try
        {
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Token);

            MultipartFormDataContent multiContent = new MultipartFormDataContent();

            foreach (var item in formDataContent)
            {
                var fileContent = new StreamContent(item.OpenReadStream(15728640));

                fileContent.Headers.ContentType = new MediaTypeHeaderValue(item.ContentType);

                multiContent.Add(content: fileContent, name: "\"File\"", fileName: item.Name);

            }

            foreach (var item in name)
            {
                if (!string.IsNullOrWhiteSpace(item))
                    multiContent.Add(new StringContent(item), "FileName");
            }

            if (oldFileName != null && oldFileName.Count > 0)
            {
                foreach (var item in oldFileName)
                {
                    if (!string.IsNullOrWhiteSpace(item))
                        multiContent.Add(new StringContent(item), "OldFileName");
                }
            }

            if (!string.IsNullOrWhiteSpace(folderName))
                multiContent.Add(new StringContent(folderName), "FolderName");

            var httpResponseMessage = await client.PostAsync(url, multiContent);
            var response = await httpResponseMessage.Content.ReadAsStringAsync();
            var statusCode = Convert.ToInt32(httpResponseMessage.StatusCode);
            if (statusCode != StatusCodeConsts.Success)
            {
                var JsonResponseList = JsonConvert.DeserializeObject<CallAPIList>(response);
                return new CallAPIList
                {
                    meta = new ResponseMetaListCallAPI
                    {
                        message = JsonResponseList.meta.message,
                        statusCode = JsonResponseList.meta.statusCode
                    }
                };
            }
            var JsonResponse = JsonConvert.DeserializeObject<CallAPI>(response);
            return new CallAPI
            {
                data = JsonResponse.GetJsonData(),
                meta = new ResponseMetaCallAPI
                {
                    message = JsonResponse.meta.message,
                    statusCode = JsonResponse.meta.statusCode
                }
            };
        }
        catch (Exception)
        {
            return new CallAPI
            {
                meta = new ResponseMetaCallAPI
                {
                    message = "Exception!",
                    statusCode = 1
                }
            };
        }
    }

    #endregion Post Upload Image
}