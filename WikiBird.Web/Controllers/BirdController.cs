using Microsoft.AspNetCore.Mvc;
using WikiBird.Model.Entities;
using WikiBird.Web.Services;

namespace WikiBird.Web.Controllers
{
    public class BirdController : Controller
    {
        public IBirdService birdService;

        public BirdController(IBirdService birdService)
        {
            this.birdService = birdService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetBird(int id)
        {
            var bird = birdService.getBird(id);
            return View(bird);
        }

        [HttpGet]
        public IActionResult GetAllBirds()
        {
            var birds = birdService.getAll();
            return View(birds);
        }

        [HttpGet]
        public IActionResult CreateBird()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateBird(Bird bird)
        {
            birdService.createBird(bird);
            return View();
        }

    }
}