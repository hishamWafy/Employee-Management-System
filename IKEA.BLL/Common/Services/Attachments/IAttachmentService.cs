using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http; // Add this using directive


namespace Manage.BLL.Common.Services.Attachments
{
    public interface IAttachmentService
    {

        Task<string?> UploadAsync(IFormFile file, string fileName);

        bool DeleteAsync(string filePath);








    }
}
