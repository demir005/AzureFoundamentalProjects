using AzureBlobProject.Services;
using Microsoft.AspNetCore.Mvc;

namespace AzureFoundamentalsProject.Controllers
{
    public class BlobController : Controller
    {
        private readonly IBlobService _blobService;

        public BlobController(IBlobService blobService)
        {
            _blobService = blobService;
        }

        [HttpGet]
        public async Task<IActionResult> Manage(string containerName)
        {
            var allBlobs = await _blobService.GetAllBlobs(containerName);
            return View(allBlobs);
        }

        [HttpGet]
        public IActionResult AddFile()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddFile(IFormFile file)
        {
            if (file == null || file.Length < 1) return View();

            // file name  - xps_img2.png
            //new name - xps_img2_GUIDHERE.png
            var fileName  = Path.GetFileNameWithoutExtension(file.Name) + "_"+Guid.NewGuid()+Path.GetExtension(file.FileName);
            var result = await _blobService.UploadBlob(fileName, file, "images");
            if(result)
                return RedirectToAction("Index","Container");
            return View();
        }
    }
}
