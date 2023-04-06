using azara.models.Responses.Common;

namespace azara.files.implementations;

public class FileManagerLogic : IFileManagerLogic, IDisposable
{
    #region Constructor and Object Declaration

    private readonly BlobServiceClient blobServiceClient;

    private ConnectionConfigs ConnectionConfigs { get; set; }

    public FileManagerLogic(IOptions<ConnectionConfigs> ConnectionConfigsOption)
    {
        ConnectionConfigs = ConnectionConfigsOption.Value;

        if (!string.IsNullOrEmpty(ConnectionConfigs.BlobStorageConnection)) blobServiceClient = new BlobServiceClient(ConnectionConfigs.BlobStorageConnection);
    }

    #endregion Constructor and Object Declaration

    #region 1. Upload

    public async Task<string> Upload(IFormFile file, string folder, string filename)
    {
        await CheckFolderExists(folder);

        var blobContainer = blobServiceClient.GetBlobContainerClient(folder);

        if (!string.IsNullOrEmpty(filename)) filename = $"{filename}{Path.GetExtension(file.FileName)}";
        if (string.IsNullOrEmpty(filename)) filename = file.FileName;

        await Delete(folder, filename);

        var blobClient = blobContainer.GetBlobClient(filename);
        await blobClient.UploadAsync(file.OpenReadStream());
        return filename;
    }

    public async Task<string> UploadMultiple<T>(IFormFile file, string folder, string filename)
    {
        await CheckFolderExists(folder);

        var blobContainer = blobServiceClient.GetBlobContainerClient(folder);

        if (!string.IsNullOrEmpty(filename)) filename = $"{filename}{Path.GetExtension(file.FileName)}";
        if (string.IsNullOrEmpty(filename)) filename = file.FileName;

        await Delete(folder, filename);

        var blobClient = blobContainer.GetBlobClient(filename);
        await blobClient.UploadAsync(file.OpenReadStream());
        return filename;
    }

    //public async Task<string> UploadIds(IFormFile file, Guid Id)
    //{
    //    string blobConainerName = ;
    //    //var admin = DbContext._admin.FirstOrDefault(x => x.Id.Equals(Id))));

    //    var fileName = file.FileName;
    //    //admin.AdminProfile = file;
    //    //DbContext.SaveChanges();

    //    var blobContainer = blobServiceClient.GetBlobContainerClient(blobConainerName);

    //    var imageUrl = $"Profile/{Id}/" + fileName;

    //    try
    //    {
    //    }
    //    catch (RequestFailedException e)
    //    {
    //        Console.WriteLine(e.Message);
    //        Console.ReadLine();
    //        throw;
    //    }

    //    return imageUrl;

    //}

    public async Task<string> UploadId(IFormFile file, string folder, string filename, Guid Id)
    {
        await CheckFolderExists(folder);

        var blobContainer = blobServiceClient.GetBlobContainerClient(folder);

        if (!string.IsNullOrEmpty(filename)) filename = $"{filename}{Path.GetExtension(file.FileName)}";
        if (string.IsNullOrEmpty(filename)) filename = file.FileName;

        await Delete(folder, filename);

        var imageUrl = $"{Id}" + filename;

        var blobClient = blobContainer.GetBlobClient(imageUrl);
        await blobClient.UploadAsync(file.OpenReadStream());

        return filename;
    }

    public async Task<string> UploadNew(IFormFile file, string folder, string filename, string oldFileName = "")
    {
        await CheckFolderExists(folder);

        var blobContainer = blobServiceClient.GetBlobContainerClient(folder);

        //if (!string.IsNullOrEmpty(filename)) filename = $"{filename}";
        if (string.IsNullOrEmpty(filename)) filename = file.FileName;

        await Delete(folder, oldFileName);

        var blobClient = blobContainer.GetBlobClient(filename);
        await blobClient.UploadAsync(file.OpenReadStream());

        return filename;
        //return blobClient.Uri.ToString();
    }

