﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Domain;

[Table("tb_ProductCategory")]
public partial class TbProductCategory
{
    [Key]
    public Guid Id { get; set; }
    [StringLength(150)]
    public string? Title { get; set; }
    public bool IsActive { get; set; }
}
