using System.Runtime.InteropServices.JavaScript;
using System.Globalization;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebBanHangOnline.Models;
using WebBanHangOnline.Models.DbContext;
using WebBanHangOnline.Models.Entity;
using WebBanHangOnline.Models.ViewModels.Assignment.Roles;

namespace WebBanHangOnline.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("assignment")]
    [Authorize(Roles = $"{TbStaticField.Role_Admin}")]
    public class AssignmentController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly WebBanHangOnlineContext _db;

        public AssignmentController(WebBanHangOnlineContext db, RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            _roleManager = roleManager;
            _db = db;
            _userManager = userManager;
        }

        #region Roles
        [Route("roles")]
        public IActionResult Roles()
        {
            return View();
        }

        // Xay dung xu ly them role
        [HttpPost]
        [Route("roles/create")]
        public async Task<IActionResult> CreateRole(ApplicationRole role)
        {
            role.NormalizedName = role.Name?.ToUpper();
            await _db.ApplicationRoles.AddAsync(role);
            await _db.SaveChangesAsync();
            return Ok();
        }

        [Route("roles/all")]
        public async Task<IActionResult> GetAllRole()
        {
            var data = await _db.ApplicationRoles.ToListAsync();

            return Ok(data);
        }

        [Route("roles/getbyid")]
        public async Task<IActionResult> GetIdentityById(Guid id)
        {
            var item = await _db.ApplicationRoles.FindAsync(id.ToString());
            return Ok(item);
        }

        [HttpPut]
        [Route("/assignment/roles/edit")]
        public async Task<IActionResult> Edit(ApplicationRole role)
        {
            var item = await _db.ApplicationRoles.FindAsync(role.Id);

            if (item == null) return NotFound();
            item.Name = role.Name;
            item.Mota = role.Mota;
            item.IsActive = role.IsActive;

            await _db.SaveChangesAsync();
            return Ok();
        }

        [HttpPut]
        [Route("/assignment/roles/changeStatus")]
        public async Task<IActionResult> ChangeStatus(Guid id)
        {
            var item = await _db.ApplicationRoles.FindAsync(id.ToString());
            if (item == null) return NotFound();
            item.IsActive = !item.IsActive;
            await _db.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        [Route("/assignment/roles/deleterole")]
        public async Task<IActionResult> DeleteRole(Guid id)
        {
            var item = await _db.ApplicationRoles.FindAsync(id.ToString());
            if (item == null) return NotFound();
            _db.ApplicationRoles.Remove(item);
            await _db.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        [Route("/assignment/roles/deleterangerole")]
        public async Task<IActionResult> DeleteRangeRole(IEnumerable<string> listId)
        {
            var roles = await _db.ApplicationRoles.Where(x => listId.Contains(x.Id)).ToListAsync();
            _db.ApplicationRoles.RemoveRange(roles);
            await _db.SaveChangesAsync();
            TempData["success"] = "Xóa vai trò thành công";
            return RedirectToAction(nameof(GetAllRole));
        }

        [Route("/assignment/role/search")]
        public async Task<IActionResult> Search(string? searchStr)
        {
            var items = await _db.ApplicationRoles
                .ToListAsync();
            if (!string.IsNullOrEmpty(searchStr) && items.Count > 0)
            {
                string searchTerm = RemoveDiacritics(searchStr);

                var result = items.Where(x =>
                    RemoveDiacritics(x.Name)
                        .ToUpper().Contains(searchTerm.ToUpper()) ||
                    RemoveDiacritics(x.Id)
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

        #endregion

        #region Permission

        [Route("permission")]
        public IActionResult Permission()
        {
            return View();
        }

        [Route("permission/all")]
        public async Task<IActionResult> GetAllPermissions()
        {
            var query = from ur in _db.UserRoles
                        join u in _db.Users on ur.UserId equals u.Id
                        join r in _db.ApplicationRoles on ur.RoleId equals r.Id
                        select new
                        {
                            ur.UserId,
                            u.UserName,
                            u.Email,
                            ur.RoleId,
                            RoleName = r.Name,
                            IsActive = u.LockoutEnd == null && r.IsActive == true,
                        };
            var permissions = await query.ToListAsync();
            return Ok(permissions);
        }

        [Route("permission/getbyid")]
        public async Task<IActionResult> GetById(string userId, string roleId)
        {
            var item = await _db.UserRoles.FindAsync(userId, roleId);
            return Ok(item);
        }

        [Route("/permission/create")]
        [HttpPost]
        public async Task<IActionResult> CreatePermission(string userId, List<string> arrRoleId)
        {
            
            var user = await _userManager.FindByIdAsync(userId);
            
            if (user == null) return BadRequest();
            
            var arrRoleName = new List<string>();

            foreach (var roleId in arrRoleId)
            {
                var roleName = _roleManager.FindByIdAsync(roleId).Result?.NormalizedName;

                if (roleName != null) arrRoleName.Add(roleName);
            }

            try
            {
                var result = await _userManager.AddToRolesAsync(user, arrRoleName);
                if (result.Succeeded)
                {
                    return Ok();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return BadRequest();
        }

        [HttpPut]
        [Route("/assignment/permission/edit")]
        public async Task<IActionResult> EditPermission(string userId, List<string> arrRoleId, string oldRoleId)
        {
            var oldUserRoles = new IdentityUserRole<string>() { RoleId = oldRoleId, UserId = userId };
            _db.UserRoles.RemoveRange(oldUserRoles);
            await _db.SaveChangesAsync();

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return BadRequest();
            var arrRoleName = new List<string>();
            foreach (var roleId in arrRoleId)
            {
                var roleName = _roleManager.FindByIdAsync(roleId).Result?.NormalizedName;

                if (roleName != null) arrRoleName.Add(roleName);
            }

            try
            {
                var result = await _userManager.AddToRolesAsync(user, arrRoleName);
                return Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return BadRequest();
        }

        [HttpDelete]
        [Route("/assignment/permission/delete")]
        public async Task<IActionResult> DeletePermission(string userId, string roleId)
        {
            var item = await _db.UserRoles.FindAsync(userId, roleId);
            if (item == null) return NotFound();
            _db.UserRoles.Remove(item);
            await _db.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        [Route("/assignment/permission/deleterangepermission")]
        public async Task<IActionResult> DeleteRangePermission(List<string> arrPermission)
        {
            foreach (var permission in arrPermission)
            {
                string[] splitPermission = permission.Split(",");
                var item = await _db.UserRoles.
                    FindAsync(splitPermission[0], splitPermission[1]);
                if(item != null) _db.UserRoles.Remove(item);
            }

            await _db.SaveChangesAsync();
            return Ok();
        }

        [Route("/assignment/permission/search")]
        public async Task<IActionResult> SearchPermission(string? searchStr)
        {
            var query = from ur in _db.UserRoles
                join u in _db.Users on ur.UserId equals u.Id
                join r in _db.ApplicationRoles on ur.RoleId equals r.Id
                select new
                {
                    ur.UserId,
                    u.UserName,
                    u.Email,
                    ur.RoleId,
                    RoleName = r.Name,
                    IsActive = u.LockoutEnd == null && r.IsActive == true,
                };
            var items = await query.ToListAsync();
            if (!string.IsNullOrEmpty(searchStr) && items.Count > 0)
            {
                string searchTerm = RemoveDiacritics(searchStr);

                var result = items.Where(x =>
                    RemoveDiacritics(x.UserName)
                        .ToUpper().Contains(searchTerm.ToUpper()) ||
                    RemoveDiacritics(x.Email)
                        .ToUpper().Contains(searchTerm.ToUpper())||
                    RemoveDiacritics(x.RoleName)
                        .ToUpper().Contains(searchTerm.ToUpper())||
                    RemoveDiacritics(x.UserId)
                        .ToUpper().Contains(searchTerm.ToUpper())
                );

                return Ok(result);
            }

            return Ok(items);
        }



        #endregion
    }
}
