using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;

namespace WebBanHangOnline.Models;

[Table("tb_New")]
public partial class TbNews
{
    [Key]
    public int Id { get; set; }
    [Required(ErrorMessage ="Vui lòng điền tiêu đề.")]
    [MaxLength(250, ErrorMessage ="Tiêu đề chỉ bao gồm 250 ký tự")]
    public string? Title { get; set; }

    public string? Alias { get; set; }
    [Required]
    public int? CategoryId { get; set; }
    [ForeignKey("CategoryId")]
    [ValidateNever]
    public TbCategory? tbCategory { get; set; }

    [Required(ErrorMessage ="Vui lòng điền mô tả")]
    public string? Description { get; set; }
    [Required(ErrorMessage ="Vui lòng điền chi tiết")]
    public string? Detail { get; set; }
    [ValidateNever]
    public string? ImageUrl { get; set; }

    [Required(ErrorMessage = "Vui lòng điền SeoTitle")]
    [MaxLength(250, ErrorMessage = "Nội dung Seotitle chỉ phải ít hơn 250 ký tự.")]
    public string? SeoTitle { get; set; }

    [Required(ErrorMessage = "Vui lòng điền SeoDescription")]
    [MaxLength(500, ErrorMessage = "Nội dung SeoDescription chỉ phải ít hơn 500 ký tự.")]
    public string? SeoDescription { get; set; }

    [Required(ErrorMessage = "Vui lòng điền SeoKeyWords")]
    [MaxLength(250, ErrorMessage = "Nội dung SeoKeyWords chỉ phải ít hơn 250 ký tự.")]
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
}
