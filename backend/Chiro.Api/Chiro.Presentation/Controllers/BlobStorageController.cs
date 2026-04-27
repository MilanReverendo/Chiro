//using Chiro.Application.Dtos.Blob;
//using Chiro.Application.Interfaces;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Chiro.Presentation.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class BlobStorageController : ControllerBase
//    {
//        private readonly IBlobRepository _blobRepository;

//        public BlobStorageController(IBlobRepository blobRepository)
//        {
//            _blobRepository = blobRepository;
//        }

//        [HttpPost("UploadBlobFile")]
//        public async Task<IActionResult> UploadBlobFile(IFormFile file)
//        {
//            if (file == null || file.Length == 0)
//                return BadRequest("No file uploaded");

//            using var stream = file.OpenReadStream();

//            var result = await _blobRepository.UploadBlobFile(stream, file.FileName);

//            return Ok(result);
//        }

//        [HttpGet("GetBlobFile")]
//        public async Task<IActionResult> GetBlobFile(string url)
//        {
//            var result = await _blobRepository.GetBlobFile(url);
//            return File(result.Content, result.ContentType);
//        }

//        [HttpDelete("DeleteBlob")]
//        public async Task<IActionResult> DeleteBlob(string url)
//        {
//            _blobRepository.DeleteBlob(url);
//            return NoContent();
//        }
//    }
//}
