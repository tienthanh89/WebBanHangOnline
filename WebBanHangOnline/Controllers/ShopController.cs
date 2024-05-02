using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebBanHangOnline.Models;
using WebBanHangOnline.Models.ViewModels.User;

namespace WebBanHangOnline.Controllers
{
    public class ShopController : Controller
    {
        private readonly WebBanHangDemoContext _db;

        public ShopController(WebBanHangDemoContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> loadData(int? id)
        {
            var items = _db.TbProducts.AsQueryable();
            if (id.HasValue)
            {
                items = items.Where(x => x.ProductCategoryId == id.Value);
            }
            var result = items.Include(x=>x.tbProductCategory).Select(x => new shopProductVM
            {
                Id = x.Id,
                Title = x.Title,
                ImageUrl = x.ImageUrl,
                Price = x.Price,
                Description = x.Description,
                ProductCategory = x.tbProductCategory.Title,
            });
            return Ok(result);
        }
        
        public async Task<IActionResult> loadShopDetail(int? id)
        {
            var item = await _db.TbProducts.Include(x=> x.tbProductCategory).Where(x=>x.Id == id).FirstOrDefaultAsync();
            if(item == null) { return NotFound(); }
            var result = new shopDetailVM
            {
                Id = item.Id,
                Title = item.Title,
                ImageUrl = item.ImageUrl,
                Category = item.tbProductCategory.Title,
                Price = item.Price,
                Description = item.Description,
                Detail = item.Detail,
                Quantity = item.Quantity,
            };
            return Ok(result);
        }
    }
}
