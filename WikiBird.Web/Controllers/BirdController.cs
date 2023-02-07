using Microsoft.AspNetCore.Mvc;

namespace WikiBird.Web.Controllers
{
    public class BirdController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
