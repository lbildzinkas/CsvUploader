using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using CsvLoader.Services.Implementations;
using CsvLoader.Services.Interfaces;
using CsvUploader.Api.Attributes;
using CsvUploader.Api.Providers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Net.Http.Headers;
namespace CsvUploader.Api.Controllers
{
    [Route("ProductFileUpload")]
    [ApiController]
    public class ProductFileUploadController : Controller
    {
        private static readonly FormOptions _defaultFormOptions = new FormOptions();
        private readonly IProductUploadService _productUploadService;
        private readonly IMultipartRequestUtilitiesProvider _multipartRequestUtilitiesProvider;

        public ProductFileUploadController(IProductUploadService productUploadService, IMultipartRequestUtilitiesProvider multipartRequestUtilitiesProvider)
        {
            _productUploadService = productUploadService;
            _multipartRequestUtilitiesProvider = multipartRequestUtilitiesProvider;
        }

        // [HttpGet]
        // [GenerateAntiforgeryTokenCookieForAjax]
        // public IActionResult Index()
        // {
        //     return View();
        // }

        [HttpPost("upload")]
        [DisableFormValueModelBinding]
        public async Task<IActionResult> Upload()
        {
            if (!_multipartRequestUtilitiesProvider.IsMultipartContentType(Request.ContentType))
            {
                return BadRequest($"Expected a multipart request, but got {Request.ContentType}");
            }

            string targetFilePath = null;

            var boundary = _multipartRequestUtilitiesProvider.GetBoundary(
                MediaTypeHeaderValue.Parse(Request.ContentType),
                _defaultFormOptions.MultipartBoundaryLengthLimit);
            var reader = _multipartRequestUtilitiesProvider.CreateMultipartReader(boundary, HttpContext.Request.Body);

            var section = await reader.ReadNextSectionAsync();
            while (section != null)
            {
                var hasContentDispositionHeader = ContentDispositionHeaderValue.TryParse(section.ContentDisposition, out var contentDisposition);

                if (hasContentDispositionHeader)
                {
                    if (_multipartRequestUtilitiesProvider.HasFileContentDisposition(contentDisposition))
                    {
                        targetFilePath = Path.GetTempFileName();
                        using (var targetStream = System.IO.File.Create(targetFilePath))
                        {
                            await section.Body.CopyToAsync(targetStream);
                        }
                        await _productUploadService.UploadCsvAndPersistProductsAsync(targetFilePath);
                    }
                }

                // Drains any remaining section body that has not been consumed and
                // reads the headers for the next section.
                section = await reader.ReadNextSectionAsync();
            }
            return Ok();
        }
    }
}