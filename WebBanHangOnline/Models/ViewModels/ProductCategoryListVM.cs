using System.ComponentModel.DataAnnotations;

namespace WebBanHangOnline.Models.ViewModels
{
    public class ProductCategoryListVM
    {
        [Key]
        public Guid Id { get; set; }
        public string? Title { get; set; }  
        public int? Count { get; set; }
    }
}
