using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Text;
using WebBanHangOnline.Models;
using WebBanHangOnline.Models.DbContext;
using WebBanHangOnline.Models.Entity;
using TbProductCategory = WebBanHangOnline.Models.TbProductCategory;


namespace WebBanHangOnline.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = $"{TbStaticField.Role_Admin}")]
    public class ProductCategoryController : Controller
    {
        private WebBanHangOnlineContext _db;
        public ProductCategoryController(WebBanHangOnlineContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var items = _db.TbProductCategories;
            return View(items);
        }

        public IActionResult Add_MVC()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add_MVC(TbProductCategory tbProductCategory)
        {
            tbProductCategory.Id = Guid.NewGuid();
            _db.TbProductCategories.Add(tbProductCategory);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit_MVC(Guid? id)
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
            if (item == null) { return NotFound(); }
            _db.Entry(item).State = EntityState.Detached;
            _db.TbProductCategories.Update(tbProductCategory);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> changeStatus(Guid? id)
        {
            var item = _db.TbProductCategories.Find(id);
            if (item == null) { return NotFound(); }
            else
            {
                item.IsActive = !item.IsActive;
            }
            _db.TbProductCategories.Update(item);
            _db.SaveChanges();
            return Ok(item);
        }

        [Route("/admin/ProductCategory/search")]
        public async Task<IActionResult> Search(string? searchStr)
        {
            var items = await _db.TbProductCategories
                .ToListAsync();
            if (!string.IsNullOrEmpty(searchStr))
            {
                string searchTerm = RemoveDiacritics(searchStr);

                var result = items.Where(x =>
                    RemoveDiacritics(x.Title)
                        .ToUpper().Contains(searchTerm.ToUpper()) ||
                    RemoveDiacritics(x.Id.ToString())
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
