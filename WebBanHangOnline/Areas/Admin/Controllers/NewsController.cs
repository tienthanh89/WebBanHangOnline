using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebBanHangOnline.Models;
using WebBanHangOnline.Models.ViewModels;

namespace WebBanHangOnline.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class NewsController : Controller
    {
        private readonly WebBanHangDemoContext _db;
        // Tạo môi trường luu trữu web
        private readonly IWebHostEnvironment _webHostEnvironment;


        public NewsController(WebBanHangDemoContext db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
        }


        //public IActionResult Index()
        //{
        //    //var items = _db.TbNews.Include(n => n.tbCategory).OrderByDescending(x => x.Id).ToList();
        //    //return View(items);
        //    var categories = _db.TbCategories;

        //    DbSetTbNewsVM dbSetTbNewsVM = new DbSetTbNewsVM()
        //    {
        //        dbSetTbNews = _db.TbNews,
                


        //    };
        //    var lst = _db.TbNews.Include(x => x.tbCategory).Include(x => x.tbCategory.Id);
        //    if (dbSetTbNewsVM.dbSetTbNews != null) {
        //        foreach(var item in dbSetTbNewsVM.dbSetTbNews)
        //        {
        //            dbSetTbNewsVM.CategoryList = dbSetTbNewsVM.dbSetTbNews.Select(n => item.ContainsKey(n.CategoryId) ? categories[n.CategoryId] : null)
        //        }
        //    }

        //    return View(dbSetTbNewsVM);
        //}

        
        public IActionResult Index()
        {
            var items = _db.TbNews.Select(n => new TbCategoryTbNewsVM
            {
                News = n,
                Category = n.tbCategory,
            }).OrderByDescending(x => x.News.Id);
            return View(items);
        }
        




        //public IActionResult Index()
        //{
        //    var items = _db.TbNews.Select(n => new TbCategoryTbNewsVM
        //    {
        //        News = n,
        //        CategoryList = _db.TbCategories.Select(x =>)
        //    }).OrderByDescending(x => x.News.Id);
        //    return View(items);
        //}

        //public IActionResult Index()
        //{
        //    var items = _db.TbNews.Include(n => n.tbCategory).OrderByDescending(x => x.Id).ToList();
        //    return View(items);
        //}

        public IActionResult Add_MVC()
        {
            TbNewsVM tbNewsVM = new TbNewsVM()
            {
                TbNews = new TbNews(),
                CategoryList = _db.TbCategories.Select(u => new SelectListItem { Text = u.Title, Value = u.Id.ToString() })
            };
            return View(tbNewsVM);
        }

        [HttpPost]
        public IActionResult Add_MVC(TbNewsVM tbNewsVM, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                // Tạo đường dẫn tới wwwroot
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                // Chuẩn hóa lại tên tệp thành duy nhất
                // Tạo đường dẫn mới cho file ảnh về wwwroot
                // Tạo tên duy nhất cho ảnh, gắn phần mở rộng của file tải lên vào tên ảnh
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string filePath = Path.Combine(wwwRootPath, @"images\admin\news");
                    // Tạo FileStream mới tới vị trí muốn luu file
                    // Copy file tải lên vào
                    using (var fileStream = new FileStream(Path.Combine(filePath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    tbNewsVM.TbNews.ImageUrl = @"\images\admin\news\" + fileName;
                }
                tbNewsVM.TbNews.CreatedDate = DateTime.Now;
                tbNewsVM.TbNews.ModifierDate = DateTime.Now;
                if (tbNewsVM.TbNews.Title != null)
                {
                    tbNewsVM.TbNews.Alias = WebBanHangOnline.Models.Common.Filter.FilterChar(tbNewsVM.TbNews.Title);
                }

                //tbNews.tbCategory = _db.TbNews.Include(n =>n.tbCategory).FirstOrDefault(n=>n.Id == id);

                _db.TbNews.Add(tbNewsVM.TbNews);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                tbNewsVM.CategoryList = _db.TbCategories.Select(u => new SelectListItem { Text = u.Title, Value = u.Id.ToString() });
            }
            return View(tbNewsVM);
        }

        public ActionResult Edit_MVC(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TbNewsVM tbNewsVM = new TbNewsVM()
            {
                TbNews = new TbNews(),
                CategoryList = _db.TbCategories.Select(u => new SelectListItem { Text = u.Title, Value = u.Id.ToString() })
            };
            tbNewsVM.TbNews = _db.TbNews.Find(id);
            if (tbNewsVM.TbNews == null)
            {
                return NotFound();
            }
            return View(tbNewsVM);
        }


        // kiểm tra thông tim đầu vào
        // tìm tbnews cũ trong db thong qua id
        // tách biệt tbnew cũ ra khỏi entity
        // gán các thông tin cũ cho tbnews mới
        // cập nhật thông tin mới như alias, modifierdate,
        // update, save
        [HttpPost]
        public ActionResult Edit_MVC(TbNewsVM? tbNewsVM, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                if (tbNewsVM == null)
                {
                    return NotFound();
                }
                var tbNews_Old = _db.TbNews.Find(tbNewsVM.TbNews.Id);
                if (tbNews_Old != null)
                {
                    _db.Entry(tbNews_Old).State = EntityState.Detached;
                    tbNewsVM.TbNews.CreatedDate = tbNews_Old.CreatedDate;
                    tbNewsVM.TbNews.CreatedBy = tbNews_Old.CreatedBy;
                    tbNewsVM.TbNews.ModifierDate = DateTime.Now;
                    if (tbNewsVM.TbNews.Title != null)
                    {
                        tbNewsVM.TbNews.Alias = WebBanHangOnline.Models.Common.Filter.FilterChar(tbNewsVM.TbNews.Title);
                    }
                    if (file == null)
                    {
                        tbNewsVM.TbNews.ImageUrl = tbNews_Old.ImageUrl;
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

                        tbNewsVM.TbNews.ImageUrl = @"\images\admin\news\" + fileName;
                    }

                    _db.Update(tbNewsVM.TbNews);
                    _db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View();
        }

        [HttpPost]
        public IActionResult Delete(int? id)
        {
            var item = _db.TbNews.FirstOrDefault(x => x.Id == id);
            if (item != null)
            {
                _db.TbNews.Remove(item);
                _db.SaveChanges();
            }

            return Ok();
        }

        [HttpPost]
        public  IActionResult changeStatus(int id) {
            var item = _db.TbNews.FirstOrDefault(x => x.Id == id);
            if(item != null)
            {
                item.IsActive = !item.IsActive;
                _db.Update(item);
                _db.SaveChanges();
            }
            return Ok(item);
        }
    }
}