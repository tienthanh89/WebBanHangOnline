using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebBanHangOnline.Migrations
{
    /// <inheritdoc />
    public partial class addTbCategoryWeb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_CategoryWeb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TbAdv = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    TbCategory = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    TbContact = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    TbNews = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    TbOrder = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    TbOrderDetail = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    TbPost = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    TbProductCategory = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    TbSubscribe = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    TbSystemSetting = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_CategoryWeb", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_CategoryWeb");
        }
    }
}
