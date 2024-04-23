using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebBanHangOnline.Migrations
{
    /// <inheritdoc />
    public partial class updateTbNews2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_New_tb_Category_CategoryId",
                table: "tb_New");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "tb_New",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_New_tb_Category_CategoryId",
                table: "tb_New",
                column: "CategoryId",
                principalTable: "tb_Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_New_tb_Category_CategoryId",
                table: "tb_New");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "tb_New",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_New_tb_Category_CategoryId",
                table: "tb_New",
                column: "CategoryId",
                principalTable: "tb_Category",
                principalColumn: "Id");
        }
    }
}
