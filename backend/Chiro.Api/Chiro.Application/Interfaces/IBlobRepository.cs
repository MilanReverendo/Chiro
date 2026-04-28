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
        Task<string> UploadUserProfileImageAsync(Guid userId, Stream stream, string contentType);
        Task<string> UploadGroupImageAsync(Guid userId, Stream stream, string contentType);
        Task DeleteUserProfileImageAsync(string url);
        Task DeleteGroupImageAsync(Guid groupId);
    }
}
