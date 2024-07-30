using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using WebBanHangOnline.Models;
using Microsoft.EntityFrameworkCore;
using WebBanHangOnline.Models.ViewModels;
using WebBanHangOnline.Data.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using WebBanHangOnline.Models.DbContext;
using WebBanHangOnline.Models.Entity;

namespace WebBanHangOnline.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("category")]
    [Authorize(Roles = $"{TbStaticField.Role_Admin}")]
    public class CategoryController : Controller
    {
        private readonly WebBanHangOnlineContext _db;
        public CategoryController(WebBanHangOnlineContext db)
        {
            _db = db;
        }

        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("loaddata")]
        public async Task<IActionResult> LoadData()
        {
            var list_Category = await _db.TbCategories.ToListAsync();

            return Ok(list_Category);
        }

        [Route("isactive")]
        [HttpPut]
        public async Task<IActionResult> isActive(Guid id)
        {
            var item = await _db.TbCategories.FirstOrDefaultAsync(x => x.Id == id);
            if (item == null)
            {
                return NotFound();
            }

            item.IsActive = !item.IsActive;

            _db.TbCategories.Update(item);
            _db.SaveChanges();

            return Ok(item.IsActive);
        }

        //[Route("category-add")]
        //public IActionResult Add()
        //{
        //    return View();
        //}

        //[Route("category-add")]
        //[HttpPost]
        //public IActionResult Add(TbCategory tbCategory)
        //{
        //    // Kiểm tra dữ liệu người dùng nhập vào
        //    if (ModelState.IsValid)
        //    {
        //        tbCategory.Id = Guid.NewGuid();
        //        tbCategory.CreatedDate = DateTime.Now;
        //        tbCategory.ModifierDate = DateTime.Now;
        //        tbCategory.Alias = WebBanHangOnline.Models.Common.Filter.FilterChar(tbCategory.Title);
        //        _db.TbCategories.Add(tbCategory);
        //        _db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View();
        //}

        //[Route("add-jquery")]
        //[HttpPost]
        //public IActionResult Add_Jquery(TbCategory tbCategory)
        //{
        //    tbCategory.CreatedDate = DateTime.Now;
        //    tbCategory.ModifierDate = DateTime.Now;
        //    tbCategory.Alias = WebBanHangOnline.Models.Common.Filter.FilterChar(tbCategory.Title);

        //    _db.TbCategories.Add(tbCategory);
        //    _db.SaveChanges();

        //    return Ok(tbCategory);
        //}

        

        //public IActionResult Edit(int? id)
        //{
        //    if (id == null || id == 0)
        //    {
        //        return NotFound();
        //    }

        //    TbCategory? tbCategory = _db.TbCategories.Find(id);
        //    if (tbCategory == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(tbCategory);
        //}

        //[HttpPost]
        //public IActionResult Edit(TbCategory tbCategory)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        TbCategory? category_edit = _db.TbCategories.Find(tbCategory.Id);

        //        if (category_edit != null)
        //        {
        //            _db.Entry(category_edit).State = EntityState.Detached; // Detach the existing entity
        //            tbCategory.CreatedDate = category_edit.CreatedDate;
        //            tbCategory.CreatedBy = category_edit.CreatedBy;
        //            tbCategory.ModifierBy = category_edit.ModifierBy;
        //            tbCategory.ModifierDate = DateTime.Now;
        //            tbCategory.Alias = WebBanHangOnline.Models.Common.Filter.FilterChar(tbCategory.Title);
        //            _db.TbCategories.Update(tbCategory);
        //            _db.SaveChanges();
        //            return RedirectToAction("Index");
        //        }
        //    }
        //    return View();
        //}

        //public IActionResult Delete(int? id)
        //{
        //    if (id == null || id == 0)
        //    {
        //        return NotFound();
        //    }

        //    TbCategory? tbCategory = _db.TbCategories.Find(id);
        //    if (tbCategory == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(tbCategory);
        //}

        //[HttpPost, ActionName("Delete")]
        //public IActionResult DeleteCategory(Guid? id)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var item = _db.TbCategories.FirstOrDefault(x => x.Id == id);
        //        if (item != null)
        //        {
        //            _db.TbCategories.Remove(item);
        //            _db.SaveChanges();
        //        }
        //    }
        //    return RedirectToAction("Index");
        //}

        //[Area("Admin")]
        //[Route("test-jquery")]
        //[HttpPost]
        //public async Task<IActionResult> DeleteCategory_jquery(Guid? id)
        //{
        //    var item = _db.TbCategories.FirstOrDefault(x => x.Id == id);
        //    if (item != null)
        //    {
        //        _db.TbCategories.Remove(item);
        //        _db.SaveChanges();
        //    }
        //    return Ok();
        //}

        //[Area("Admin")]
        //[Route("editcategoryajax")]
        //public async Task<IActionResult> editCategory_ajax(Guid? id)
        //{
        //    var item = _db.TbCategories.FirstOrDefault(x => x.Id == id);
        //    return Ok(item);
        //}

        //[Area("Admin")]
        //[Route("editcategoryajax")]
        //[HttpPost]
        //public async Task<IActionResult> editCategory_ajax(TbCategory? tbCategory)
        //{
        //    TbCategory? category_edit = _db.TbCategories.Find(tbCategory.Id);

        //    if (category_edit != null)
        //    {
        //        _db.Entry(category_edit).State = EntityState.Detached; // Detach the existing entity
        //        tbCategory.CreatedDate = category_edit.CreatedDate;
        //        tbCategory.CreatedBy = category_edit.CreatedBy;
        //        tbCategory.ModifierBy = category_edit.ModifierBy;
        //        tbCategory.ModifierDate = DateTime.Now;
        //        tbCategory.Alias = WebBanHangOnline.Models.Common.Filter.FilterChar(tbCategory.Title);
        //        _db.TbCategories.Update(tbCategory);
        //        _db.SaveChanges();
        //    }
        //    return Ok(tbCategory);
        //}

        
    }
}
