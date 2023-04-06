namespace azara.files;

public interface IUploadManager : IDisposable
{
    Task<string> Store(IFormFile file, string name, string type);

    void Delete(string name, string type);

    void DeleteDirectory(string folderpath);

    void Move(string sourceFilePath, string destinationFilePath);

    void MoveDirectory(string sourceFilePath, string destinationFilePath);
}