using Microsoft.AspNetCore.Mvc;

namespace Social_Media_Platform.Controllers
{
    public class PrivacySettingsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
