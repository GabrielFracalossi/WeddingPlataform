using Microsoft.AspNetCore.Mvc;

namespace WeddingPlatform.Controllers
{
    public class GiftRegistryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}