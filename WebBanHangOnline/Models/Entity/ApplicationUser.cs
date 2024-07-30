using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace WebBanHangOnline.Models.Entity
{
    public class ApplicationUser:IdentityUser
    {
        [Required]
        public string Name { get; set; }
        public string StreetAddress { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; }
    }
}
