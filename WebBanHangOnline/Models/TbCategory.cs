﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebBanHangOnline.Models;

[Table("tb_Category")]
public partial class TbCategory
{
    public TbCategory()
    {
        this.tb_News = new HashSet<TbNew>();
        //this.tb_Posts = new HashSet<TbPost>();
    }

    [Key]
    public int Id { get; set; }

    [StringLength(150)]
    public string? Title { get; set; }

    [StringLength(500)]
    public string? Description { get; set; }

    public int? Position { get; set; }

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

    public ICollection<TbNew> tb_News { get; set; }
    public ICollection<TbPost> tb_Posts { get; set; }
}