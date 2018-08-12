namespace AutoGarden.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class ScheduleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Add()
        {
            return View();
        }
    }
}