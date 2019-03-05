using System.Threading.Tasks;
using CsvUploader.Api.Attributes;
using CsvUploader.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CsvUploader.Api.Controllers
{
    [Route("ProductFileUpload")]
    [ApiController]
    public class ProductFileUploadController : Controller
    {
        private readonly IFileUploadService _fileUploadService;

        public ProductFileUploadController(IFileUploadService fileUploadService)
        {
            _fileUploadService = fileUploadService;
        }

        [HttpPost("upload")]
        [DisableFormValueModelBinding]
        public async Task<IActionResult> Upload()
        {
            // if (!_multipartRequestUtilitiesProvider.IsMultipartContentType(Request.ContentType))
            // {
            //     return BadRequest($"Expected a multipart request, but got {Request.ContentType}");
            // }

            await _fileUploadService.UploadFile(Request.ContentType, HttpContext.Request.Body);

            return Ok();
        }
    }
}