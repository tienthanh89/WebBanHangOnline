using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;

namespace WebBanHangOnline.Models;

[Table("tb_Product")]
public partial class TbProduct
{
    [Key]
    public Guid Id { get; set; }
    [Required(ErrorMessage ="Vui lòng điền tên sản phẩm")]
    [StringLength(150)]
    public string? Title { get; set; }
    [Column("ProductCategoryID")]
    public Guid? ProductCategoryId { get; set; }
    [ForeignKey("ProductCategoryId")]
    [ValidateNever]
    public TbProductCategory? tbProductCategory { get; set; }

    [Required(ErrorMessage = "Vui lòng điền chi tiết sản phẩm")]
    public string? Detail { get; set; }
    [Required(ErrorMessage = "Vui lòng thêm hình ảnh sản phẩm")]
    [StringLength(500)]
    [ValidateNever]
    public string? ImageUrl { get; set; }
    [Required(ErrorMessage = "Vui lòng điền giá sản phẩm")]
    public double Price { get; set; }
    [Required]
    public double PriceSale { get; set; }
    [Required(ErrorMessage = "Vui lòng điền số lượng sản phẩm")]
    public int Quantity { get; set; }
    [Required]
    public bool IsActive { get; set; }
    public bool IsHot { get; set; }

}
