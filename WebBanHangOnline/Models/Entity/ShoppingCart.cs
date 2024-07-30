using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebBanHangOnline.Models.Entity
{
    public class ShoppingCart
    {
        [Key]
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        [ForeignKey("ProductId")]
        [ValidateNever]
        public TbProduct Product { get; set; }
        [Range(1,1000,ErrorMessage ="Vui lòng chọn số lượng từ 1 dến 1000")]
        public int Quantity { get; set; } 
        public string ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        [ValidateNever]
        public ApplicationUser ApplicationUser { get; set; }

        public ShoppingCart()
        {
            Quantity = 0;
        }
    }
}
