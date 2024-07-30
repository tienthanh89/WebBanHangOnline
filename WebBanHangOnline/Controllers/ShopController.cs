using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Security.Claims;
using System.Text;
using WebBanHangOnline.Data.Repository;
using WebBanHangOnline.Helper;
using WebBanHangOnline.Models;
using WebBanHangOnline.Models.DbContext;
using WebBanHangOnline.Models.Entity;
using WebBanHangOnline.Models.ViewModels;
using WebBanHangOnline.Models.ViewModels.User;

namespace WebBanHangOnline.Controllers
{
    public class ShopController : Controller
    {
        private readonly WebBanHangOnlineContext _db;

        public ShopController(WebBanHangOnlineContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }

        const string CART_KEY = "MYCART";
        public List<CartItemVM> Cart => HttpContext.Session.Get<List<CartItemVM>>(CART_KEY) ?? new List<CartItemVM>();

        [HttpPost]
        public async Task<IActionResult> loadData(Guid? id)
        {
            var items = _db.TbProducts.Where(x=>x.IsActive).AsQueryable();
            if (id.HasValue)
            {
                items = items.Where(x => x.ProductCategoryId == id.Value );
            }
            var result = items.Include(x => x.tbProductCategory).Select(x => new ShopProductVM
            {
                Id = x.Id,
                Title = x.Title,
                ImageUrl = x.ImageUrl,
                Price = x.Price,
                ProductCategory = x.tbProductCategory.Title,
            });
            return Ok(result);
        }

        public async Task<IActionResult> loadShopDetail(Guid? id)
        {
            var item = await _db.TbProducts.Include(x => x.tbProductCategory).Where(x => x.Id == id).FirstOrDefaultAsync();
            if (item == null) { return NotFound(); }
            var result = new ShopDetailVM
            {
                Id = item.Id,
                Title = item.Title,
                ImageUrl = item.ImageUrl,
                Category = item.tbProductCategory.Title,
                Price = item.Price,
                Detail = item.Detail,
                Quantity = item.Quantity,
            };
            return Ok(result);
        }


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddToCart(ShoppingCart shoppingCart)
        {
            // Create total product of 1 user
            var totalProduct = 0;
            // Claim user Id
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            // Check cart of 1 product
            var cartFromDb = _db.ShoppingCarts.Where(x => x.ApplicationUserId == userId && x.ProductId == shoppingCart.ProductId).FirstOrDefault();
            
            
            if(cartFromDb != null)
            {
                // shopping cart exits
                cartFromDb.Quantity += shoppingCart.Quantity;
                _db.Update(cartFromDb);
                _db.SaveChanges();
            }
            else
            {
                // add cart record
                shoppingCart.Id = new Guid();
                shoppingCart.ApplicationUserId = userId;
                _db.Add(shoppingCart);
                _db.SaveChanges();
            }

            // Check total product of user
            var totalCartFromDb = _db.ShoppingCarts.Where(x => x.ApplicationUserId == userId).ToList();
            foreach (var item in totalCartFromDb)
            {
                totalProduct += item.Quantity;
            }
            return Ok(totalProduct);


            //var gioHang = Cart;
            //var item = gioHang.SingleOrDefault(x => x.Id == id);
            //if (item == null)
            //{
            //    var product = _db.TbProducts.SingleOrDefault(x => x.Id == id);
            //    if (product == null)
            //    {
            //        return NotFound();
            //    }
            //    item = new CartItemVM
            //    {
            //        Id = product.Id,
            //        Title = product.Title,
            //        Price = product.Price ?? 0,
            //        Quantity = quantity,
            //        ImageUrl = product.ImageUrl ?? string.Empty,
            //    };
            //    gioHang.Add(item);
            //}
            //else
            //{
            //    item.Quantity = quantity;
            //}
            //HttpContext.Session.Set(CART_KEY, gioHang);
            //return Ok(gioHang);
        }

        
        public async Task<IActionResult> Search(string? searchStr)
        {
            var items = await _db.TbProducts
                .ToListAsync();
            if (!string.IsNullOrEmpty(searchStr) && items.Count > 0)
            {
                string searchTerm = RemoveDiacritics(searchStr);

                var result = items.Where(x =>
                    RemoveDiacritics(x.Title)
                        .ToUpper().Contains(searchTerm.ToUpper()) ||
                    RemoveDiacritics(x.Price.ToString(CultureInfo.InvariantCulture))
                        .ToUpper().Contains(searchTerm.ToUpper())
                );

                return Ok(result);
            }

            return Ok(items);
        }

        private static string RemoveDiacritics(string text)
        {
            string normalizedString = text.Normalize(NormalizationForm.FormD);
            string result = "";
            foreach (char c in normalizedString)
            {
                UnicodeCategory category = CharUnicodeInfo.GetUnicodeCategory(c);
                if (category != UnicodeCategory.NonSpacingMark)

                {
                    result += c;
                }
            }

            return result;
        }
    }
}
