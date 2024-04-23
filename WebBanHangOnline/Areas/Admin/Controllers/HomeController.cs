using Microsoft.AspNetCore.Mvc;

namespace WebBanHangOnline.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        
        public IActionResult Index()
        {
            var controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            ViewBag.ControllerName = controllerName;


            return View();
        }
    }
}
