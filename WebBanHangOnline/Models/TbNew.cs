using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebBanHangOnline.Models;

[Table("tb_New")]
public partial class TbNew
{
    [Key]
    public int Id { get; set; }

    [StringLength(250)]
    public string? Title { get; set; }
    
    public int? CategoryId { get; set; }

    // Foreign key
    // Khi ta khai báo trong 1 Model TbNew có 1 Model khác thì nó sẽ tạo ra mối liên hệ giữa 2 Model
    // và qua đó nó sẽ tạo ra mối liên hệ giữa các bảng, các table trên sql server
    // EfCore sẽ tự động tạo khóa ngoại theo tên khóa chính của TbCategory
    // để chỉ định tên khóa ngoại theo ý muốn ta sử dụng [ForeinKey("CategoryId")]
    [ForeignKey("CategoryId")]
    public TbCategory? Category { get; set; } // Fk -> Pk Id

    public string? Description { get; set; }

    public string? Detail { get; set; }

    [StringLength(500)]
    public string? Image { get; set; }

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
}