    public async Task<FileResponse> UploadFile(IFormFile file, string folder, string filename, string oldFileName = "")
    {
        await CheckFolderExists(folder);

        var blobContainer = blobServiceClient.GetBlobContainerClient(folder);

        if (string.IsNullOrEmpty(filename)) filename = file.FileName;

        await Delete(folder, oldFileName);

        var blobClient = blobContainer.GetBlobClient(filename);
        await blobClient.UploadAsync(file.OpenReadStream());

        var respose = new FileResponse
        {
            FilePath = blobClient.Uri.ToString(),
            FileName = filename
        };

        return respose;
    }

    public async Task<FileResponse> UploadNewFile(IFormFile file, string folder, string filename)
    {
        await CheckFolderExists(folder);

        var blobContainer = blobServiceClient.GetBlobContainerClient(folder);

        if (string.IsNullOrEmpty(filename)) filename = file.FileName;

        var blobClient = blobContainer.GetBlobClient(filename);
        await blobClient.UploadAsync(file.OpenReadStream());

        var respose = new FileResponse
        {
            FilePath = blobClient.Uri.ToString(),
            FileName = filename
        };

        return respose;
    }

    public async Task Upload(FileStream content, string container, string filename)
    {
        await CheckFolderExists(container);
        await DeleteAllFilesFromBlob(container);

        var blobContainer = blobServiceClient.GetBlobContainerClient(container);
        var blobClient = blobContainer.GetBlobClient(filename);
        await blobClient.UploadAsync(content, new BlobHttpHeaders());
    }

    #endregion 1. Upload

    #region 2. Delete

    public async Task Delete(string folder, string name)
    {
        var blobContainer = blobServiceClient.GetBlobContainerClient(folder);
        blobContainer.GetBlobClient(name).DeleteIfExists();
    }

    #endregion 2. Delete

    #region 3. Upload Csv

    public async Task<string> UploadCsv<T>(
        IList<T> list,
        string fileName,
        string container,
        string folderFullPath,
        bool includeHeader = true)
    {
        // Build CSV Content to write in file from List
        var csvContent = await this.Write(list, includeHeader);

        // Store it to local folder and write the csv content in file
        var filepath = await ExportAsync(fileName, csvContent, folderFullPath);

        // Initialize fromFile from Path
        using var content = File.OpenRead(filepath);

        // Upload to Azure blob
        await Upload(content, container, fileName);

        return fileName;
    }

    private async Task<string> ExportAsync(string fileName, string content, string folderFullPath)
    {
        if (string.IsNullOrEmpty(folderFullPath)) return string.Empty;
        if (!Directory.Exists(folderFullPath)) Directory.CreateDirectory(folderFullPath);

        var path = $"{folderFullPath}\\{fileName}";
        await File.WriteAllTextAsync(path, content);
        return path;
    }

    #region CSV Conversion Files

    private async Task<string> Write<T>(IList<T> list, bool includeHeader = true)
    {
        var sb = new StringBuilder();
        var type = typeof(T);
        var properties = type.GetProperties();

        if (includeHeader) sb.AppendLine(this.CreateCsvHeaderLine(properties));

        foreach (var item in list)
            sb.AppendLine(this.CreateCsvLine(item, properties));

        return sb.ToString();
    }

    private string CreateCsvHeaderLine(PropertyInfo[] properties)
    {
        var propertyValues = new Collection<string>();

        foreach (var prop in properties)
        {
            string value = prop.Name;

            var attribute = prop.GetCustomAttribute(typeof(DisplayAttribute));
            if (attribute != null) value = (attribute as DisplayAttribute).Name;

            this.CreateCsvStringItem(propertyValues, value);
        }

        return this.CreateCsvLine(propertyValues);
    }

    private string CreateCsvLine<T>(T item, PropertyInfo[] properties)
    {
        var propertyValues = new Collection<string>();

        foreach (var prop in properties)
        {
            var value = prop.GetValue(item, null);

            if (prop.PropertyType == typeof(string)) this.CreateCsvStringItem(propertyValues, value);
            else if (prop.PropertyType == typeof(string[])) this.CreateCsvStringArrayItem(propertyValues, value);
            else if (prop.PropertyType == typeof(List<string>)) this.CreateCsvStringListItem(propertyValues, value);
            else this.CreateCsvItem(propertyValues, value);
        }

        return this.CreateCsvLine(propertyValues);
    }

