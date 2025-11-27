using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GearCore.Monolith.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddIdentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    COL_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    COL_NAME = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    COL_NORMALIZED_NAME = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    COL_CONCURRENCY_STAMP = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.COL_ID);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    COL_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    COL_TENANT_ID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    COL_USERNAME = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    COL_NORMALIZED_USERNAME = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    COL_EMAIL = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    COL_NORMALIZED_EMAIL = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    COL_EMAIL_CONFIRMED = table.Column<bool>(type: "bit", nullable: false),
                    COL_PASSWORD_HASH = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    COL_SECURITY_STAMP = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    COL_CONCURRENCY_STAMP = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    COL_PHONE_NUMBER = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    COL_PHONE_NUMBER_CONFIRMED = table.Column<bool>(type: "bit", nullable: false),
                    COL_TWO_FACTOR_ENABLED = table.Column<bool>(type: "bit", nullable: false),
                    COL_LOCKOUT_END = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    COL_LOCKOUT_ENABLED = table.Column<bool>(type: "bit", nullable: false),
                    COL_ACCESS_FAILED_COUNT = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.COL_ID);
                });

            migrationBuilder.CreateTable(
                name: "TB_PRODUCT",
                columns: table => new
                {
                    COL_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    COL_NAME = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    COL_DESCRIPTION = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    COL_PRICE = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    COL_IS_ACTIVE = table.Column<bool>(type: "bit", nullable: false),
                    COL_BRAND = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    COL_CREATED_AT = table.Column<DateTime>(type: "datetime2", nullable: false),
                    COL_UPDATED_AT = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TenantId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_PRODUCT", x => x.COL_ID);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "COL_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "COL_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "COL_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "COL_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "COL_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    COL_USER_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    COL_LOGIN_PROVIDER = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    COL_NAME = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    COL_VALUE = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.COL_USER_ID, x.COL_LOGIN_PROVIDER, x.COL_NAME });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_COL_USER_ID",
                        column: x => x.COL_USER_ID,
                        principalTable: "AspNetUsers",
                        principalColumn: "COL_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TB_STOCK",
                columns: table => new
                {
                    COL_FK_PRODUCTID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    COL_CURRENT_QUANTITY = table.Column<int>(type: "int", nullable: false),
                    TenantId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_STOCK", x => x.COL_FK_PRODUCTID);
                    table.ForeignKey(
                        name: "FK_TB_STOCK_TB_PRODUCT_COL_FK_PRODUCTID",
                        column: x => x.COL_FK_PRODUCTID,
                        principalTable: "TB_PRODUCT",
                        principalColumn: "COL_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TB_STOCK_MOVIMENTS",
                columns: table => new
                {
                    COL_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    COL_FK_PRODUCTID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    COL_QUANTITY = table.Column<int>(type: "int", nullable: false),
                    COL_PREVIOUS_QUANTITY = table.Column<int>(type: "int", nullable: false),
                    COL_NEW_QUANTITY = table.Column<int>(type: "int", nullable: false),
                    COL_MOVEMENT_DATE = table.Column<DateTime>(type: "datetime2", nullable: false),
                    COL_MOVEMENT_TYPE = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    COL_ORIGIN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    COL_OBSERVATION = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenantId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_STOCK_MOVIMENTS", x => x.COL_ID);
                    table.ForeignKey(
                        name: "FK_TB_STOCK_MOVIMENTS_TB_PRODUCT_COL_FK_PRODUCTID",
                        column: x => x.COL_FK_PRODUCTID,
                        principalTable: "TB_PRODUCT",
                        principalColumn: "COL_ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TB_USER_TOKEN",
                columns: table => new
                {
                    COL_USER_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    COL_LOGIN_PROVIDER = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    COL_NAME = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_USER_TOKEN", x => new { x.COL_USER_ID, x.COL_LOGIN_PROVIDER, x.COL_NAME });
                    table.ForeignKey(
                        name: "FK_TB_USER_TOKEN_AspNetUserTokens_COL_USER_ID_COL_LOGIN_PROVIDER_COL_NAME",
                        columns: x => new { x.COL_USER_ID, x.COL_LOGIN_PROVIDER, x.COL_NAME },
                        principalTable: "AspNetUserTokens",
                        principalColumns: new[] { "COL_USER_ID", "COL_LOGIN_PROVIDER", "COL_NAME" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "COL_NORMALIZED_NAME",
                unique: true,
                filter: "[COL_NORMALIZED_NAME] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "COL_NORMALIZED_EMAIL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_COL_EMAIL_COL_TENANT_ID",
                table: "AspNetUsers",
                columns: new[] { "COL_EMAIL", "COL_TENANT_ID" },
                unique: true,
                filter: "[COL_EMAIL] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_COL_USERNAME_COL_TENANT_ID",
                table: "AspNetUsers",
                columns: new[] { "COL_USERNAME", "COL_TENANT_ID" },
                unique: true,
                filter: "[COL_USERNAME] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "COL_NORMALIZED_USERNAME",
                unique: true,
                filter: "[COL_NORMALIZED_USERNAME] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_TB_STOCK_MOVIMENTS_COL_FK_PRODUCTID",
                table: "TB_STOCK_MOVIMENTS",
                column: "COL_FK_PRODUCTID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "TB_STOCK");

            migrationBuilder.DropTable(
                name: "TB_STOCK_MOVIMENTS");

            migrationBuilder.DropTable(
                name: "TB_USER_TOKEN");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "TB_PRODUCT");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
