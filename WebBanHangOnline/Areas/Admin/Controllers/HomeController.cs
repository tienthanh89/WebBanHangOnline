using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebBanHangOnline.Models.DbContext;
using WebBanHangOnline.Models.Entity;

namespace WebBanHangOnline.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = $"{TbStaticField.Role_Admin}")]
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}
