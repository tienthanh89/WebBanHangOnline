using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebBanHangOnline.Models;

[Table("tb_Adv")]
public partial class TbAdv
{
    [Key]
    public int Id { get; set; }

    [StringLength(150)]
    public string? Title { get; set; }

    [StringLength(500)]
    public string? Description { get; set; }

    [StringLength(500)]
    public string? Image { get; set; }

    public int? Type { get; set; }

    [StringLength(500)]
    public string? Link { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedDate { get; set; }

    [StringLength(150)]
    public string? CreatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ModifierDate { get; set; }

    [StringLength(150)]
    public string? ModifierBy { get; set; }
}
