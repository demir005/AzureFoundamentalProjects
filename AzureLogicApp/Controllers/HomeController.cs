using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using AzureLogicApp.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;

namespace AzureLogicApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        static readonly HttpClient httpClient = new HttpClient();
        private readonly BlobServiceClient _blobClient;

        public HomeController(ILogger<HomeController> logger, BlobServiceClient blobClient)
        {
            _logger = logger;
            _blobClient = blobClient;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(SpookyRequest spookyRequest, IFormFile file)
        {
            spookyRequest.Id = Guid.NewGuid().ToString();
            var jsonContent = JsonConvert.SerializeObject(spookyRequest);

            using (var content = new StringContent(jsonContent, Encoding.UTF8, "application/json"))
            {
                HttpResponseMessage httpResposne = await httpClient.PostAsync("https://prod-12.northcentralus.logic.azure.com:443/workflows/cf294e4e7edc4bfeb833fcb8100988bc/triggers/manual/paths/invoke?api-version=2016-10-01&sp=%2Ftriggers%2Fmanual%2Frun&sv=1.0&sig=OiL8EfKtT9NsgwFSRt3WOvzG6rQN3EiaokKxse8tg-g", content);
            }
            if (file != null)
            {
                var fileName = spookyRequest.Id + Path.GetExtension(file.FileName);
                BlobContainerClient blobContainerClient = _blobClient.GetBlobContainerClient("logicappholder");
                var blobClient = blobContainerClient.GetBlobClient(fileName);

                var httpHeaders = new BlobHttpHeaders()
                {
                    ContentType = file.ContentType
                };
                await blobClient.UploadAsync(file.OpenReadStream(), httpHeaders);
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}