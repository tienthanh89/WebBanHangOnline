using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebBanHangOnline.Helper;
using WebBanHangOnline.Models;
using WebBanHangOnline.Models.DbContext;
using WebBanHangOnline.Models.Entity;
using WebBanHangOnline.Models.ViewModels;

namespace WebBanHangOnline.ViewComponents
{
    public class CartViewComponent : ViewComponent
    {
        private readonly WebBanHangOnlineContext _db;
        public CartViewComponent(WebBanHangOnlineContext db)
        {
            _db = db;
        }
        public IViewComponentResult Invoke()
        {
            // Create total cart of 1 user
            var totalCart = 0;
            // Claim user Id
            if (User.Identity.IsAuthenticated)
            {
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
                // Check shopping cart
                var totalCartFromDb = _db.ShoppingCarts.Where(x => x.ApplicationUserId == userId).ToList();
                if (totalCartFromDb != null)
                {
                    foreach(var item  in totalCartFromDb)
                    {
                        totalCart += item.Quantity;
                    } 
                    return View("CartPanel", new CartViewComponentVM
                    {
                        TotalCart = totalCart,
                    });
                }
                return View("CartPanel", new CartViewComponentVM());
            }
            return View("CartPanel", new CartViewComponentVM());
        }
    }
}
