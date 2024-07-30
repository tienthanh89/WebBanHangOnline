using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;

namespace WebBanHangOnline.Models;

[Table("tb_Post")]
public partial class TbPost
{
    [Key]
    public Guid Id { get; set; }

    [StringLength(150)]
    public string? Title { get; set; }
    public Guid? CategoryId { get; set; }
    [ValidateNever]
    [ForeignKey("CategoryId")]
    public TbCategory? tbCategory { get; set; }
    public string? Description { get; set; }
    public string? Detail { get; set; }
    [ValidateNever]
    public string? ImageUrl { get; set; }
    [Required]
    public bool IsActive { get; set; }
}
