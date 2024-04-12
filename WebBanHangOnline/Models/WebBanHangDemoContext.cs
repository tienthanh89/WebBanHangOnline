using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebBanHangOnline.Models;

public partial class WebBanHangDemoContext : DbContext
{
    public WebBanHangDemoContext()
    {
    }

    public WebBanHangDemoContext(DbContextOptions<WebBanHangDemoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TbAdv> TbAdvs { get; set; }

    public virtual DbSet<TbCategory> TbCategories { get; set; }

    public virtual DbSet<TbContact> TbContacts { get; set; }

    public virtual DbSet<TbNew> TbNews { get; set; }

    public virtual DbSet<TbOrder> TbOrders { get; set; }

    public virtual DbSet<TbOrderDetail> TbOrderDetails { get; set; }

    public virtual DbSet<TbPost> TbPosts { get; set; }

    public virtual DbSet<TbProduct> TbProducts { get; set; }

    public virtual DbSet<TbProductCategory> TbProductCategories { get; set; }

    public virtual DbSet<TbSubscribe> TbSubscribes { get; set; }

    public virtual DbSet<TbSystemSetting> TbSystemSettings { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("name=default");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TbPost>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_tb_Page");
        });

        modelBuilder.Entity<TbSubscribe>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_tb_Subcribe");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
