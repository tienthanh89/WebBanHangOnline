using System.Globalization;
using System.Security.Cryptography.Xml;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebBanHangOnline.Helper;
using WebBanHangOnline.Models;
using WebBanHangOnline.Models.DbContext;
using WebBanHangOnline.Models.Entity;
using WebBanHangOnline.Models.ViewModels;
using X.PagedList;
using static System.Text.RegularExpressions.Regex;

namespace WebBanHangOnline.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = $"{TbStaticField.Role_Admin}")]
    public class ProductController : Controller
    {
        private readonly WebBanHangOnlineContext _db;

        // Tạo môi trường luu trữu web
        private readonly IWebHostEnvironment _webHostEnvironment;


        public ProductController(WebBanHangOnlineContext db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index(int? page, string searchStr = "")
        {
            var items = _db.TbProducts.Include(x => x.tbProductCategory).ToList();
            //if (searchStr != "")
            //{
            //    items = _db.TbProducts.Where(x => x.Title.ToLower().Contains(searchStr.ToLower())).AsNoTracking();
            //}

            //var pageSize = 2;

            //int pageNumber = page == null || page < 1 ? 1 : page.Value;
            //PagedList<TbProduct> lst = new PagedList<TbProduct>(items, pageNumber, pageSize);

            return View(items);
        }


        public IActionResult Add_MVC()
        {
            var items = new TbProductVM()
            {
                TbProduct = new TbProduct(),
                CategoryList = _db.TbProductCategories.Select(x => new SelectListItem
                { Text = x.Title, Value = x.Id.ToString() }),
            };
            
            return View(items);
        }

        [HttpPost]
        public IActionResult Add_MVC(TbProductVM TbProductVM, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                TbProductVM.CategoryList = _db.TbProductCategories.Select(u => new SelectListItem
                { Text = u.Title, Value = u.Id.ToString() });

                TbProductVM.TbProduct.ImageUrl = FilesManagement.uploadImage(file);

                _db.Add(TbProductVM.TbProduct);
                _db.SaveChanges();
                //TempData["success"] = "Thêm sản phẩm thành công";
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Add_MVC));
        }


        public IActionResult Edit_MVC(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var items = new TbProductVM()
            {
                TbProduct = new TbProduct(),
                CategoryList = _db.TbProductCategories.Select(x => new SelectListItem
                { Text = x.Title, Value = x.Id.ToString() }),
            };
            items.TbProduct = _db.TbProducts.Find(id);
            if (items.TbProduct == null)
            {
                return NotFound();
            }

            return View(items);
        }

        // kiểm tra thông tim đầu vào
        // tìm tbnews cũ trong db thong qua id
        // tách biệt tbnew cũ ra khỏi entity
        // gán các thông tin cũ cho tbnews mới
        // cập nhật thông tin mới như alias, modifierdate,
        // update, save
        [HttpPost]
        public ActionResult Edit_MVC(TbProductVM? TbProductVM, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                if (TbProductVM == null)
                {
                    return NotFound();
                }

                var tbProduct_Old = _db.TbProducts.Find(TbProductVM.TbProduct.Id);
                if (tbProduct_Old != null)
                {
                    _db.Entry(tbProduct_Old).State = EntityState.Detached;

                    if (file == null)
                    {
                        TbProductVM.TbProduct.ImageUrl = tbProduct_Old.ImageUrl;
                    }
                    else
                    {
                        // Tạo đường dẫn tới thư mục wwroot
                        // Chuẩn hóa lại tên tệp thành duy nhất
                        // Tạo tên duy nhất cho ảnh, gắn phần mở rộng của file tải lên vào tên ảnh
                        // Tạo đường dẫn mới cho file ảnh về wwwroot
                        // Tạo FileStream mới tới vị trí muốn luu file
                        // Copy file tải lên vào fileStream
                        // Tạo địa chỉ hoàn chỉnh cho file
                        string wwwRootPath = _webHostEnvironment.WebRootPath;
                        string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                        string filePath = Path.Combine(wwwRootPath, @"images\admin\news");
                        using (var fileStream = new FileStream(Path.Combine(filePath, fileName), FileMode.Create))
                        {
                            file.CopyTo(fileStream);
                        }

                        TbProductVM.TbProduct.ImageUrl = @"\images\admin\news\" + fileName;
                    }

                    _db.Update(TbProductVM.TbProduct);
                    _db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile_Ajax(IFormFile file)
        {
            // Tạo đường dẫn tới wwwroot
            // Chuẩn hóa lại tên tệp thành duy nhất
            // Tạo đường dẫn mới cho file ảnh về wwwroot
            // Tạo tên duy nhất cho ảnh, gắn phần mở rộng của file tải lên vào tên ảnh
            string wwwRootPath = _webHostEnvironment.WebRootPath;
            if (file != null)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                string filePath = Path.Combine(wwwRootPath, @"images\admin\product");
                // Tạo FileStream mới tới vị trí muốn luu file
                // Copy file tải lên vào
                using (var fileStream = new FileStream(Path.Combine(filePath, fileName), FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }

                //TbProductVM.TbProduct.ImageUrl = @"\images\admin\product\" + fileName;
            }

            return Ok(file);
        }

        [HttpDelete]
        [Route("/product/delete")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var item = await _db.TbProducts.FindAsync(id);
            if (item != null)
            {
                _db.TbProducts.Remove(item);
                await _db.SaveChangesAsync();
            }

            //TempData["success"] = "Xóa sản phẩm thành công";
            return RedirectToAction(nameof(Index));
        }

        [HttpDelete]
        [Route("/product/deleterange")]
        public async Task<IActionResult> DeleteRange(IEnumerable<Guid> listId)
        {
            var products = await _db.TbProducts.Where(x => listId.Contains(x.Id)).ToListAsync();
            _db.TbProducts.RemoveRange(products);
            await _db.SaveChangesAsync();
            //TempData["success"] = "Xóa sản phẩm thành công";
            return Ok();
        }

        [HttpPost]
        //[Route("/product/changeStatus")]
        public async Task<IActionResult> changeStatus(Guid? id)
        {
            var item = await _db.TbProducts.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            else
            {
                item.IsActive = !item.IsActive;
            }

            _db.TbProducts.Update(item);
            await _db.SaveChangesAsync();
            return Ok(item);
        }

        [Route("/product/search")]
        public async Task<IActionResult> Search(string? searchStr)
        {
            var items = await _db.TbProducts
                .Include(x => x.tbProductCategory).ToListAsync();
            if (!string.IsNullOrEmpty(searchStr))
            {
                string searchTerm = RemoveDiacritics(searchStr);

                

                var result = items.Where(x =>
                    RemoveDiacritics(x.Title)
                        .ToUpper().Contains(searchTerm.ToUpper()) ||
                    RemoveDiacritics(x.tbProductCategory.Title)
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

        [Route("/product/sortbycategory")]
        public IActionResult SortByCategory(int? page, string searchStr = "")
        {
            var items = _db.TbProducts
                .Include(x => x.tbProductCategory)
                .OrderBy(x=>x.tbProductCategory.Title)
                .AsNoTracking();
            if (searchStr != "")
            {
                items = _db.TbProducts.Where(x => x.Title.ToLower().Contains(searchStr.ToLower())).AsNoTracking();
            }

            var pageSize = 7;

            int pageNumber = page == null || page < 1 ? 1 : page.Value;
            PagedList<TbProduct> lst = new PagedList<TbProduct>(items, pageNumber, pageSize);

            return View(lst);
        }
    }
}
