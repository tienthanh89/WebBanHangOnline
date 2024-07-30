using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebBanHangOnline.Models.Entity;


namespace WebBanHangOnline.Models.DbContext;

public partial class WebBanHangOnlineContext : IdentityDbContext<IdentityUser>
{
    public WebBanHangOnlineContext() { }
    public WebBanHangOnlineContext(DbContextOptions<WebBanHangOnlineContext> options) : base(options) { }

    public virtual DbSet<TbCategory> TbCategories { get; set; }
    public virtual DbSet<TbNews> TbNews { get; set; }
    public virtual DbSet<TbPost> TbPosts { get; set; }
    public virtual DbSet<TbProduct> TbProducts { get; set; }
    public virtual DbSet<TbProductCategory> TbProductCategories { get; set; }
    public virtual DbSet<ApplicationUser> ApplicationUsers { get; set; }
    public virtual DbSet<ApplicationRole> ApplicationRoles { get; set; }
    public virtual DbSet<ShoppingCart> ShoppingCarts { get; set; }
    public virtual DbSet<OrderDetail> OrderDetails { get; set; }
    public virtual DbSet<OrderHeader> OrderHeaders { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("name=default");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<TbPost>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_tb_Page");
        });

        modelBuilder.Entity<ApplicationRole>(entity =>
        {
            entity.Property(p => p.Mota).HasMaxLength(255);
            entity.Property(p => p.IsActive).HasDefaultValue(true);
        });


        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
