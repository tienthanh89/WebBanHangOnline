using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebBanHangOnline.Models;

[Table("tb_SystemSetting")]
public partial class TbSystemSetting
{
    [Key]
    [StringLength(50)]
    public string SettingKey { get; set; } = null!;

    public string? SettingValue { get; set; }

    [StringLength(250)]
    public string? SettingDescription { get; set; }
}
