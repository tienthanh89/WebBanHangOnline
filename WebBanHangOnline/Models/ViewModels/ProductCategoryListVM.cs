using System.ComponentModel.DataAnnotations;

namespace WebBanHangOnline.Models.ViewModels
{
    public class ProductCategoryListVM
    {
        [Key]
        public int Id { get; set; }
        public string? Title { get; set; }  
        public int? Count { get; set; }
    }
}
