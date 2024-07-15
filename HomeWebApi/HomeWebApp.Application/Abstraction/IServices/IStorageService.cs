using Microsoft.AspNetCore.Http;

namespace HomeWebApp.Application.Abstraction.IServices
{
    public interface IStorageService
    {                                                                            //This will be responsible for Storing the file because this job is for Storage//
                                                                                 //IFileService job is Not this //
            Task<string> UploadFileAsync(IFormFile file);


            Task<List<string>> UploadFilesAsync(IFormFileCollection files);


            Task<bool> DeleteFileAsync(string filePath);


            Task<int> DeleteFilesAsync(List<string> filePaths);
        
    }
}
