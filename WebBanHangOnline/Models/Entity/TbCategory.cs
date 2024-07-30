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
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Vui lòng điền tiêu đề.")]
    [MaxLength(150, ErrorMessage = "Tiêu đề phải ít hơn 150 ký tự")]
    [StringLength(150)]
    public string? Title { get; set; }
    [Required]
    public bool IsActive { get; set; }
}
