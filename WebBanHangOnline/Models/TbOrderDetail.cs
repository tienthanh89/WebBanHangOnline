using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebBanHangOnline.Models;

[Table("tb_OrderDetail")]
public partial class TbOrderDetail
{
    [Key]
    public int Id { get; set; }

    public int? OrderId { get; set; }

    public int? ProductId { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? Price { get; set; }

    public int? Quantity { get; set; }

    public virtual TbOrder tb_Orders { get; set; }
    public virtual TbProduct tb_Products { get; set; }
}
