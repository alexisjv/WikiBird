using Microsoft.AspNetCore.Mvc;
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

            string path = await mLService.uploadImage(image);

            return RedirectToAction("PredictBird", new { imagePath = path });
        }

        public IActionResult PredictBird(string imagePath)
        {

            var result = mLService.predictBird(imagePath);

            mLService.deleteImage(imagePath);

            return View("ShowResult", result.Prediction);

        }

    }
    }
