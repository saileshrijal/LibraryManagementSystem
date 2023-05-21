using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Helpers.Interface
{
    public interface IFileHelper
    {
        Task<string> UploadFile(IFormFile file, string folderName);
    }
}