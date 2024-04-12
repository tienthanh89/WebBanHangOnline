using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebBanHangOnline.Models;

[Table("tb_Order")]
public partial class TbOrder
{
    public TbOrder()
    {
        this.tb_OrderDetails = new HashSet<TbOrderDetail>();
    }

    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    public string? Code { get; set; }

    [StringLength(150)]
    public string? CustomerName { get; set; }

    [StringLength(20)]
    public string? Phone { get; set; }

    [StringLength(500)]
    public string? Address { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? TotalAmount { get; set; }

    public int? Quantity { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedDate { get; set; }

    [StringLength(150)]
    public string? CreatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ModifierDate { get; set; }

    [StringLength(150)]
    public string? ModifierBy { get; set; }

    public ICollection<TbOrderDetail> tb_OrderDetails {  get; set; }
}
