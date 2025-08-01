using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manage.BLL.Common.Services.Attachments
{
    public class AttachmentService : IAttachmentService 
    {

        private readonly List<string> _allowedExtensions = new List<string>
            () { ".jpg", ".jpeg", ".png", ".pdf" };

        private const int _allowedMaxSize = 2_097_152; // 2 MB in bytes

        public async Task<string?> UploadAsync(IFormFile file, string folderName)
        {
           var extension = Path.GetExtension(file.FileName);

            if (!_allowedExtensions.Contains(extension))
                return null;

            if (file.Length > _allowedMaxSize)
                return null; 

            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files",folderName);

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }


            var fileName = $"{Guid.NewGuid()}{extension}"; // must be UNIque

            var filePath = Path.Combine(folderPath,fileName);

            // sreaming: Data Per Time

           // using var fileStream = File.Create(filePath);
            using var fileStream = new FileStream(filePath,FileMode.Create);

            await file.CopyToAsync(fileStream);

            return fileName;

        }
        public bool DeleteAsync(string filePath)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                return true;
            }
            return false;
        }

    }
}
