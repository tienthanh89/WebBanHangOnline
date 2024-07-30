//using AutoMapper;
//using Microsoft.AspNetCore.Mvc;
//using WebBanHangOnline.Helper;
//using WebBanHangOnline.Models;
//using WebBanHangOnline.Models.Entity;
//using WebBanHangOnline.Models.ViewModels.User;

//namespace WebBanHangOnline.Controllers
//{
//    public class CustomerController : Controller
//    {
//        private readonly WebBanHangOnlineContext _db;
//        private readonly IMapper _mapper;

//        public CustomerController(WebBanHangOnlineContext db, IMapper mapper)
//        {
//            _db = db;
//            _mapper= mapper;
//        }

//        #region Register
//        public IActionResult Register()
//        {
//            return View();
//        }

//        [HttpPost]
//        public IActionResult Register(RegisterVM register, IFormFile file)
//        {
//            if(ModelState.IsValid)
//            {
//                var customer = _mapper.Map<TbCustomer>(register);
//                customer.RandomKey = MyUtil.GenerateRandoomKey();
//                customer.Password = register.Password.ToMd5Hash(customer.RandomKey);
//                customer.IsActive = true; // sẽ xử lý khi active mail
//                customer.VaiTro = 0;
//                if (file != null)
//                {
//                    customer.ImageUrl = FilesManagement.uploadImage(file);
//                }
//                _db.Add(customer);
//                _db.SaveChanges();
//                return RedirectToAction("Index", "shop");
//            }
//            return View();
//        }
//        #endregion

//        #region Login
//        public IActionResult login()
//        {
//            return View();
//        }
//        #endregion

//    }
//}
