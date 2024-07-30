using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Text;
using WebBanHangOnline.Models;
using WebBanHangOnline.Models.DbContext;
using WebBanHangOnline.Models.Entity;
using WebBanHangOnline.Models.ViewModels;
using X.PagedList;

namespace WebBanHangOnline.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Route("order")]
    [Authorize(Roles=$"{TbStaticField.Role_Admin}")]
    public class OrderController : Controller
    {
        private readonly WebBanHangOnlineContext _db;
        [BindProperty]
        public OrderVM OrderVM{ get; set; }
        public OrderController(WebBanHangOnlineContext db)
        {
            _db = db;
        }

        //[Route("")]
        public async Task<IActionResult>Index(int? page)
        {
            var items = await _db.OrderHeaders.Include("ApplicationUser").AsNoTracking().ToListAsync();
            var pageSize = 2;

            int pageNumber = page == null || page < 1 ? 1 : page.Value;
            PagedList<OrderHeader> lst = new PagedList<OrderHeader>(items, pageNumber, pageSize);

            return View(lst);
        }
        
        [Route("/order/getall")]
        public async Task<IActionResult> GetAll ()
        {
            var items = await _db.OrderHeaders.Include("ApplicationUser").ToListAsync(); 
            return Ok(items);
        }

        [Route("/order/detail")]
        public async Task<IActionResult> Detail(Guid id)
            {
            OrderVM = new OrderVM
            {
                orderHeader = await _db.OrderHeaders.Where(x => x.Id == id).Include("ApplicationUser").FirstOrDefaultAsync(),
            };
            OrderVM.orderDetailList = await _db.OrderDetails.Include(x=>x.Product).Where(x=> x.OrderHeaderId == OrderVM.orderHeader.Id).ToListAsync();

            return View(OrderVM);
        }

        [HttpPost]
        [Route("/order/updatedetail")]
        public async Task<IActionResult> UpdateDetail(Guid id)
        {
            var orderHeaderFromDb = await _db.OrderHeaders.FirstOrDefaultAsync(x => x.Id == id);
            if (orderHeaderFromDb != null)
            {
                orderHeaderFromDb.Name = OrderVM.orderHeader.Name;
                orderHeaderFromDb.PhoneNumber = OrderVM.orderHeader.PhoneNumber;
                orderHeaderFromDb.StreetAddress = OrderVM.orderHeader.StreetAddress;
                orderHeaderFromDb.District = OrderVM.orderHeader.District;
                orderHeaderFromDb.City = OrderVM.orderHeader.City;
                if (!string.IsNullOrEmpty(OrderVM.orderHeader.Carrier))
                {
                    orderHeaderFromDb.Carrier = OrderVM.orderHeader.Carrier;
                }
                if (!string.IsNullOrEmpty(OrderVM.orderHeader.TrackingNumber))
                {
                    orderHeaderFromDb.TrackingNumber = OrderVM.orderHeader.TrackingNumber;
                }

                await _db.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Detail),new {id = orderHeaderFromDb.Id});
        }

        [HttpPost]
        [Route("/order/startprocessing")]
        public async Task<IActionResult> StartProcessing(Guid id)
        {
            // cập nhật trạng thái đơn hàng
            var item = await _db.OrderHeaders.FindAsync(id);
            if (item != null)
            {
                item.OrderStatus = TbStaticField.StatusInProcess;
                await _db.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Detail), new { id});
        }

        [HttpPost]
        [Route("/order/shiporder")]
        public async Task<IActionResult> ShipOrder(Guid id)
        {
            // cập nhật trạng thái đơn hàng
            var orderHeader = await _db.OrderHeaders.FindAsync(id);
            if (orderHeader != null)
            {
                orderHeader.Carrier = OrderVM.orderHeader.Carrier;
                orderHeader.TrackingNumber = OrderVM.orderHeader.TrackingNumber;
                orderHeader.OrderStatus = TbStaticField.StatusShipped;
                orderHeader.ShippingDate = DateTime.Now;
                await _db.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Detail), new { id });
        }

        [HttpPost]
        [Route("/order/cancelorder")]
        public async Task<IActionResult> CancelOrder(Guid id)
        {
            // refund tiền cho khách nếu có thanh toán
            // cập nhật trạng thái đơn hàng
            var orderHeader = await _db.OrderHeaders.FindAsync(id);
            if (orderHeader != null)
            {
                orderHeader.OrderStatus = TbStaticField.StatusCancelled;
                await _db.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Detail), new { id });
        }

        [Route("/order/search")]
        public async Task<IActionResult> Search(string? searchStr)
        {
            var items = await _db.OrderHeaders.Include("ApplicationUser")
                .ToListAsync();
            if (!string.IsNullOrEmpty(searchStr))
            {
                string searchTerm = RemoveDiacritics(searchStr);

                var result = items.Where(x =>
                    RemoveDiacritics(x.Name)
                        .ToUpper().Contains(searchTerm.ToUpper()) ||
                    RemoveDiacritics(x.Id.ToString())
                        .ToUpper().Contains(searchTerm.ToUpper())||
                    RemoveDiacritics(x.PhoneNumber)
                        .ToUpper().Contains(searchTerm.ToUpper())||
                    RemoveDiacritics(x.ApplicationUser.Email)
                        .ToUpper().Contains(searchTerm.ToUpper())||
                    RemoveDiacritics(x.OrderStatus)
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
