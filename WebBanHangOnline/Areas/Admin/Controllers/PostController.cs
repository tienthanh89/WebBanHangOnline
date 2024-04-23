﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebBanHangOnline.Models.ViewModels;
using WebBanHangOnline.Models;
using X.PagedList;

namespace WebBanHangOnline.Areas.Admin.Controllers
{

    [Area("Admin")]
    public class PostController : Controller
    {
        private readonly WebBanHangDemoContext _db;
        // Tạo môi trường luu trữu web
        private readonly IWebHostEnvironment _webHostEnvironment;


        public PostController(WebBanHangDemoContext db, IWebHostEnvironment webHostEnvironment)
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

        public IActionResult Index(int? page)
        {
            var controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            ViewBag.ControllerName = controllerName;

            var pageSize = 2;
            int pageNumber = page == null || page < 1 ? 1 : page.Value;
            
            var items = _db.TbPosts.Select(n => new TbCategoryTbPostVM
            {
                Post = n,
                Category = n.tbCategory,
            }).AsNoTracking().OrderByDescending(x => x.Post.Id);
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
        public IActionResult Add_MVC(TbPostVM tbPostVM, IFormFile? file, int? id)
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

                    tbPostVM.TbPost.ImageUrl = @"\images\admin\news\" + fileName;
                }
                tbPostVM.TbPost.CreatedDate = DateTime.Now;
                tbPostVM.TbPost.ModifierDate = DateTime.Now;
                if (tbPostVM.TbPost.Title != null)
                {
                    tbPostVM.TbPost.Alias = WebBanHangOnline.Models.Common.Filter.FilterChar(tbPostVM.TbPost.Title);
                }

                //tbPost.tbCategory = _db.TbPost.Include(n =>n.tbCategory).FirstOrDefault(n=>n.Id == id);

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

        public ActionResult Edit_MVC(int? id)
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
                    tbPostVM.TbPost.CreatedDate = tbPost_Old.CreatedDate;
                    tbPostVM.TbPost.CreatedBy = tbPost_Old.CreatedBy;
                    tbPostVM.TbPost.ModifierDate = DateTime.Now;
                    if (tbPostVM.TbPost.Title != null)
                    {
                        tbPostVM.TbPost.Alias = WebBanHangOnline.Models.Common.Filter.FilterChar(tbPostVM.TbPost.Title);
                    }
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
        public IActionResult Delete(int? id)
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
        public IActionResult changeStatus(int id)
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
    }
}

