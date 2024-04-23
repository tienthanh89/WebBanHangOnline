using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebBanHangOnline.Migrations
{
    /// <inheritdoc />
    public partial class updateNewsImageUrl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Image",
                table: "tb_New",
                newName: "ImageUrl");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "tb_New",
                newName: "Image");
        }
    }
}
