using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GearCore.Monolith.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddStockAndSales : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TB_USER_ROLE_Roles_COL_ROLE_ID",
                table: "TB_USER_ROLE");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Roles",
                table: "Roles");

            migrationBuilder.RenameTable(
                name: "Roles",
                newName: "TB_ROLE");

            migrationBuilder.RenameColumn(
                name: "COL_UPDATE_BY",
                table: "TB_USERS",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "COL_CREATE_BY",
                table: "TB_USERS",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "COL_UPDATE_BY",
                table: "TB_PRODUCT",
                newName: "COL_UPDATE_AT");

            migrationBuilder.RenameColumn(
                name: "COL_CREATE_BY",
                table: "TB_PRODUCT",
                newName: "COL_CREATE_AT");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TB_ROLE",
                table: "TB_ROLE",
                column: "COL_ID");

            migrationBuilder.CreateTable(
                name: "TB_SALES",
                columns: table => new
                {
                    COL_ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FK_TENANT_ID = table.Column<string>(type: "nvarchar(36)", nullable: false),
                    FK_USER_ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    COL_TOTAL_AMOUNT = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    COL_DISCOUNT_AMOUNT = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    COL_FINAL_AMOUNT = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    COL_STATUS = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    COL_CREATE_AT = table.Column<DateTime>(type: "datetime2", nullable: false),
                    COL_UPDATE_AT = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_SALES", x => x.COL_ID);
                    table.ForeignKey(
                        name: "FK_TB_SALES_TB_TENANT_FK_TENANT_ID",
                        column: x => x.FK_TENANT_ID,
                        principalTable: "TB_TENANT",
                        principalColumn: "COL_ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TB_SALES_TB_USERS_FK_USER_ID",
                        column: x => x.FK_USER_ID,
                        principalTable: "TB_USERS",
                        principalColumn: "COL_ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TB_STOCK",
                columns: table => new
                {
                    COL_ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FK_TENANT_ID = table.Column<string>(type: "nvarchar(36)", nullable: false),
                    FK_PRODUCT_ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    COL_QUANTITY = table.Column<int>(type: "int", nullable: false),
                    COL_RESERVED_QUANTITY = table.Column<int>(type: "int", nullable: false),
                    COL_ACTIVE = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    COL_CREATE_AT = table.Column<DateTime>(type: "datetime2", nullable: false),
                    COL_UPDATE_AT = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_STOCK", x => x.COL_ID);
                    table.ForeignKey(
                        name: "FK_TB_STOCK_TB_PRODUCT_FK_PRODUCT_ID",
                        column: x => x.FK_PRODUCT_ID,
                        principalTable: "TB_PRODUCT",
                        principalColumn: "COL_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TB_STOCK_TB_TENANT_FK_TENANT_ID",
                        column: x => x.FK_TENANT_ID,
                        principalTable: "TB_TENANT",
                        principalColumn: "COL_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TB_SALE_ITEM",
                columns: table => new
                {
                    COL_ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FK_SALE_ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FK_PRODUCT_ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    COL_PRODUCT_NAME = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    COL_UNIT_PRICE = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    COL_QUANTITY = table.Column<int>(type: "int", nullable: false),
                    COL_TOTAL_PRICE = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_SALE_ITEM", x => x.COL_ID);
                    table.ForeignKey(
                        name: "FK_TB_SALE_ITEM_TB_PRODUCT_FK_PRODUCT_ID",
                        column: x => x.FK_PRODUCT_ID,
                        principalTable: "TB_PRODUCT",
                        principalColumn: "COL_ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TB_SALE_ITEM_TB_SALES_FK_SALE_ID",
                        column: x => x.FK_SALE_ID,
                        principalTable: "TB_SALES",
                        principalColumn: "COL_ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TB_STOCK_MOVIMENTS",
                columns: table => new
                {
                    COL_ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FK_STOCK_ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    COL_TYPE = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    COL_QUANTITY = table.Column<int>(type: "int", nullable: false),
                    COL_REASON = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    COL_REFERENCE_ID = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    COL_CREATE_AT = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_STOCK_MOVIMENTS", x => x.COL_ID);
                    table.ForeignKey(
                        name: "FK_TB_STOCK_MOVIMENTS_TB_STOCK_FK_STOCK_ID",
                        column: x => x.FK_STOCK_ID,
                        principalTable: "TB_STOCK",
                        principalColumn: "COL_ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TB_SALE_ITEM_FK_PRODUCT_ID",
                table: "TB_SALE_ITEM",
                column: "FK_PRODUCT_ID");

            migrationBuilder.CreateIndex(
                name: "IX_TB_SALE_ITEM_FK_SALE_ID",
                table: "TB_SALE_ITEM",
                column: "FK_SALE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_TB_SALES_FK_TENANT_ID",
                table: "TB_SALES",
                column: "FK_TENANT_ID");

            migrationBuilder.CreateIndex(
                name: "IX_TB_SALES_FK_USER_ID",
                table: "TB_SALES",
                column: "FK_USER_ID");

            migrationBuilder.CreateIndex(
                name: "IX_TB_STOCK_FK_PRODUCT_ID",
                table: "TB_STOCK",
                column: "FK_PRODUCT_ID");

            migrationBuilder.CreateIndex(
                name: "IX_TB_STOCK_FK_TENANT_ID",
                table: "TB_STOCK",
                column: "FK_TENANT_ID");

            migrationBuilder.CreateIndex(
                name: "IX_TB_STOCK_MOVIMENTS_FK_STOCK_ID",
                table: "TB_STOCK_MOVIMENTS",
                column: "FK_STOCK_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_TB_USER_ROLE_TB_ROLE_COL_ROLE_ID",
                table: "TB_USER_ROLE",
                column: "COL_ROLE_ID",
                principalTable: "TB_ROLE",
                principalColumn: "COL_ID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TB_USER_ROLE_TB_ROLE_COL_ROLE_ID",
                table: "TB_USER_ROLE");

            migrationBuilder.DropTable(
                name: "TB_SALE_ITEM");

            migrationBuilder.DropTable(
                name: "TB_STOCK_MOVIMENTS");

            migrationBuilder.DropTable(
                name: "TB_SALES");

            migrationBuilder.DropTable(
                name: "TB_STOCK");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TB_ROLE",
                table: "TB_ROLE");

            migrationBuilder.RenameTable(
                name: "TB_ROLE",
                newName: "Roles");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "TB_USERS",
                newName: "COL_UPDATE_BY");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "TB_USERS",
                newName: "COL_CREATE_BY");

            migrationBuilder.RenameColumn(
                name: "COL_UPDATE_AT",
                table: "TB_PRODUCT",
                newName: "COL_UPDATE_BY");

            migrationBuilder.RenameColumn(
                name: "COL_CREATE_AT",
                table: "TB_PRODUCT",
                newName: "COL_CREATE_BY");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Roles",
                table: "Roles",
                column: "COL_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_TB_USER_ROLE_Roles_COL_ROLE_ID",
                table: "TB_USER_ROLE",
                column: "COL_ROLE_ID",
                principalTable: "Roles",
                principalColumn: "COL_ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
