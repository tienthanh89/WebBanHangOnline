using Microsoft.AspNetCore.Mvc;
using WebBanHangOnline.Models;
using WebBanHangOnline.Models.ViewModels;
using WebBanHangOnline.Helper;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using WebBanHangOnline.Models.ViewModels.User;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using WebBanHangOnline.Models.Entity;
using NuGet.Protocol;
using System.Collections.Concurrent;
using Stripe.Checkout;
using WebBanHangOnline.Models.DbContext;

namespace WebBanHangOnline.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        WebBanHangOnlineContext _db;
        [BindProperty]
        public ShoppingCartVM ShoppingCartVM { get; set; }
        public CartController(WebBanHangOnlineContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoadData()
        {
            // Claim user Id
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            ShoppingCartVM = new()
            {
                ShoppingCartList = await _db.ShoppingCarts.Where(x => x.ApplicationUserId == userId).Include("Product").ToListAsync(),
                OrderHeader = new()
            };

            foreach (var i in ShoppingCartVM.ShoppingCartList)
            {
                ShoppingCartVM.CartTotal += i.Quantity;
                ShoppingCartVM.OrderHeader.OrderTotal += i.Product.Price * i.Quantity;
            }
            return Ok(ShoppingCartVM);
        }

        // Hàm giảm số lượng sản phẩm khi ấn nút -
        [HttpPut]
        public async Task<IActionResult> Minus(Guid id)
        {
            var item = await _db.ShoppingCarts.Where(X => X.Id == id).FirstOrDefaultAsync();

            if (item == null)
            {
                return BadRequest();
            }

            --item.Quantity;
            if (item.Quantity < 0)
            {
                item.Quantity = 0;
            }

            _db.ShoppingCarts.Update(item);
            await _db.SaveChangesAsync();

            return Ok();
        }

        // Hàm tăng số lượng sản phẩm khi ấn +
        [HttpPut]
        public async Task<IActionResult> Plus(Guid id)
        {
            var item = await _db.ShoppingCarts.Where(X => X.Id == id).FirstOrDefaultAsync();
            if (item == null) { return BadRequest(); }
            ++item.Quantity;
            _db.ShoppingCarts.Update(item);
            await _db.SaveChangesAsync();
            return Ok();
        }

        // Hàm thay đổi số lượng sản phẩm trong cart
        [HttpPut]
        public async Task<IActionResult> ChangeQuantity(Guid id, int quantity)
        {
            var item = await _db.ShoppingCarts.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (item == null) { return NotFound(); }
            item.Quantity = quantity;
            _db.ShoppingCarts.Update(item);
            await _db.SaveChangesAsync();
            return Ok();
        }

        // Hàm xóa sản phẩm trong cart
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            var item = await _db.ShoppingCarts.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (item == null) { return NotFound(); }
            _db.ShoppingCarts.Remove(item);
            await _db.SaveChangesAsync();
            return Ok();
        }

        // Hàm hiển thị xác nhận đơn hàng
        public async Task<IActionResult> Summary()
        {
            // Claim user Id
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            ShoppingCartVM = new()
            {
                ShoppingCartList = await _db.ShoppingCarts.Where(x => x.ApplicationUserId == userId).Include("Product").ToListAsync(),
                OrderHeader = new()
            };

            ShoppingCartVM.OrderHeader.OrderDate = DateTime.Now;
            ShoppingCartVM.OrderHeader.ApplicationUserId = userId;

            ShoppingCartVM.OrderHeader.ApplicationUser = await _db.ApplicationUsers.Where(x => x.Id == userId).FirstOrDefaultAsync();
            ShoppingCartVM.OrderHeader.Name = ShoppingCartVM.OrderHeader.ApplicationUser.Name;
            ShoppingCartVM.OrderHeader.City = ShoppingCartVM.OrderHeader.ApplicationUser.City;
            ShoppingCartVM.OrderHeader.StreetAddress = ShoppingCartVM.OrderHeader.ApplicationUser.StreetAddress;
            ShoppingCartVM.OrderHeader.PhoneNumber = ShoppingCartVM.OrderHeader.ApplicationUser.PhoneNumber;
            ShoppingCartVM.OrderHeader.District = ShoppingCartVM.OrderHeader.ApplicationUser.District;

            foreach (var i in ShoppingCartVM.ShoppingCartList)
            {
                ShoppingCartVM.CartTotal += i.Quantity;
                ShoppingCartVM.OrderHeader.OrderTotal += i.Product.Price * i.Quantity;
            }

            return Ok(ShoppingCartVM);
        }


        [HttpPost]
        public async Task<IActionResult> SummaryPOST(OrderHeader orderHeader)
        {
            // Claim user Id
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            ShoppingCartVM = new()
            {
                ShoppingCartList = await _db.ShoppingCarts.Where(x => x.ApplicationUserId == userId).Include("Product").ToListAsync(),
                OrderHeader = new()
            };

            ShoppingCartVM.OrderHeader.ApplicationUser = await _db.ApplicationUsers.Where(x => x.Id == userId).FirstOrDefaultAsync();
            ShoppingCartVM.OrderHeader.Name = orderHeader.Name;
            ShoppingCartVM.OrderHeader.City = orderHeader.City;
            ShoppingCartVM.OrderHeader.StreetAddress = orderHeader.StreetAddress;
            ShoppingCartVM.OrderHeader.PhoneNumber = orderHeader.PhoneNumber;
            ShoppingCartVM.OrderHeader.District = orderHeader.District;

            foreach (var i in ShoppingCartVM.ShoppingCartList)
            {
                ShoppingCartVM.CartTotal += i.Quantity;
                ShoppingCartVM.OrderHeader.OrderTotal += i.Product.Price * i.Quantity;
            }

            ShoppingCartVM.OrderHeader.PaymentStatus = TbStaticField.PaymentStatusPending;
            ShoppingCartVM.OrderHeader.OrderStatus = TbStaticField.StatusPending;

            _db.OrderHeaders.Add(ShoppingCartVM.OrderHeader);
            await _db.SaveChangesAsync();

            foreach (var cart in ShoppingCartVM.ShoppingCartList)
            {
                OrderDetail orderDetail = new()
                {
                    ProductId = cart.ProductId,
                    OrderHeaderId = ShoppingCartVM.OrderHeader.Id,
                    Price = cart.Product.Price,
                    Count = cart.Quantity,
                };
                _db.OrderDetails.Add(orderDetail);
                _db.ShoppingCarts.Remove(cart);
                await _db.SaveChangesAsync();
            }

            

            return Ok(ShoppingCartVM.OrderHeader.Id);
        }

        public IActionResult OrderConfirmation(Guid id)
        {
            return View(id);
        }
    }
}
