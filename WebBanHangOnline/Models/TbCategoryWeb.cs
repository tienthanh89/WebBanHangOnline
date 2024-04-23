using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebBanHangOnline.Models;

[Table("tb_CategoryWeb")]
public partial class TbCategoryWeb
{
    [Key]
    public int Id { get; set; }

    [StringLength(30)]
    public string? Name { get; set; }  
}
