using Microsoft.AspNetCore.Mvc;

namespace ResumeClient.Controllers
{
    public class MessageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
