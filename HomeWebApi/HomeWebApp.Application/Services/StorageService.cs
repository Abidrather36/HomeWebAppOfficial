using HomeWebApp.Application.Abstraction.IServices;
using Microsoft.AspNetCore.Http;

namespace HomeWebApp.Application.Services
{
    public class StorageService : IStorageService
    {
        private readonly string webRootPath;

        public StorageService(string webRootPath)
        {
            this.webRootPath = webRootPath;
        }
        public Task<bool> DeleteFileAsync(string filePath)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteFilesAsync(List<string> filePaths)
        {
            throw new NotImplementedException();
        }

        /* public async  Task<string> UploadFileAsync(IFormFile file)
         {
             var dirPath = GetPhysicalDirectoryPath();
             var fileName=string.Concat(Guid.NewGuid(), file.FileName);
             var fullPath =Path.Combine(dirPath, fileName);
             using FileStream fs = new FileStream(fullPath, FileMode.Create);
             await file.CopyToAsync(fs);
             return string.Concat(GetVirtualDirectoryPath ,fileName);
         }*/
        /*  public async Task<string> UploadFileAsync(IFormFile file)
          {
              string dirPath = GetPhysicalDirectoryPath();
              // ValidateFile(file);
              string extension = Path.GetExtension(file.FileName);
              string fileName = string.Concat(Guid.NewGuid(), extension);
              string fullPath = Path.Combine(dirPath, fileName);

              using FileStream fs = new FileStream(fullPath, FileMode.Create);
              await file.CopyToAsync(fs);

              return GetVirtualDirectoryPath() + fileName;
          }*/

        public async Task<string> UploadFileAsync(IFormFile file)
        {
            // Check if file is null
            if (file == null || file.Length == 0)
            {
                throw new ArgumentException("Invalid file");
            }

            // Get the physical directory path
            string dirPath = GetPhysicalDirectoryPath();
            if (string.IsNullOrEmpty(dirPath))
            {
                throw new Exception("Directory path is invalid");
            }

            // Generate a unique file name with the same extension
            string extension = Path.GetExtension(file.FileName);
            string fileName = string.Concat(Guid.NewGuid(), extension);
            string fullPath = Path.Combine(dirPath, fileName);

            try
            {
                // Save the file to the specified path
                using FileStream fs = new FileStream(fullPath, FileMode.Create);
                await file.CopyToAsync(fs);
            }
            catch (Exception ex)
            {
                // Handle exceptions that may occur during file upload
                throw new Exception("File upload failed", ex);
            }

            // Get the virtual directory path
            string virtualPath = GetVirtualDirectoryPath();
            if (string.IsNullOrEmpty(virtualPath))
            {
                throw new Exception("Virtual directory path is invalid");
            }

            // Return the virtual directory path concatenated with the file name
            return Path.Combine(virtualPath, fileName);
        }

        public Task<List<string>> UploadFilesAsync(IFormFileCollection files)
        {
            throw new NotImplementedException();
        }

        private string GetPhysicalDirectoryPath()
        {
            var dirPath=Path.Combine(webRootPath, "Files");
            if (Directory.Exists(dirPath))
            {
                return dirPath;
            }
            Directory.CreateDirectory(dirPath);
            return dirPath;
        }
        private void ValidateFile(IFormFile file, string contentType)
        {
            string extension = Path.GetExtension(file.FileName).ToLower();
            long len = 0;
            var size = file.Length;


            //if (extension != ".jpg" || extension != ".jpeg" || extension !=".png")
            //{
            //    throw new Exception("File format not supported");
            //}
            if (!file.ContentType.Contains("image"))
            {
                throw new Exception("File format not supported");
            }
            if (file.ContentType.Contains("images"))
            {
                len = 102404;
            }
            if (file.ContentType.Contains("video"))
            {
                len = 10240400;
            }
            if (file.ContentType.Contains("application/pdf"))
            {
                len = 52002;
            }
            if (size > len)
            {
                throw new Exception("File size exceeded the limit");
            }
        }

        private string GetVirtualDirectoryPath() => "Files/";
    }
}
