using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebBanHangOnline.Models;

[Table("tb_Category")]
public partial class TbCategory
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "Vui lòng điền tiêu đề.")]
    [MaxLength(150, ErrorMessage = "Tiêu đề phải ít hơn 150 ký tự")]
    [StringLength(150)]
    public string? Title { get; set; }

    public string? Alias { get; set; }

    [Required(ErrorMessage ="Vui lòng điền mô tả.")]
    [MaxLength(500, ErrorMessage = "Nội dung mô tả chỉ phải ít hơn 500 ký tự.")]
    public string? Description { get; set; }

    [Required(ErrorMessage = "Vui lòng điền vị trí.")]
    [Range(1,255,ErrorMessage ="Vị trí nằm trong khoảng từ 1 đến 255")]
    public int? Position { get; set; }

    [Required(ErrorMessage = "Vui lòng điền SeoTitle.")]
    [MaxLength(250, ErrorMessage = "Nội dung Seotitle chỉ phải ít hơn 250 ký tự.")]
    public string? SeoTitle { get; set; }

    [Required(ErrorMessage = "Vui lòng điền SeoDescription.")]
    [MaxLength(500, ErrorMessage = "Nội dung SeoDescription chỉ phải ít hơn 500 ký tự.")]
    public string? SeoDescription { get; set; }

    [Required(ErrorMessage = "Vui lòng điền SeoKeyWords.")]
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
