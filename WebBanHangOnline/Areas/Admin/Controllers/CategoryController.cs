using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using WebBanHangOnline.Models;
using Microsoft.EntityFrameworkCore;
using WebBanHangOnline.Models.ViewModels;

namespace WebBanHangOnline.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly WebBanHangDemoContext _db;

        public CategoryController(WebBanHangDemoContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var item = _db.TbCategories;

            return View(item);
        }

        [Route("category-add")]
        public IActionResult Add()
        {
            return View();
        }

        [Route("category-add")]
        [HttpPost]
        public IActionResult Add(TbCategory tbCategory)
        {
            // Kiểm tra dữ liệu người dùng nhập vào
            if (ModelState.IsValid)
            {
                tbCategory.CreatedDate = DateTime.Now;
                tbCategory.ModifierDate = DateTime.Now;
                tbCategory.Alias = WebBanHangOnline.Models.Common.Filter.FilterChar(tbCategory.Title);
                _db.TbCategories.Add(tbCategory);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        [Route("add-jquery")]
        [HttpPost]
        public IActionResult Add_Jquery(TbCategory tbCategory)
        {
            tbCategory.CreatedDate = DateTime.Now;
            tbCategory.ModifierDate = DateTime.Now;
            tbCategory.Alias = WebBanHangOnline.Models.Common.Filter.FilterChar(tbCategory.Title);

            _db.TbCategories.Add(tbCategory);
            _db.SaveChanges();

            return Ok(tbCategory);
        }

        [Route("loaddata")]
        [HttpPost]
        public async Task<IActionResult> LoadData()
        {
            var list_Category = await _db.TbCategories.ToListAsync();

            return Ok(list_Category);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            TbCategory? tbCategory = _db.TbCategories.Find(id);
            if (tbCategory == null)
            {
                return NotFound();
            }

            return View(tbCategory);
        }

        [HttpPost]
        public IActionResult Edit(TbCategory tbCategory)
        {
            if (ModelState.IsValid)
            {
                TbCategory? category_edit = _db.TbCategories.Find(tbCategory.Id);

                if (category_edit != null)
                {
                    _db.Entry(category_edit).State = EntityState.Detached; // Detach the existing entity
                    tbCategory.CreatedDate = category_edit.CreatedDate;
                    tbCategory.CreatedBy = category_edit.CreatedBy;
                    tbCategory.ModifierBy = category_edit.ModifierBy;
                    tbCategory.ModifierDate = DateTime.Now;
                    tbCategory.Alias = WebBanHangOnline.Models.Common.Filter.FilterChar(tbCategory.Title);
                    _db.TbCategories.Update(tbCategory);
                    _db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View();
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            TbCategory? tbCategory = _db.TbCategories.Find(id);
            if (tbCategory == null)
            {
                return NotFound();
            }

            return View(tbCategory);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteCategory(int? id)
        {
            if (ModelState.IsValid)
            {
                var item = _db.TbCategories.FirstOrDefault(x => x.Id == id);
                if (item != null)
                {
                    _db.TbCategories.Remove(item);
                    _db.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }

        [Area("Admin")]
        [Route("test-jquery")]
        [HttpPost]
        public async Task<IActionResult> DeleteCategory_jquery(int? id)
        {
            var item = _db.TbCategories.FirstOrDefault(x => x.Id == id);
            if (item != null)
            {
                _db.TbCategories.Remove(item);
                _db.SaveChanges();
            }
            return Ok();
        }

        [Area("Admin")]
        [Route("editcategoryajax")]
        public async Task<IActionResult> editCategory_ajax(int? id)
        {
            var item = _db.TbCategories.FirstOrDefault(x => x.Id == id);
            return Ok(item);
        }

        [Area("Admin")]
        [Route("editcategoryajax")]
        [HttpPost]
        public async Task<IActionResult> editCategory_ajax(TbCategory? tbCategory)
        {
            TbCategory? category_edit = _db.TbCategories.Find(tbCategory.Id);

            if (category_edit != null)
            {
                _db.Entry(category_edit).State = EntityState.Detached; // Detach the existing entity
                tbCategory.CreatedDate = category_edit.CreatedDate;
                tbCategory.CreatedBy = category_edit.CreatedBy;
                tbCategory.ModifierBy = category_edit.ModifierBy;
                tbCategory.ModifierDate = DateTime.Now;
                tbCategory.Alias = WebBanHangOnline.Models.Common.Filter.FilterChar(tbCategory.Title);
                _db.TbCategories.Update(tbCategory);
                _db.SaveChanges();
            }
            return Ok(tbCategory);
        }
    }
}
