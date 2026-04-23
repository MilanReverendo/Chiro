using Chiro.Application.Dtos.Blob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chiro.Application.Interfaces
{
    public interface IBlobRepository
    {
        Task<BlobObject> GetBlobFile(string url);
        Task<string> UploadBlobFile(Stream stream, string fileName);
        Task DeleteBlob(string path);
    }
}
