using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Mvc;
using WikiBird.Service;

namespace BlobStorageApi.Controllers
{
    public class BlobStorageController : Controller
    {

        private IBlobStorageService blobStorageService;

        public BlobStorageController(IBlobStorageService blobStorageService)
        {
            this.blobStorageService = blobStorageService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> uploadImage(IFormFile image)
        {
            string fileName = Path.GetRandomFileName();
            fileName = fileName.Replace(".", "") + ".jpg";
            using (Stream stream = image.OpenReadStream())
            {
                await blobStorageService.UploadBlobAsync(fileName, stream);
            }

            return RedirectToAction("ShowResult");

        }

        public async Task<IActionResult> ShowResult()
        {
            List<string> imagenes = await blobStorageService.GetBlobAsync();
            return View(imagenes);
        }
    }
}
