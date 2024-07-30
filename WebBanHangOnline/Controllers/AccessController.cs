using Microsoft.AspNetCore.Mvc;
using WebBanHangOnline.Models;
using WebBanHangOnline.Models.DbContext;

namespace WebBanHangOnline.Controllers
{
    public class AccessController : Controller
    {
        private readonly WebBanHangOnlineContext _db;

        public AccessController(WebBanHangOnlineContext db)
        {
            _db=db;
        }
        public IActionResult Login()
        {
            return View();
        }
    }
}
