using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using CsvLoader.Services.Implementations;
using CsvLoader.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CsvUploader.Api.Controllers
{
    public class ProductFileUploadController : Controller
    {
        private readonly IProductUploadService _productUploadService;

        public ProductFileUploadController(IProductUploadService productUploadService)
        {
            _productUploadService = productUploadService;
        }

        [HttpPost]
        public async Task<IActionResult> Index([FromForm]IList<IFormFile> files)
        {
            foreach (var file in files)
            {
                using(var streamReader = new StreamReader(file.OpenReadStream()))
                {
                    await _productUploadService.UploadCsvAndPersistProductsAsync(streamReader);
                }
            }
            return Ok();
        }
    }
}