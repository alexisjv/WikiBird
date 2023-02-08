using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Mvc;

namespace BlobStorageApi.Controllers
{
    public class BlobStorageController : Controller
    {

        private readonly BlobServiceClient blobServiceClient;

        public BlobStorageController(BlobServiceClient blobServiceClient)
        {
            this.blobServiceClient = blobServiceClient;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<string> uploadImage(IFormFile image)
        {
            string mensaje = "";
           
            try
            {
                BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient("birds");
                await containerClient.UploadBlobAsync(image.FileName, image.OpenReadStream());
                mensaje = "Archivo cargado con éxito";
            }
            catch(Exception ex)
            {
                mensaje = ex.Message;
            }

            return mensaje;
        }
    }
}
