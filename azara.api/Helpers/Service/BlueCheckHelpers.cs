namespace azara.api.Helpers.Service;

public class BlueCheckHelpers : IDisposable
{
    private static readonly HttpClient client = new HttpClient();
    private readonly string blueCheckApiPath = "https://verify.bluecheck.me/api/";
    private readonly string blueCheckApiVersion = "4.3";
#if DEBUG
    private readonly string blueCheckApiDomainToken = "1OYYCtgJE51kdd1NiaES"; // Local

#elif RELEASE
    private readonly string blueCheckApiDomainToken = "1OYYCtgJE51kdd1NiaES"; // Live

#elif UAT
    //private readonly string blueCheckApiDomainToken = "vuot78VYzUVGuCI64oPM"; // UAT
    private readonly string blueCheckApiDomainToken = "1OYYCtgJE51kdd1NiaES"; // UAT
#endif
    private readonly string verificationType = "age_photoID";

    #region Constructor

    AzaraContext DbContext { get; set; }

    ICrypto Crypto { get; set; }

    IMapper Mapper { get; set; }

    public BlueCheckHelpers(
        AzaraContext DbContext,
        ICrypto Crypto,
        IMapper Mapper)
    {
        this.DbContext = DbContext;
        this.Crypto = Crypto;
        this.Mapper = Mapper;
    }

    #endregion Constructor

    #region 1. Blue check

    public async Task<BlueCheckResponse> VerifyWithBlueCheck(BlueCheckVerifyModel request)
    {
        //try
        //{
        BlueCheckVerifyBindingModel bindingModel = new();
        bindingModel.version = blueCheckApiVersion;
        bindingModel.domainToken = blueCheckApiDomainToken;
        bindingModel.verificationType = verificationType;
        bindingModel.files = request.files;
        bindingModel.userData = request.userData;

        var form = new MultipartFormDataContent();

        var getFrontFile = await GetFileData(request.files.id_front);
        if (getFrontFile != null)
            form.Add(getFrontFile.Stream, name: "file", getFrontFile.Name);

        var getSelfieFile = await GetFileData(request.files.id_selfie);
        if (getSelfieFile != null)
            form.Add(getSelfieFile.Stream, name: "file", getSelfieFile.Name);

        //local
        //byte[] fileData = File.ReadAllBytes(@"C:\Yudiz\1. Project\A\Azara\azara.api\images\" + bindingModel.files.id_front.FileName);

        //ByteArrayContent byteContent = new ByteArrayContent(fileData);

        //byteContent.Headers.ContentType = MediaTypeHeaderValue.Parse("multipart/form-data");

        //form.Add(byteContent, "file", Path.GetFileName(bindingModel.files.id_front.FileName));

        var httpResponseMessage = await client.PostAsync($"{blueCheckApiPath}verify?version={blueCheckApiVersion}&domainToken={blueCheckApiDomainToken}&userData[first_name]={bindingModel.userData.first_name}&userData[last_name]={bindingModel.userData.last_name}&userData[address]={bindingModel.userData.address}&verificationType={verificationType}&userData[dob]={bindingModel.userData.dob}&userData[email]={bindingModel.userData.email}", form);
        //&userData[country] ={ bindingModel.userData.country}
        var response = await httpResponseMessage.Content.ReadAsStringAsync();

        BlueCheckResponse blueCheckResponse = new BlueCheckResponse();

        if (response != null && httpResponseMessage.StatusCode == HttpStatusCode.OK)
        {
            BlueCheckVerificationResponse blueCheckVerificationResponse = JsonConvert.DeserializeObject<BlueCheckVerificationResponse>(response);
            if (blueCheckVerificationResponse != null)
            {
                blueCheckResponse.StatusCode = httpResponseMessage.StatusCode;
                blueCheckResponse.BlueCheckData = blueCheckVerificationResponse;
            }
        }

        return blueCheckResponse;
        //}
        //catch (Exception e)
        //{
        //    Console.WriteLine(e);
        //}

        return new BlueCheckResponse();
    }

    #endregion 1. Blue check

    private async Task<MultipartFormDataModel> GetFileData(string fileUrl)
    {
        using (var client = new HttpClient()) // WebClient
        {
            //var fileName = @"C:\temp\imgd.jpg";
            var uri = new Uri(fileUrl);

            //bool exists = System.IO.Directory.Exists("C:\\temp\\");

            //if (!exists)
            //    System.IO.Directory.CreateDirectory("C:\\temp\\");

            //File.Delete(fileName);

            using (var s = await client.GetStreamAsync(uri))
            {
                //using (var fs = new FileStream(fileName, FileMode.CreateNew))
                //{
                //    await s.CopyToAsync(fs);
                byte[] fileBytes;
                using var ms = new MemoryStream();
                s.CopyTo(ms);
                fileBytes = ms.ToArray();
                string filename = System.IO.Path.GetFileName(uri.AbsoluteUri);
                //var fileStreamContent = new StreamContent(fs);
                //fileStreamContent.Headers.ContentType = MediaTypeHeaderValue.Parse("multipart/form-data");
                return new MultipartFormDataModel { Name = filename, Stream = new ByteArrayContent(fileBytes) };
                //}
            }
        }
    }


    #region Dispose

    public void Dispose() => GC.SuppressFinalize(this);

    #endregion Dispose
}