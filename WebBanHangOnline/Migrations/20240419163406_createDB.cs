using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebBanHangOnline.Migrations
{
    /// <inheritdoc />
    public partial class createDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_Adv",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Image = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Type = table.Column<int>(type: "int", nullable: true),
                    Link = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    ModifierDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ModifierBy = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_Adv", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tb_Category",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Alias = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Position = table.Column<int>(type: "int", nullable: false),
                    SeoTitle = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    SeoDescription = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    SeoKeyWords = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    ModifierDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ModifierBy = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_Category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tb_Contact",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Website = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsRead = table.Column<bool>(type: "bit", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    ModifierDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ModifierBy = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_Contact", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tb_Order",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CustomerName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    ModifierDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ModifierBy = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_Order", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tb_ProductCategory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Icon = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    ModifierDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ModifierBy = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_ProductCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tb_Subscribe",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_Subcribe", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tb_SystemSetting",
                columns: table => new
                {
                    SettingKey = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SettingValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SettingDescription = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_SystemSetting", x => x.SettingKey);
                });

            migrationBuilder.CreateTable(
                name: "tb_New",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Alias = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoryId = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Detail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SeoTitle = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    SeoDescription = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    SeoKeyWords = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    ModifierDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ModifierBy = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_New", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tb_New_tb_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "tb_Category",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "tb_Post",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Alias = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoryId = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Detail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PriceSale = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    ModifierDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ModifierBy = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_Page", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tb_Post_tb_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "tb_Category",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "tb_Product",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Alias = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductCategoryID = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Detail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PriceSale = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: true),
                    SeoTitle = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    SeoDescription = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    SeoKeyWords = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    ModifierDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ModifierBy = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_Product", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tb_Product_tb_ProductCategory_ProductCategoryID",
                        column: x => x.ProductCategoryID,
                        principalTable: "tb_ProductCategory",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "tb_OrderDetail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: true),
                    ProductId = table.Column<int>(type: "int", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_OrderDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tb_OrderDetail_tb_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "tb_Order",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_tb_OrderDetail_tb_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "tb_Product",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_New_CategoryId",
                table: "tb_New",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_OrderDetail_OrderId",
                table: "tb_OrderDetail",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_OrderDetail_ProductId",
                table: "tb_OrderDetail",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_Post_CategoryId",
                table: "tb_Post",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_Product_ProductCategoryID",
                table: "tb_Product",
                column: "ProductCategoryID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_Adv");

            migrationBuilder.DropTable(
                name: "tb_Contact");

            migrationBuilder.DropTable(
                name: "tb_New");

            migrationBuilder.DropTable(
                name: "tb_OrderDetail");

            migrationBuilder.DropTable(
                name: "tb_Post");

            migrationBuilder.DropTable(
                name: "tb_Subscribe");

            migrationBuilder.DropTable(
                name: "tb_SystemSetting");

            migrationBuilder.DropTable(
                name: "tb_Order");

            migrationBuilder.DropTable(
                name: "tb_Product");

            migrationBuilder.DropTable(
                name: "tb_Category");

            migrationBuilder.DropTable(
                name: "tb_ProductCategory");
        }
    }
}
