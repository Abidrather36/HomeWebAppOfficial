using HomeWebApp.Application.Abstraction.IRepositoryService;
using HomeWebApp.Application.Abstraction.IServices;
using HomeWebApp.Domain.Entities;
using HomeWebApp.Domain.Enums.Enums;
using Microsoft.AspNetCore.Http;
using Practyc.Application.RRModel;
namespace HomeWebApp.Application.Services
{
    public class FileService : IFileService
    {
        
        private readonly IFileRepository fileRepository;
        private readonly IStorageService storageService;

        public FileService(IFileRepository fileRepository, IStorageService storageService)
        {
            this.fileRepository = fileRepository;
            this.storageService = storageService;
        }
       /* public async  Task<string> AddFile(IFormFile file)
        {
            string extension=Path.GetExtension(file.FileName);
            string fileName = Guid.NewGuid().ToString()+ extension;
            string relativePath = @"Files\" + fileName;
            string path=Path.Combine(webRootPath, relativePath);
            using FileStream fs = new FileStream(path, FileMode.Create);
            await file.CopyToAsync(fs);
            return relativePath;
        }*/

        public Task<bool> DeleteFileAsync(string filePath)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteFileAsync(Guid Id, string filePath)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteFilesAsync(List<string> filePaths)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteFilesAsync(List<Guid> Ids, List<string> filePaths)
        {
            throw new NotImplementedException();
        }

        /* public async Task<string> UploadFileAsync(IFormFile file)
         {
             var dirPath = GetPhysicalDirectoryPath();
             var extension = Path.GetExtension(file.FileName).ToLower();
             var size = file.Length;
             if(size > 10240)
             {
                 throw new Exception("File size exceeded");
             }
            *//* if(extension !=".jpg" || extension !=".png" || extension !=".jpeg")
             {
                 throw new Exception(" File Format not supported");
             }*//*

             if (!file.ContentType.Contains("images/"))
             {
                 throw new Exception("");
             }

              string filename =string.Concat(Guid.NewGuid(), file.FileName);
              string  fullPath=Path.Combine(dirPath, filename);
             using FileStream fs = new FileStream(fullPath, FileMode.Create);
             await file.CopyToAsync(fs);
             return string.Concat(GetVirtualDirectoryPath, filename);
         }*/

        /*  public async  Task<AppFileResponse> UploadFileAsync(AppModule module, Guid EntityId, IFormFile file)
          {
             string filePath= await storageService.UploadFileAsync(file);
              AppFiles appFiles = new AppFiles()
              {
                  Module = module,
                  EntityId = EntityId,
                  FilePath = filePath

              };
           int retVal=  await fileRepository.InsertAsync(appFiles);
              if (retVal > 0)
              {
                  AppFileResponse appFileResponse = new()
                  {
                      AppModule = module,
                      EntityId = EntityId,
                      FilePath = filePath,

                  };
                  return appFileResponse;
              }
              throw new Exception("failed to insert file Record");


          }*/
        public async Task<AppFileResponse> UploadFileAsync(AppModule module, Guid entityId, IFormFile file)
        {
            // Check if file is null
            if (file == null || file.Length == 0)
            {
                throw new ArgumentException("Invalid file");
            }

            // Upload the file and get the file path
            string filePath = await storageService.UploadFileAsync(file);
            if (string.IsNullOrEmpty(filePath))
            {
                throw new Exception("File upload failed, returned file path is null or empty");
            }

            // Create a new AppFiles record
            AppFiles appFiles = new AppFiles()
            {
                Module = module,
                EntityId = entityId,
                FilePath = filePath
            };

            // Insert the AppFiles record into the repository
            int retVal = await fileRepository.InsertAsync(appFiles);
            if (retVal > 0)
            {
                // Create and return a successful response
                AppFileResponse appFileResponse = new AppFileResponse()
                {
                    AppModule = module,
                    EntityId = entityId,
                    FilePath = filePath
                };
                return appFileResponse;
            }

            // Throw an exception if the insertion failed
            throw new Exception("Failed to insert AppFiles record");
        }



        public Task<bool> UploadFilesAsync(IFormCollection files)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<AppFileResponse>> UploadFilesAsync(AppModule module, Guid entityId, IFormFileCollection files)
        {
            throw new NotImplementedException();
        }

       
    }
}
