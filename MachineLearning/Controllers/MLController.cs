using Microsoft.AspNetCore.Mvc;
using static System.Net.WebRequestMethods;

namespace MachineLearning.Controllers
{
    public class MLController : Controller
    {
        
        public void Index()
        {


        }

        [HttpPost]
        public async Task<IActionResult> UploadImage(IFormFile image)
        {
            if (image == null || image.Length == 0)
                return Content("image not selected");

            var path = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\images", image.FileName);
            Console.WriteLine("la ruta de la imagen es: " + path);
            using (var stream = new FileStream(path, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }

            return RedirectToAction("PredictBird", new { imagePath = path });
        }

        public IActionResult PredictBird(string imagePath)
        {

            //Load sample data
            var sampleData = new MLModel.ModelInput()
            {
                ImageSource = imagePath
            };

            //Load model and predict output
            var result = MLModel.Predict(sampleData);
            return Ok(result.Prediction);

        }

    }
}
