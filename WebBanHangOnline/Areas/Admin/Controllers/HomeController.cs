using Microsoft.AspNetCore.Mvc;

namespace WebBanHangOnline.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        [Area("/Admin")]
        [Route("admin")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
