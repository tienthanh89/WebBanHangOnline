using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebBanHangOnline.Models.Entity
{
    public class OrderDetail
    {
        [Key]
        public Guid Id { get; set; }

        public Guid OrderHeaderId { get; set; }
        [ForeignKey("OrderHeaderId")]
        [ValidateNever]
        public OrderHeader OrderHeader { get; set; }

        public Guid ProductId { get; set; }
        [ForeignKey("ProductId")]
        [ValidateNever]
        public TbProduct Product { get; set; }

        public int Count { get; set; }
        // Giá của sản phẩm tại thời điểm đặt hàng
        public double Price { get; set; }
    }
}
