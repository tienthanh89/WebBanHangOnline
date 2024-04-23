using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebBanHangOnline.Migrations
{
    /// <inheritdoc />
    public partial class updatePostDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "tb_Post");

            migrationBuilder.DropColumn(
                name: "PriceSale",
                table: "tb_Post");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "tb_Post");

            migrationBuilder.AddColumn<string>(
                name: "SeoDescription",
                table: "tb_Post",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SeoKeyWords",
                table: "tb_Post",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SeoTitle",
                table: "tb_Post",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SeoDescription",
                table: "tb_Post");

            migrationBuilder.DropColumn(
                name: "SeoKeyWords",
                table: "tb_Post");

            migrationBuilder.DropColumn(
                name: "SeoTitle",
                table: "tb_Post");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "tb_Post",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PriceSale",
                table: "tb_Post",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "tb_Post",
                type: "int",
                nullable: true);
        }
    }
}
