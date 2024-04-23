using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebBanHangOnline.Migrations
{
    /// <inheritdoc />
    public partial class updateTbCategoryWeb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TbAdv",
                table: "tb_CategoryWeb");

            migrationBuilder.DropColumn(
                name: "TbCategory",
                table: "tb_CategoryWeb");

            migrationBuilder.DropColumn(
                name: "TbContact",
                table: "tb_CategoryWeb");

            migrationBuilder.DropColumn(
                name: "TbNews",
                table: "tb_CategoryWeb");

            migrationBuilder.DropColumn(
                name: "TbOrder",
                table: "tb_CategoryWeb");

            migrationBuilder.DropColumn(
                name: "TbOrderDetail",
                table: "tb_CategoryWeb");

            migrationBuilder.DropColumn(
                name: "TbPost",
                table: "tb_CategoryWeb");

            migrationBuilder.DropColumn(
                name: "TbProductCategory",
                table: "tb_CategoryWeb");

            migrationBuilder.DropColumn(
                name: "TbSubscribe",
                table: "tb_CategoryWeb");

            migrationBuilder.RenameColumn(
                name: "TbSystemSetting",
                table: "tb_CategoryWeb",
                newName: "Name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "tb_CategoryWeb",
                newName: "TbSystemSetting");

            migrationBuilder.AddColumn<string>(
                name: "TbAdv",
                table: "tb_CategoryWeb",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TbCategory",
                table: "tb_CategoryWeb",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TbContact",
                table: "tb_CategoryWeb",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TbNews",
                table: "tb_CategoryWeb",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TbOrder",
                table: "tb_CategoryWeb",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TbOrderDetail",
                table: "tb_CategoryWeb",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TbPost",
                table: "tb_CategoryWeb",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TbProductCategory",
                table: "tb_CategoryWeb",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TbSubscribe",
                table: "tb_CategoryWeb",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true);
        }
    }
}
