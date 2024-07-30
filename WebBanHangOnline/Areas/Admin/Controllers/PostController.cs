using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebBanHangOnline.Models.ViewModels;
using WebBanHangOnline.Models;
using WebBanHangOnline.Models.DbContext;
using X.PagedList;
using Microsoft.AspNetCore.Authorization;
using WebBanHangOnline.Models.Entity;

namespace WebBanHangOnline.Areas.Admin.Controllers
{

    [Area("Admin")]
    [Authorize(Roles = $"{TbStaticField.Role_Admin}")]
    public class PostController : Controller
    {
        private readonly WebBanHangOnlineContext _db;
        // Tạo môi trường luu trữu web
        private readonly IWebHostEnvironment _webHostEnvironment;


        public PostController(WebBanHangOnlineContext db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
        }
        //public IActionResult Index()
        //{
        //    //var items = _db.TbPost.Include(n => n.tbCategory).OrderByDescending(x => x.Id).ToList();
        //    //return View(items);

        //    DbSetTbPostVM dbSetTbPostVM = new DbSetTbPostVM()
        //    {
        //        dbSetTbPost = _db.TbPost,
        //        CategoryList = _db.TbCategories.Select(u => new SelectListItem { Text = u.Title, Value = u.Id.ToString() })
        //    };
        //    return View(dbSetTbPostVM);
        //}

        public IActionResult Index(int? page, string searchStr = "")
        {
            var items = _db.TbPosts.Select(n => new TbCategoryTbPostVM
            {
                Post = n,
                Category = n.tbCategory,
            }).AsNoTracking().OrderByDescending(x => x.Post.Id);

            if (searchStr != "")
            {
                items = _db.TbPosts.Select(n => new TbCategoryTbPostVM
                {
                    Post = n,
                    Category = n.tbCategory,
                }).AsNoTracking().Where(x => x.Post.Title.ToLower().Contains(searchStr.ToLower())).OrderByDescending(x => x.Post.Id);

            }

            var pageSize = 7;
            int pageNumber = page == null || page < 1 ? 1 : page.Value;
            PagedList<TbCategoryTbPostVM> lst = new PagedList<TbCategoryTbPostVM>(items, pageNumber, pageSize);

            return View(lst);
        }

        //public IActionResult Index()
        //{
        //    var items = _db.TbPost.Select(n => new TbCategoryTbPostVM
        //    {
        //        Post = n,
        //        CategoryList = _db.TbCategories.Select(x =>)
        //    }).OrderByDescending(x => x.Post.Id);
        //    return View(items);
        //}

        //public IActionResult Index()
        //{
        //    var items = _db.TbPost.Include(n => n.tbCategory).OrderByDescending(x => x.Id).ToList();
        //    return View(items);
        //}

        public IActionResult Add_MVC()
        {
            TbPostVM tbPostVM = new TbPostVM()
            {
                TbPost = new TbPost(),
                CategoryList = _db.TbCategories.Select(u => new SelectListItem { Text = u.Title, Value = u.Id.ToString() })
            };
            return View(tbPostVM);
        }

        [HttpPost]
        public IActionResult Add_MVC(TbPostVM? tbPostVM, IFormFile? file)
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
                    string filePath = Path.Combine(wwwRootPath, @"images\admin\post");
                    // Tạo FileStream mới tới vị trí muốn luu file
                    // Copy file tải lên vào
                    using (var fileStream = new FileStream(Path.Combine(filePath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    tbPostVM.TbPost.ImageUrl = @"\images\admin\post\" + fileName;
                }
                tbPostVM.TbPost.Id = Guid.NewGuid();

                //tbPost.tbCategory = TbProductVM.TbPost.Include(n =>n.tbCategory).FirstOrDefault(n=>n.Id == id);

                _db.TbPosts.Add(tbPostVM.TbPost);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                tbPostVM.CategoryList = _db.TbCategories.Select(u => new SelectListItem { Text = u.Title, Value = u.Id.ToString() });
            }
            return View(tbPostVM);
        }

        public ActionResult Edit_MVC(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TbPostVM tbPostVM = new TbPostVM()
            {
                TbPost = new TbPost(),
                CategoryList = _db.TbCategories.Select(u => new SelectListItem { Text = u.Title, Value = u.Id.ToString() })
            };
            tbPostVM.TbPost = _db.TbPosts.Find(id);
            if (tbPostVM.TbPost == null)
            {
                return NotFound();
            }
            return View(tbPostVM);
        }


        // kiểm tra thông tim đầu vào
        // tìm tbnews cũ trong db thong qua id
        // tách biệt tbnew cũ ra khỏi entity
        // gán các thông tin cũ cho tbnews mới
        // cập nhật thông tin mới như alias, modifierdate,
        // update, save
        [HttpPost]
        public ActionResult Edit_MVC(TbPostVM? tbPostVM, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                if (tbPostVM == null)
                {
                    return NotFound();
                }
                var tbPost_Old = _db.TbPosts.Find(tbPostVM.TbPost.Id);
                if (tbPost_Old != null)
                {
                    _db.Entry(tbPost_Old).State = EntityState.Detached;
                    if (file == null)
                    {
                        tbPostVM.TbPost.ImageUrl = tbPost_Old.ImageUrl;
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

                        tbPostVM.TbPost.ImageUrl = @"\images\admin\news\" + fileName;
                    }

                    _db.Update(tbPostVM.TbPost);
                    _db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View();
        }

        [HttpPost]
        public IActionResult Delete(Guid? id)
        {
            var item = _db.TbPosts.FirstOrDefault(x => x.Id == id);
            if (item != null)
            {
                _db.TbPosts.Remove(item);
                _db.SaveChanges();
            }

            return Ok();
        }

        [HttpPost]
        public IActionResult changeStatus(Guid id)
        {
            var item = _db.TbPosts.FirstOrDefault(x => x.Id == id);
            if (item != null)
            {
                item.IsActive = !item.IsActive;
                _db.Update(item);
                _db.SaveChanges();
            }
            return Ok(item);
        }

        #region API CALLS
        public IActionResult GetAll()
        {
            var items = _db.TbPosts.Select(n => new TbCategoryTbPostVM
            {
                Post = n,
                Category = n.tbCategory,
            }).AsNoTracking().OrderByDescending(x => x.Post.Id);

            return Json(new { data = items });
        }
        #endregion
    }
}

