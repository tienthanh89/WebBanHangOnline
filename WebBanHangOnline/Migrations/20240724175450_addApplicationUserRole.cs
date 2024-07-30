using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebBanHangOnline.Migrations
{
    /// <inheritdoc />
    public partial class addApplicationUserRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApplicationRoleApplicationUser",
                columns: table => new
                {
                    ApplicationRolesId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ApplicationUsersId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationRoleApplicationUser", x => new { x.ApplicationRolesId, x.ApplicationUsersId });
                    table.ForeignKey(
                        name: "FK_ApplicationRoleApplicationUser_AspNetRoles_ApplicationRolesId",
                        column: x => x.ApplicationRolesId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationRoleApplicationUser_AspNetUsers_ApplicationUsersId",
                        column: x => x.ApplicationUsersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ApplicationRoleId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_ApplicationUserRoles_AspNetRoles_ApplicationRoleId",
                        column: x => x.ApplicationRoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ApplicationUserRoles_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationRoleApplicationUser_ApplicationUsersId",
                table: "ApplicationRoleApplicationUser",
                column: "ApplicationUsersId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserRoles_ApplicationRoleId",
                table: "ApplicationUserRoles",
                column: "ApplicationRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserRoles_ApplicationUserId",
                table: "ApplicationUserRoles",
                column: "ApplicationUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationRoleApplicationUser");

            migrationBuilder.DropTable(
                name: "ApplicationUserRoles");
        }
    }
}
