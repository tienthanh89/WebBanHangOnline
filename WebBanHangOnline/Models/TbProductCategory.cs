using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebBanHangOnline.Models;

[Table("tb_ProductCategory")]
public partial class TbProductCategory
{
    public TbProductCategory()
    {
        this.tb_Products = new HashSet<TbProduct>();
    }

    [Key]
    public int Id { get; set; }

    [StringLength(150)]
    public string? Title { get; set; }

    [StringLength(500)]
    public string? Description { get; set; }

    [StringLength(500)]
    public string? Icon { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedDate { get; set; }

    [StringLength(150)]
    public string? CreatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ModifierDate { get; set; }

    [StringLength(150)]
    public string? ModifierBy { get; set; }

    public ICollection<TbProduct> tb_Products { get; set; }
}
