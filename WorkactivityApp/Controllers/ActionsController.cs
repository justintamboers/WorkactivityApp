using Microsoft.AspNetCore.Mvc;

namespace WorkactivityApp.Controllers
{
    public class ActionsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
