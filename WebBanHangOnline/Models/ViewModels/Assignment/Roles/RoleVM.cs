using Microsoft.AspNetCore.Identity;
using WebBanHangOnline.Models.Entity;

namespace WebBanHangOnline.Models.ViewModels.Assignment.Roles
{
    public class RoleVM
    {
        public IEnumerable<IdentityRole> IdentityRoles { get; set; }
        public IEnumerable<ApplicationRole> ApplicationRoles { get; set; }
        //public string? id { get; set; }
        //public string? name { get; set; }
        //public string? mota {  get; set; }
        //public bool? isActive { get; set; }
    }
}
