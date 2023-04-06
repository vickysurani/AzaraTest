namespace azara.files.implementations;

public class StoreToLocalFolder : IDisposable
{
    private string storage_path = string.Empty;
    private string file_name = "", full_path = "";

    public StoreToLocalFolder(BaseUrlConfigs baseUrl) => storage_path = baseUrl.ImageLocalPath;

    public async Task<string> Upload(IFormFile file, string name, string folder)
    {
        if (string.IsNullOrWhiteSpace(storage_path)) storage_path = "AdminPanelImages";

        if (!Directory.Exists($"{storage_path}//{folder}")) Directory.CreateDirectory($"{storage_path}//{folder}");

        file_name = $"{name}{Path.GetExtension(file.FileName)}";
        full_path = $"{storage_path}/{folder}/{file_name}";

        using (var fileStream = new FileStream(full_path, FileMode.Create))
        {
            await file.CopyToAsync(fileStream);
        }
        return file_name;
    }

    public void Delete(string fileName, string folder)
    {
        if (!Directory.Exists($"{storage_path}//{folder}")) Directory.CreateDirectory($"{storage_path}//{folder}");

        full_path = $"{storage_path}/{folder}/{fileName}";

        Delete(full_path);
    }

    public void Delete(string fullPath)
    {
        // Code for delete fieles even in use
        GC.Collect();
        GC.WaitForPendingFinalizers();

        if (File.Exists(fullPath)) File.Delete(fullPath);
    }

    public void DeleteDirectory(string folderpath)
    {
        // Code for delete fieles even in use
        GC.Collect();
        GC.WaitForPendingFinalizers();

        if (Directory.Exists(folderpath)) Directory.Delete(folderpath);
    }

    public void Dispose() => GC.SuppressFinalize(this);
}