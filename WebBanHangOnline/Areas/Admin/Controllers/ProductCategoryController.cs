using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using WebBanHangOnline.Models;

namespace WebBanHangOnline.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductCategoryController : Controller
    {
        private WebBanHangDemoContext _db;
        public ProductCategoryController(WebBanHangDemoContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var items = _db.TbProductCategories;
            return View(items);
        }

        public IActionResult Add_MVC() {
            return View();
        }

        [HttpPost]
        public IActionResult Add_MVC(TbProductCategory tbProductCategory)
        {
            tbProductCategory.CreatedDate = DateTime.Now;
            tbProductCategory.Alias = WebBanHangOnline.Models.Common.Filter.FilterChar(tbProductCategory.Title);
            _db.TbProductCategories.Add(tbProductCategory);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit_MVC(int? id)
        {
            var item = _db.TbProductCategories.Find(id);
            if (item == null) { return NotFound(); }   
            
            return View(item);
        }

        [HttpPost]
        public IActionResult Edit_MVC(TbProductCategory tbProductCategory)
        {
            // tìm kiếm item cũ trong db
            // tách item cũ khỏi entity
            // luu các createby, createdate vào item mới
            // cập nhập các thông tin
            // luu vào db
            var item = _db.TbProductCategories.Find(tbProductCategory.Id);
            if(item == null) { return NotFound(); }
            _db.Entry(item).State = EntityState.Detached;

            tbProductCategory.ModifierDate = DateTime.Now;
            tbProductCategory.CreatedDate = item.CreatedDate;
            tbProductCategory.CreatedBy = item.CreatedBy;
            _db.TbProductCategories.Update(tbProductCategory);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> changeStatus(int? id)
        {
            var item = _db.TbProductCategories.Find(id);
            if(item == null) { return NotFound(); }
            else
            {
                item.IsActive = !item.IsActive;
            }
            _db.TbProductCategories.Update(item);
            _db.SaveChanges();
            return Ok(item);
        }
    }
}
