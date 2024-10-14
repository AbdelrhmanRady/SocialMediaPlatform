using Microsoft.AspNetCore.Mvc;

namespace Social_Media_Platform.Controllers
{
    public class NotificationSettingsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
