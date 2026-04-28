using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Chiro.Application.Dtos.Blob;
using Chiro.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Chiro.Infrastructure.Repositories
{
    public class BlobRepository : IBlobRepository
    {
        private readonly BlobContainerClient _containerClient;

        public BlobRepository(BlobContainerClient containerClient)
        {
            _containerClient = containerClient;
        }

        public Task DeleteGroupImageAsync(Guid groupId)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteUserProfileImageAsync(string url)
        {
            var uri = new Uri(url);
            //extract absolute blob path from the url
            var blobPath = uri.AbsolutePath.TrimStart('/').Split('/', 2)[1];
            var blobClient = _containerClient.GetBlobClient(blobPath);
            await blobClient.DeleteIfExistsAsync();
        }

        public async Task<string> UploadGroupImageAsync(Guid groupId, Stream stream, string contentType)
        {
            var fileName = $"groups/{groupId}/profile.jpg";

            var blobClient = _containerClient.GetBlobClient(fileName);

            await blobClient.UploadAsync(stream, new BlobUploadOptions
            {
                HttpHeaders = new BlobHttpHeaders
                {
                    ContentType = contentType
                }
            });

            return blobClient.Uri.AbsoluteUri;
        }

        public async Task<string> UploadUserProfileImageAsync(Guid userId, Stream stream, string contentType)
        {
            //Generate new name every time to avoid caching issues
            var fileName = $"users/{userId}/{DateTime.UtcNow.ToString("yyyyMMddHHmmssfff")}/profile.jpg";

            var blobClient = _containerClient.GetBlobClient(fileName);

            await blobClient.UploadAsync(stream, new BlobUploadOptions
            {
                HttpHeaders = new BlobHttpHeaders
                {
                    ContentType = contentType
                }
            });

            return blobClient.Uri.AbsoluteUri;
        }


    }
}
