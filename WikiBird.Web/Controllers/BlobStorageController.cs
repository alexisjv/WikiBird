using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text;
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
            Console.WriteLine("imagen que subo: " + image);

            string fileName = Path.GetRandomFileName();
            fileName = fileName.Replace(".", "") + ".jpg";
            string imageURL = "";
            using (Stream stream = image.OpenReadStream())
            {
                imageURL = await blobStorageService.UploadBlobAsync(fileName, stream);
            }
            Console.WriteLine("url de la imagen guardada en blob storage: " + imageURL);

            return RedirectToAction("ShowResult", new {image = imageURL});

        }

        public async Task<IActionResult> ShowResult(string image)
        {
            Console.WriteLine("url de la imagen guardada en blob storage que recibo por parametro: " + image);
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7239");
                var content = new StringContent(image, Encoding.UTF8, "application/json");
                var response = await client.PostAsync("/ml/index", content);
                Console.WriteLine("url que recibo despues del machine learning response: " + response);
                Console.WriteLine("url que recibo despues del machine learning content: " + response.Content);


                response.EnsureSuccessStatusCode();
                var resultString = await response.Content.ReadAsStringAsync();
                string decodedUrl = Uri.UnescapeDataString(resultString);
                Console.WriteLine("url que recibo despues del machine learning modificado: " + decodedUrl);

                return View("ShowResult", resultString);
            }

            /* List<string> imagenes = await blobStorageService.GetBlobAsync();
            return View(imagenes);*/
        }
    }
}
