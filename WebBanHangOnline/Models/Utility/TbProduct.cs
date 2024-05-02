using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebBanHangOnline.Models;

[Table("tb_Product")]
public partial class TbProduct
{
    [Key]
    public int Id { get; set; }
    [Required(ErrorMessage ="Vui lòng điền tên sản phẩm")]
    [StringLength(150)]
    public string? Title { get; set; }
    [Required(ErrorMessage ="Vui lòng điền mã sản phẩm")]
    [StringLength(50)]
    public string? ProductCode { get; set; }
    public string? Alias { get; set; }

    [Column("ProductCategoryID")]
    public int? ProductCategoryId { get; set; }
    [ForeignKey("ProductCategoryId")]
    public TbProductCategory? tbProductCategory { get; set; }
    [Required(ErrorMessage = "Vui lòng điền mô tả sản phẩm")]
    public string? Description { get; set; }
    [Required(ErrorMessage = "Vui lòng điền chi tiết sản phẩm")]
    public string? Detail { get; set; }
    [Required(ErrorMessage = "Vui lòng thêm hình ảnh sản phẩm")]
    [StringLength(500)]
    public string? ImageUrl { get; set; }
    [Required(ErrorMessage = "Vui lòng điền giá sản phẩm")]
    [Column(TypeName = "decimal(18, 2)")]
    public decimal? Price { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? PriceSale { get; set; }
    [Required(ErrorMessage = "Vui lòng điền số lượng sản phẩm")]
    public int? Quantity { get; set; }

    [StringLength(250)]
    public string? SeoTitle { get; set; }

    [StringLength(500)]
    public string? SeoDescription { get; set; }

    [StringLength(250)]
    public string? SeoKeyWords { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedDate { get; set; }

    [StringLength(150)]
    public string? CreatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ModifierDate { get; set; }

    [StringLength(150)]
    public string? ModifierBy { get; set; }

    [Required]
    public bool IsActive { get; set; }
    [Required]
    public bool IsHome { get; set; }
    [Required]
    public bool IsHot { get; set; }
    [Required]
    public bool IsFeature { get; set; }
    [Required]
    public bool IsSale { get; set; }

}
