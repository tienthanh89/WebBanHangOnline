using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebBanHangOnline.Models.ViewModels.User
{
    public class ShopProductVM
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? ImageUrl { get; set; }
        public double? Price { get; set; }
        public string? Description { get; set; }
        public string? ProductCategory { get; set; }
    }
}
