using System.ComponentModel.DataAnnotations;

namespace WebBanHangOnline.Models.ViewModels.User
{
    public class ShopDetailVM
    {
        [Key]
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Category {  get; set; }
        public double? Price { get; set; }
        public string? ImageUrl { get; set; }
        public string? Description { get; set; }
        public string? Detail {  get; set; }
        public int? Quantity { get; set; }
    }
}
