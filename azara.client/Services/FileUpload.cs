namespace azara.client.Services;

public class FileUpload : IFileUpload
{
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
                    string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "AdminPanelImages");
                    //string[] images = Directory.GetFiles(fileName);
                    path = Path.Combine("C:/Yudiz/1. Project/A/Azara/azara.client/wwwroot", fileName);
                    // This will get the current WORKING directory (i.e. \bin\Debug)
                    //string workingDirectory = Path.GetFullPath("wwwroot");

                    //// This will get the current PROJECT bin directory (ie ../bin/)
                    //string projectDirectory = Directory.GetParent(workingDirectory).Parent.FullName;

                    //// This will get the current PROJECT directory
                    //string projectDirectorys = Directory.GetParent(workingDirectory).Parent.Parent.FullName;

                    //var a = Directory.GetCurrentDirectory();
                    //var solutionFileLocation = Directory.GetParent(Directory.GetCurrentDirectory());
                    //path = Path.Combine(solutionFileLocation.FullName, "azara.client\\wwwroot\\", fileName);
                }
                else
                {
                    path = Path.Combine(Directory.GetCurrentDirectory(), fileName);
                }

                if (File.Exists(path))
                {
                    File.Delete(path);
                    if (count == 1)
                        return true;
                }
                else
                    return false;

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
                    path = Path.Combine("C:/Yudiz/1. Project/A/Azara/azara.client/wwwroot/AdminPanelImages", fileName);
                }
                else
                {
                    path = Path.Combine(Directory.GetCurrentDirectory(), fileName);
                }

                var memoryStream = new MemoryStream();
                await file.OpenReadStream(2147483648).CopyToAsync(memoryStream);

                if (!Directory.Exists(folderDirectory))
                {
                    Directory.CreateDirectory(folderDirectory);
                }

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