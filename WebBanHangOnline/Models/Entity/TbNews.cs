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
    public Guid Id { get; set; }
    [Required(ErrorMessage ="Vui lòng điền tiêu đề.")]
    [MaxLength(250, ErrorMessage ="Tiêu đề chỉ bao gồm 250 ký tự")]
    public string? Title { get; set; }

    [Required]
    public Guid? CategoryId { get; set; }
    [ForeignKey("CategoryId")]
    [ValidateNever]
    public TbCategory? tbCategory { get; set; }

    [Required(ErrorMessage ="Vui lòng điền mô tả")]
    public string? Description { get; set; }
    [Required(ErrorMessage ="Vui lòng điền chi tiết")]
    public string? Detail { get; set; }
    [ValidateNever]
    public string? ImageUrl { get; set; }
    [Required]
    public bool IsActive { get; set; }
}
