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
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Upload()
        {
            await _fileUploadService.UploadFile(Request.ContentType, HttpContext.Request.Body);

            return Ok();
        }
    }
}