    private string CreateCsvLine(IList<string> list)
    {
        return string.Join(",", list);
        //return string.Join(DELIMITER, list);
    }

    private void CreateCsvItem(ICollection<string> propertyValues, object value)
    {
        if (value != null) propertyValues.Add(value.ToString());
        else propertyValues.Add(string.Empty);
    }

    private void CreateCsvStringListItem(ICollection<string> propertyValues, object value)
    {
        string formatString = "\"{0}\"";
        if (value != null)
        {
            value = this.CreateCsvLine((List<string>)value);
            propertyValues.Add(string.Format(formatString, this.ProcessStringEscapeSequence(value)));
        }
        else propertyValues.Add(string.Empty);
    }

    private void CreateCsvStringArrayItem(ICollection<string> propertyValues, object value)
    {
        string formatString = "\"{0}\"";
        if (value != null)
        {
            value = this.CreateCsvLine(((string[])value).ToList());
            propertyValues.Add(string.Format(formatString, this.ProcessStringEscapeSequence(value)));
        }
        else propertyValues.Add(string.Empty);
    }

    private void CreateCsvStringItem(ICollection<string> propertyValues, object value)
    {
        string formatString = "\"{0}\"";
        if (value != null) propertyValues.Add(string.Format(formatString, this.ProcessStringEscapeSequence(value)));
        else propertyValues.Add(string.Empty);
    }

    private string ProcessStringEscapeSequence(object value) => value.ToString().Replace("\"", "\"\"");

    #endregion CSV Conversion Files

    #endregion 3. Upload Csv

    #region Common Methods

    private async Task CheckFolderExists(string folder)
    {
        CloudStorageAccount mycloudStorageAccount = CloudStorageAccount.Parse(ConnectionConfigs.BlobStorageConnection);
        CloudBlobClient blobClient = mycloudStorageAccount.CreateCloudBlobClient();
        CloudBlobContainer container = blobClient.GetContainerReference(folder);

        //var blobServiceClient = new BlobServiceClient(ConnectionConfigs.BlobStorageConnection);
        //var container = blobServiceClient.GetBlobContainerClient(folder);

        if (await container.CreateIfNotExistsAsync())
        {
            await container.SetPermissionsAsync(new BlobContainerPermissions
            {
                PublicAccess = BlobContainerPublicAccessType.Blob
            });
        }
    }

    private async Task DeleteAllFilesFromBlob(string containerName)
    {
        BlobContainerClient containerClient = new BlobContainerClient(ConnectionConfigs.BlobStorageConnection, containerName);
        var allBlobs = containerClient.GetBlobs();
        foreach (var blob in allBlobs)
        {
            var blobClient = containerClient.GetBlobClient(blob.Name);
            await blobClient.DeleteIfExistsAsync();
        }
    }

    #endregion Common Methods

    #region Dispose

    public void Dispose() => GC.SuppressFinalize(this);

    #endregion Dispose

    async static Task ListContainers(BlobServiceClient blobServiceClient, string prefix, int? segmentSize)
    {
        try
        {
            // Call the listing operation and enumerate the result segment.
            var resultSegment =
                blobServiceClient.GetBlobContainersAsync(BlobContainerTraits.Metadata, prefix, default)
                .AsPages(default, segmentSize);

            await foreach (Azure.Page<BlobContainerItem> containerPage in resultSegment)
            {
                foreach (BlobContainerItem containerItem in containerPage.Values)
                {
                    Console.WriteLine("Container name: {0}", containerItem.Name);
                }

                Console.WriteLine();
            }
        }
        catch (RequestFailedException e)
        {
            Console.WriteLine(e.Message);
            Console.ReadLine();
            throw;
        }
    }
}