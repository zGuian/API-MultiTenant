using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GearCore.Monolith.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class UserAndTenant : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    COL_ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    COL_NAME = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    COL_DESCRIPTION = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.COL_ID);
                });

            migrationBuilder.CreateTable(
                name: "TB_TENANT",
                columns: table => new
                {
                    COL_ID = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    COL_NAME = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    COL_SUBDOMAIN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    COL_IS_ACTIVE = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_TENANT", x => x.COL_ID);
                });

            migrationBuilder.CreateTable(
                name: "TB_USERS",
                columns: table => new
                {
                    COL_ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    COL_FIRST_NAME = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    COL_NORMALIZED_FIRSTNAME = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    COL_LAST_NAME = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    COL_NORMALIZED_LASTNAME = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    COL_COMPLETE_NAME = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    COL_EMAIL = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    COL_NORMALIZED_EMAIL = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    COL_EMAIL_CONFIRMED = table.Column<bool>(type: "bit", nullable: false),
                    COL_PHONE_NUMBER = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    COL_PHONE_NUMBER_CONFIRMED = table.Column<bool>(type: "bit", nullable: false),
                    COL_ACCESS_FAILED_COUNT = table.Column<int>(type: "int", nullable: false),
                    COL_PASSWORD_HASH = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    COL_CREATE_BY = table.Column<DateTime>(type: "datetime2", nullable: false),
                    COL_UPDATE_BY = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_USERS", x => x.COL_ID);
                    table.UniqueConstraint("AK_TB_USERS_COL_EMAIL", x => x.COL_EMAIL);
                });

            migrationBuilder.CreateTable(
                name: "TB_TENANT_USER",
                columns: table => new
                {
                    COL_TENANT_ID = table.Column<string>(type: "nvarchar(36)", nullable: false),
                    COL_USER_ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    COL_ROLE = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    COL_IS_ACTIVE = table.Column<bool>(type: "bit", nullable: false),
                    COL_CREATED_BY = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_TENANT_USER", x => new { x.COL_TENANT_ID, x.COL_USER_ID });
                    table.ForeignKey(
                        name: "FK_TB_TENANT_USER_TB_TENANT_COL_TENANT_ID",
                        column: x => x.COL_TENANT_ID,
                        principalTable: "TB_TENANT",
                        principalColumn: "COL_ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TB_TENANT_USER_TB_USERS_COL_USER_ID",
                        column: x => x.COL_USER_ID,
                        principalTable: "TB_USERS",
                        principalColumn: "COL_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TB_USER_ROLE",
                columns: table => new
                {
                    COL_USER_ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    COL_ROLE_ID = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_USER_ROLE", x => new { x.COL_USER_ID, x.COL_ROLE_ID });
                    table.ForeignKey(
                        name: "FK_TB_USER_ROLE_Roles_COL_ROLE_ID",
                        column: x => x.COL_ROLE_ID,
                        principalTable: "Roles",
                        principalColumn: "COL_ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TB_USER_ROLE_TB_USERS_COL_USER_ID",
                        column: x => x.COL_USER_ID,
                        principalTable: "TB_USERS",
                        principalColumn: "COL_ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TB_TENANT_USER_COL_USER_ID",
                table: "TB_TENANT_USER",
                column: "COL_USER_ID");

            migrationBuilder.CreateIndex(
                name: "IX_TB_USER_ROLE_COL_ROLE_ID",
                table: "TB_USER_ROLE",
                column: "COL_ROLE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_TB_USERS_COL_COMPLETE_NAME",
                table: "TB_USERS",
                column: "COL_COMPLETE_NAME");

            migrationBuilder.CreateIndex(
                name: "IX_TB_USERS_COL_NORMALIZED_EMAIL",
                table: "TB_USERS",
                column: "COL_NORMALIZED_EMAIL",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TB_USERS_COL_NORMALIZED_FIRSTNAME",
                table: "TB_USERS",
                column: "COL_NORMALIZED_FIRSTNAME");

            migrationBuilder.CreateIndex(
                name: "IX_TB_USERS_COL_NORMALIZED_LASTNAME",
                table: "TB_USERS",
                column: "COL_NORMALIZED_LASTNAME");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_TENANT_USER");

            migrationBuilder.DropTable(
                name: "TB_USER_ROLE");

            migrationBuilder.DropTable(
                name: "TB_TENANT");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "TB_USERS");
        }
    }
}
