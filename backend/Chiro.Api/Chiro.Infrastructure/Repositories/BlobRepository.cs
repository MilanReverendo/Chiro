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
        public static readonly List<string> ImageExtensions = new List<string> { ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".tiff", ".svg" };

        public BlobRepository(BlobContainerClient containerClient)
        {
            _containerClient = containerClient;
        }

        public async Task DeleteBlob(string path)
        {
            var fileName = Path.GetFileName(new Uri(path).LocalPath);
            var blobClient = _containerClient.GetBlobClient(fileName);
            await blobClient.DeleteIfExistsAsync();
        }

        public async Task<BlobObject> GetBlobFile(string url)
        {
            var fileName = Path.GetFileName(new Uri(url).LocalPath);

            try
            {
                var blobClient = _containerClient.GetBlobClient(fileName);
                if (await blobClient.ExistsAsync()) 
                { 
                    var response = await blobClient.DownloadAsync();
                    var content = response.Value;
                    var downloadedData = content.Content;
                    if (ImageExtensions.Contains(Path.GetExtension(fileName).ToLower()))
                    {
                        var extension = Path.GetExtension(fileName).ToLower();
                        return new BlobObject { Content = downloadedData, ContentType = "image/" + extension.Remove(0,1) };
                    }
                    else
                    {
                        return new BlobObject
                        {
                            Content = downloadedData,
                            ContentType = content.Details.ContentType
                        };
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex) 
            {
                throw;
            }
        }

        public async Task<string> UploadBlobFile(Stream stream, string fileName)
        {
            var blobClient = _containerClient.GetBlobClient(fileName);
            await blobClient.UploadAsync(stream, overwrite: true);

            return blobClient.Uri.AbsoluteUri;
        }
    }
}
