using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebBanHangOnline.Models;
using WebBanHangOnline.Models.ViewModels;
using X.PagedList;

namespace WebBanHangOnline.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly WebBanHangDemoContext _db;
        // Tạo môi trường luu trữu web
        private readonly IWebHostEnvironment _webHostEnvironment;


        public ProductController(WebBanHangDemoContext db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index(int? page, string searchStr = "")
        {
            var items = _db.TbProducts.Include(x=>x.tbProductCategory).AsNoTracking();
            if(searchStr != "")
            {
                items = _db.TbProducts.Where(x=>x.Title.ToLower().Contains(searchStr.ToLower())).AsNoTracking();
            }

            var pageSize = 7;

            int pageNumber = page == null || page < 1 ? 1 : page.Value;
            PagedList<TbProduct> lst = new PagedList<TbProduct>(items, pageNumber, pageSize);

            return View(lst);
        }

        
        public IActionResult Add_MVC() {
            var items = new TbProductVM()
            {
                tbProduct = new TbProduct() ,
                CategoryList = _db.TbProductCategories.Select(x=>new SelectListItem { Text = x.Title, Value = x.Id.ToString() }),
            };
            return View(items); 
        }

        [HttpPost]
        public IActionResult Add_MVC(TbProductVM tbProductVM, IFormFile? file)
        {
            tbProductVM.tbProduct.CreatedDate = DateTime.Now;
            tbProductVM.tbProduct.Alias = WebBanHangOnline.Models.Common.Filter.FilterChar(tbProductVM.tbProduct.Title);
            tbProductVM.CategoryList = _db.TbProductCategories.Select(u => new SelectListItem { Text = u.Title, Value = u.Id.ToString() });


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

                tbProductVM.tbProduct.ImageUrl = @"\images\admin\product\" + fileName;
            }
            _db.Add(tbProductVM.tbProduct);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Edit_MVC()
        {
            var items = new TbProductVM()
            {
                tbProduct = new TbProduct(),
                CategoryList = _db.TbProductCategories.Select(x => new SelectListItem { Text = x.Title, Value = x.Id.ToString() }),
            };
            return View(items);
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

                //tbProductVM.tbProduct.ImageUrl = @"\images\admin\product\" + fileName;
            }

            return Ok(file);
        }
        
    }
}
