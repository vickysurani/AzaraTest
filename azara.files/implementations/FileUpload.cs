namespace azara.files.implementations;

public class FileUpload : IFileUpload
{
    private readonly IWebHostEnvironment _webHostEnvironment;

    public FileUpload(IWebHostEnvironment webHostEnvironment) => _webHostEnvironment = webHostEnvironment;

    public bool DeleteFile(string fileName)
    {
        try
        {
            int count = 0;
            bool isDeleteClientSide = true;
            string path = string.Empty;
            while (count < 2)
            {
                if (isDeleteClientSide)
                {
                    var solutionFileLocation = Directory.GetParent(Directory.GetCurrentDirectory());
                    path = Path.Combine(solutionFileLocation.FullName, "azara.client\\wwwroot\\", fileName);
                }
                else path = $"{_webHostEnvironment.WebRootPath}\\{fileName}";

                if (File.Exists(path))
                {
                    File.Delete(path);
                    if (count == 1) return true;
                }
                else return false;

                isDeleteClientSide = false;
                count++;
            }
            return false;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public async Task<string> UploadFile(IBrowserFile file)
    {
        try
        {
            int count = 0;
            string folderDirectory = string.Empty;
            string path = string.Empty;
            bool isAddClientSideImage = true;
            FileInfo fileInfo = new(file.Name);
            var fileName = Guid.NewGuid().ToString() + fileInfo.Extension;
            while (count < 2)
            {
                if (isAddClientSideImage)
                {
                    var solutionFileLocation = Directory.GetParent(Directory.GetCurrentDirectory());
                    folderDirectory = $"{solutionFileLocation.FullName}\\azara.client\\wwwroot\\AdminPanelImages";
                    path = Path.Combine(solutionFileLocation.FullName, "azara.client\\wwwroot\\AdminPanelImages", fileName);
                }
                else
                {
                    folderDirectory = $"{_webHostEnvironment.WebRootPath}\\AdminPanelImages";
                    path = Path.Combine(_webHostEnvironment.WebRootPath, "AdminPanelImages", fileName);
                }

                var memoryStream = new MemoryStream();
                await file.OpenReadStream(2147483648).CopyToAsync(memoryStream);

                if (!Directory.Exists(folderDirectory)) Directory.CreateDirectory(folderDirectory);

                await using (var fs = new FileStream(path, FileMode.Create, FileAccess.Write))
                {
                    memoryStream.WriteTo(fs);
                }
                isAddClientSideImage = false;
                count++;
            }

            var fullPath = $"AdminPanelImages/{fileName}";
            return fullPath;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}