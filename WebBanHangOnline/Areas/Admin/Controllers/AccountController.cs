using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.Globalization;
using System.Runtime.Intrinsics.X86;
using System.Text;
using WebBanHangOnline.Models;
using WebBanHangOnline.Models.DbContext;
using WebBanHangOnline.Models.Entity;
using WebBanHangOnline.Models.ViewModels.Account;

namespace WebBanHangOnline.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("account")]
    [Authorize(Roles = $"{TbStaticField.Role_Admin}")]
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUserStore<IdentityUser> _userStore;
        private readonly WebBanHangOnlineContext _db;

        public AccountController(UserManager<IdentityUser> userManager, WebBanHangOnlineContext db, IUserStore<IdentityUser> userStore)
        {
            _db = db;
            _userManager = userManager;
            _userStore = userStore;
        }
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("all")]
        public async Task<IActionResult> GetAllAccount()
        {
            var items = await _db.ApplicationUsers.Select(x => new AccountVM
            {
                id = x.Id,
                userName = x.UserName,
                email = x.Email,
                emailConfirmed = x.EmailConfirmed,
                phoneNumber = x.PhoneNumber,
                isActive = x.IsActive,
                lockoutEnd = x.LockoutEnd,
            }).ToListAsync();

            return Ok(items);
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateAccount(ApplicationUser appUser)
        {
            if (appUser.Id == null)
            {
                // Create account
                var user = CreateUser();
                user.UserName = appUser.Email;
                user.Email = appUser.Email;
                user.EmailConfirmed = true;
                user.City = appUser.City;
                user.District = appUser.District;
                user.StreetAddress = appUser.StreetAddress;
                //user.IsActive = appUser.IsActive;
                if (!appUser.IsActive)
                {
                    user.LockoutEnd = new DateTimeOffset(9999, 12, 31, 23, 59, 59, TimeSpan.Zero);
                }
                user.Description = appUser.Description;
                user.PhoneNumber = appUser.PhoneNumber;
                user.Name = appUser.Name;
                user.NormalizedEmail = user.Email.ToUpper();
                user.NormalizedUserName = user.Email.ToUpper();

                await _userStore.SetUserNameAsync(user, appUser.Email, CancellationToken.None);
                var result = await _userManager.CreateAsync(user, appUser.PasswordHash);

                await _userManager.GetUserAsync(User);

                if (result.Succeeded)
                {
                    //TempData["success"] = "Thêm tài khoản thành công";
                    return Ok(user);
                }
                //TempData["error"] = "Thêm tài khoản thất bại";
                return BadRequest(result.Errors);
            }
            else
            {
                var user = await _db.ApplicationUsers.Where(x => x.Id == appUser.Id).FirstOrDefaultAsync();
                if (user == null)
                {
                    return NotFound();
                }
                user.UserName = appUser.Email;
                user.Email = appUser.Email;
                user.EmailConfirmed = true;
                user.City = appUser.City;
                user.District = appUser.District;
                user.StreetAddress = appUser.StreetAddress;
                user.IsActive = appUser.IsActive;
                user.Description = appUser.Description;
                user.PhoneNumber = appUser.PhoneNumber;
                user.Name = appUser.Name;
                user.NormalizedEmail = user.Email.ToUpper();
                user.NormalizedUserName = user.Email.ToUpper();
                await _userStore.SetUserNameAsync(user, appUser.Email, CancellationToken.None);

                if (appUser.PasswordHash != null)
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var result = await _userManager.ResetPasswordAsync(user, token, appUser.PasswordHash);
                    await _userManager.GetUserAsync(User);

                    if (result.Succeeded)
                    {
                        return Ok(user);
                    }
                    return BadRequest(result.Errors);
                }

                _db.ApplicationUsers.Update(user);
                _db.SaveChanges();
                return Ok(user);
            }
        }

        private ApplicationUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<ApplicationUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(ApplicationUser)}'. " +
                    $"Ensure that '{nameof(ApplicationUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }

        [Route("getById/{id}")]
        public async Task<IActionResult> getById(string id)
        {
            var item = await _db.ApplicationUsers.Where(x => x.Id == id).FirstOrDefaultAsync();
            return Ok(item);
        }

        [HttpPost]
        [Route("update")]
        public async Task<IActionResult> UpdateUser(string id)
        {

            return Ok();
        }

        [HttpPost]
        [Route("delete")]
        public async Task<IActionResult> DeleteAccount(string id)
        {
            var item = await _db.Users.Where(x => x.Id == id).FirstOrDefaultAsync();
            _db.Users.Remove(item);
            _db.SaveChanges();

            return Ok();
        }

        [Route("isActive")]
        public async Task<IActionResult> isActive(string id)
        {
            var item = _db.ApplicationUsers.FirstOrDefault(x => x.Id == id);
            if (item == null)
            {
                return NotFound();
            }

            if (item.LockoutEnd == null)
            {
                item.LockoutEnd = new DateTimeOffset(9999, 12, 31, 23, 59, 59, TimeSpan.Zero);
            }
            else
            {
                item.LockoutEnd = null;
            }
            _db.Update(item);
            await _db.SaveChangesAsync();
            return Ok(item.LockoutEnd);
        }

        [Route("emailConfirm")]
        public async Task<IActionResult> emailConfirm(string id)
        {
            var item = _db.ApplicationUsers.Where(x => x.Id == id).FirstOrDefault();
            if (item == null)
            {
                return NotFound();
            }
            item.EmailConfirmed = !item.EmailConfirmed;
            _db.Update(item);
            _db.SaveChanges();
            return Ok(item.EmailConfirmed);
        }

        [Route("/account/search")]
        public async Task<IActionResult> Search(string? searchStr)
        {
            var items = await _db.ApplicationUsers
                .ToListAsync();
            if (!string.IsNullOrEmpty(searchStr) && items.Count > 0)
            {
                string searchTerm = RemoveDiacritics(searchStr);

                var result = items.Where(x => 
                        RemoveDiacritics(x.UserName)
                          .ToUpper().Contains(searchTerm.ToUpper()) ||
                        RemoveDiacritics(x.PhoneNumber)
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
