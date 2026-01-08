using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GearCore.Monolith.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TB_PRODUCT",
                columns: table => new
                {
                    COL_ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    COL_NAME = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    COL_DESCRIPTION = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    COL_PRICE = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    COL_IS_ACTIVE = table.Column<bool>(type: "bit", nullable: false),
                    COL_BRAND = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    FK_TENANT_ID = table.Column<string>(type: "nvarchar(36)", nullable: false),
                    COL_CREATE_BY = table.Column<DateTime>(type: "datetime2", nullable: false),
                    COL_UPDATE_BY = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_PRODUCT", x => x.COL_ID);
                    table.ForeignKey(
                        name: "FK_TB_PRODUCT_TB_TENANT_FK_TENANT_ID",
                        column: x => x.FK_TENANT_ID,
                        principalTable: "TB_TENANT",
                        principalColumn: "COL_ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TB_PRODUCT_FK_TENANT_ID_COL_NAME_COL_BRAND",
                table: "TB_PRODUCT",
                columns: new[] { "FK_TENANT_ID", "COL_NAME", "COL_BRAND" },
                unique: true,
                filter: "[COL_BRAND] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_PRODUCT");
        }
    }
}
