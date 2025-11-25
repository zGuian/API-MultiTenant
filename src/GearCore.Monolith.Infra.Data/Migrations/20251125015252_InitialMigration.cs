using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GearCore.Monolith.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                    COL_UPDATED_AT = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_PRODUCT", x => x.COL_ID);
                });

            migrationBuilder.CreateTable(
                name: "TB_STOCK",
                columns: table => new
                {
                    COL_FK_PRODUCTID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    COL_CURRENT_QUANTITY = table.Column<int>(type: "int", nullable: false)
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
                    COL_OBSERVATION = table.Column<string>(type: "nvarchar(max)", nullable: true)
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

            migrationBuilder.CreateIndex(
                name: "IX_TB_STOCK_MOVIMENTS_COL_FK_PRODUCTID",
                table: "TB_STOCK_MOVIMENTS",
                column: "COL_FK_PRODUCTID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_STOCK");

            migrationBuilder.DropTable(
                name: "TB_STOCK_MOVIMENTS");

            migrationBuilder.DropTable(
                name: "TB_PRODUCT");
        }
    }
}
