using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Net;
using WikiBird.Model;
using WikiBird.Web.Services;

namespace WikiBird.Web.Controllers
{
    public class MLController : Controller
    {

        private IMLService mLService;

        public MLController(IMLService mLService)
        {
            this.mLService = mLService;
        }

        public void Index()
        {
        }

        [HttpPost]
        public async Task<IActionResult> UploadImage(IFormFile image)
        {
            if (image == null || image.Length == 0)
            {
                return Content("Seleccione una imagen válida");
            }

            var allowedImageExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };

            var imageExtension = Path.GetExtension(image.FileName).ToLowerInvariant();

            if (!allowedImageExtensions.Contains(imageExtension))
            {
                return Content("Sólo se permiten imágenes en formato JPEG, PNG o GIF");
            }

            string path = await mLService.uploadImage(image);

            return RedirectToAction("PredictBird", new { imagePath = path });
        }

        public IActionResult PredictBird(string imagePath)
        {

            var result = mLService.predictBird(imagePath);

            mLService.deleteImage(imagePath);
            string searchTerm = result.Prediction;
            string title = "";
            string extract = "";
            string urlImage = "";
            string url = $"https://es.wikipedia.org/w/api.php?action=query&titles={searchTerm}&prop=pageimages%7Cextracts&format=json&pithumbsize=300&exintro=&explaintext=";

            using (WebClient client = new WebClient())
            {
                string json = client.DownloadString(url);
                JObject data = JObject.Parse(json);
                JProperty firstPage = (JProperty)data["query"]["pages"].First();
                JObject page = (JObject)firstPage.Value;

                title = (string)page["title"];
                extract = (string)page["extract"];
                urlImage = (string)page["thumbnail"]["source"];


            var wikiResult = new WikiResult
            {
                title = title,
                description = extract,
                image = urlImage,
            };


            return View("ShowResult", wikiResult);

        }
    }
}
    }
