using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace WebBanHangOnline.Models.Entity
{
    public class ApplicationRole : IdentityRole
    {
        public string? Mota { get; set; }
        public bool? IsActive { get; set; }
    }
}
