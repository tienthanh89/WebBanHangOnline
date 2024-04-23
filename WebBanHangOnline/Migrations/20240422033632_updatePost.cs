using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebBanHangOnline.Migrations
{
    /// <inheritdoc />
    public partial class updatePost : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "tb_Post");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "tb_Post",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "tb_Post");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "tb_Post",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);
        }
    }
}
