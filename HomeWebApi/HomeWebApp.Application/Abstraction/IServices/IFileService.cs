using HomeWebApp.Domain.Enums.Enums;
using Microsoft.AspNetCore.Http;
using Practyc.Application.RRModel;
using Practyc.Domain.Enums;

namespace HomeWebApp.Application.Abstraction.IServices
{
    public interface IFileService
    {


        Task<AppFileResponse> UploadFileAsync(AppModule module, Guid EntityId, IFormFile file);


        Task<IEnumerable<AppFileResponse>> UploadFilesAsync(AppModule module, Guid entityId, IFormFileCollection files);


        Task<bool> DeleteFileAsync(Guid Id, string filePath);


        Task<int> DeleteFilesAsync(List<Guid> Ids, List<string> filePaths);






       /* Task<string> AddFile(IFormFile file);*/
    }
}
