using azara.models.Responses.Common;

namespace azara.files;

public interface IFileManagerLogic
{
    Task<string> Upload(IFormFile file, string folder, string filename);

    Task<string> UploadMultiple<T>(IFormFile file, string folder, string filename);

    Task<string> UploadId(IFormFile file, string filename, string oldfilename, Guid Id);

    //Task<string> UploadIds(IFormFile file, Guid Id);

    Task<string> UploadNew(IFormFile file, string folder, string filename, string oldFilePath);

    Task<FileResponse> UploadFile(IFormFile file, string folder, string filename, string oldFilePath);
    Task<FileResponse> UploadNewFile(IFormFile file, string folder, string filename);

    Task Delete(string folder, string name);

    Task<string> UploadCsv<T>(IList<T> list, string fileName, string container, string folderFullPath,
       bool includeHeader = true);
}