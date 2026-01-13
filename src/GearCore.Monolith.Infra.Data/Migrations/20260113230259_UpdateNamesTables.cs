using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GearCore.Monolith.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateNamesTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TB_SALE_ITEM_TB_SALES_FK_SALE_ID",
                table: "TB_SALE_ITEM");

            migrationBuilder.DropForeignKey(
                name: "FK_TB_SALES_TB_TENANT_FK_TENANT_ID",
                table: "TB_SALES");

            migrationBuilder.DropForeignKey(
                name: "FK_TB_SALES_TB_USERS_FK_USER_ID",
                table: "TB_SALES");

            migrationBuilder.DropForeignKey(
                name: "FK_TB_TENANT_USER_TB_TENANT_COL_TENANT_ID",
                table: "TB_TENANT_USER");

            migrationBuilder.DropForeignKey(
                name: "FK_TB_TENANT_USER_TB_USERS_COL_USER_ID",
                table: "TB_TENANT_USER");

            migrationBuilder.DropForeignKey(
                name: "FK_TB_USER_ROLE_TB_ROLE_COL_ROLE_ID",
                table: "TB_USER_ROLE");

            migrationBuilder.DropForeignKey(
                name: "FK_TB_USER_ROLE_TB_USERS_COL_USER_ID",
                table: "TB_USER_ROLE");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_TB_USERS_COL_EMAIL",
                table: "TB_USERS");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TB_USERS",
                table: "TB_USERS");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TB_SALES",
                table: "TB_SALES");

            migrationBuilder.RenameTable(
                name: "TB_USERS",
                newName: "TB_USER");

            migrationBuilder.RenameTable(
                name: "TB_SALES",
                newName: "TB_SALE");

            migrationBuilder.RenameColumn(
                name: "COL_ROLE_ID",
                table: "TB_USER_ROLE",
                newName: "FK_ROLE_ID");

            migrationBuilder.RenameColumn(
                name: "COL_USER_ID",
                table: "TB_USER_ROLE",
                newName: "FK_USER_ID");

            migrationBuilder.RenameIndex(
                name: "IX_TB_USER_ROLE_COL_ROLE_ID",
                table: "TB_USER_ROLE",
                newName: "IX_TB_USER_ROLE_FK_ROLE_ID");

            migrationBuilder.RenameColumn(
                name: "COL_USER_ID",
                table: "TB_TENANT_USER",
                newName: "FK_USER_ID");

            migrationBuilder.RenameColumn(
                name: "COL_TENANT_ID",
                table: "TB_TENANT_USER",
                newName: "FK_TENANT_ID");

            migrationBuilder.RenameIndex(
                name: "IX_TB_TENANT_USER_COL_USER_ID",
                table: "TB_TENANT_USER",
                newName: "IX_TB_TENANT_USER_FK_USER_ID");

            migrationBuilder.RenameIndex(
                name: "IX_TB_USERS_COL_NORMALIZED_LASTNAME",
                table: "TB_USER",
                newName: "IX_TB_USER_COL_NORMALIZED_LASTNAME");

            migrationBuilder.RenameIndex(
                name: "IX_TB_USERS_COL_NORMALIZED_FIRSTNAME",
                table: "TB_USER",
                newName: "IX_TB_USER_COL_NORMALIZED_FIRSTNAME");

            migrationBuilder.RenameIndex(
                name: "IX_TB_USERS_COL_NORMALIZED_EMAIL",
                table: "TB_USER",
                newName: "IX_TB_USER_COL_NORMALIZED_EMAIL");

            migrationBuilder.RenameIndex(
                name: "IX_TB_USERS_COL_COMPLETE_NAME",
                table: "TB_USER",
                newName: "IX_TB_USER_COL_COMPLETE_NAME");

            migrationBuilder.RenameIndex(
                name: "IX_TB_SALES_FK_USER_ID",
                table: "TB_SALE",
                newName: "IX_TB_SALE_FK_USER_ID");

            migrationBuilder.RenameIndex(
                name: "IX_TB_SALES_FK_TENANT_ID",
                table: "TB_SALE",
                newName: "IX_TB_SALE_FK_TENANT_ID");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_TB_USER_COL_EMAIL",
                table: "TB_USER",
                column: "COL_EMAIL");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TB_USER",
                table: "TB_USER",
                column: "COL_ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TB_SALE",
                table: "TB_SALE",
                column: "COL_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_TB_SALE_TB_TENANT_FK_TENANT_ID",
                table: "TB_SALE",
                column: "FK_TENANT_ID",
                principalTable: "TB_TENANT",
                principalColumn: "COL_ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TB_SALE_TB_USER_FK_USER_ID",
                table: "TB_SALE",
                column: "FK_USER_ID",
                principalTable: "TB_USER",
                principalColumn: "COL_ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TB_SALE_ITEM_TB_SALE_FK_SALE_ID",
                table: "TB_SALE_ITEM",
                column: "FK_SALE_ID",
                principalTable: "TB_SALE",
                principalColumn: "COL_ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TB_TENANT_USER_TB_TENANT_FK_TENANT_ID",
                table: "TB_TENANT_USER",
                column: "FK_TENANT_ID",
                principalTable: "TB_TENANT",
                principalColumn: "COL_ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TB_TENANT_USER_TB_USER_FK_USER_ID",
                table: "TB_TENANT_USER",
                column: "FK_USER_ID",
                principalTable: "TB_USER",
                principalColumn: "COL_ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TB_USER_ROLE_TB_ROLE_FK_ROLE_ID",
                table: "TB_USER_ROLE",
                column: "FK_ROLE_ID",
                principalTable: "TB_ROLE",
                principalColumn: "COL_ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TB_USER_ROLE_TB_USER_FK_USER_ID",
                table: "TB_USER_ROLE",
                column: "FK_USER_ID",
                principalTable: "TB_USER",
                principalColumn: "COL_ID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TB_SALE_TB_TENANT_FK_TENANT_ID",
                table: "TB_SALE");

            migrationBuilder.DropForeignKey(
                name: "FK_TB_SALE_TB_USER_FK_USER_ID",
                table: "TB_SALE");

            migrationBuilder.DropForeignKey(
                name: "FK_TB_SALE_ITEM_TB_SALE_FK_SALE_ID",
                table: "TB_SALE_ITEM");

            migrationBuilder.DropForeignKey(
                name: "FK_TB_TENANT_USER_TB_TENANT_FK_TENANT_ID",
                table: "TB_TENANT_USER");

            migrationBuilder.DropForeignKey(
                name: "FK_TB_TENANT_USER_TB_USER_FK_USER_ID",
                table: "TB_TENANT_USER");

            migrationBuilder.DropForeignKey(
                name: "FK_TB_USER_ROLE_TB_ROLE_FK_ROLE_ID",
                table: "TB_USER_ROLE");

            migrationBuilder.DropForeignKey(
                name: "FK_TB_USER_ROLE_TB_USER_FK_USER_ID",
                table: "TB_USER_ROLE");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_TB_USER_COL_EMAIL",
                table: "TB_USER");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TB_USER",
                table: "TB_USER");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TB_SALE",
                table: "TB_SALE");

            migrationBuilder.RenameTable(
                name: "TB_USER",
                newName: "TB_USERS");

            migrationBuilder.RenameTable(
                name: "TB_SALE",
                newName: "TB_SALES");

            migrationBuilder.RenameColumn(
                name: "FK_ROLE_ID",
                table: "TB_USER_ROLE",
                newName: "COL_ROLE_ID");

            migrationBuilder.RenameColumn(
                name: "FK_USER_ID",
                table: "TB_USER_ROLE",
                newName: "COL_USER_ID");

            migrationBuilder.RenameIndex(
                name: "IX_TB_USER_ROLE_FK_ROLE_ID",
                table: "TB_USER_ROLE",
                newName: "IX_TB_USER_ROLE_COL_ROLE_ID");

            migrationBuilder.RenameColumn(
                name: "FK_USER_ID",
                table: "TB_TENANT_USER",
                newName: "COL_USER_ID");

            migrationBuilder.RenameColumn(
                name: "FK_TENANT_ID",
                table: "TB_TENANT_USER",
                newName: "COL_TENANT_ID");

            migrationBuilder.RenameIndex(
                name: "IX_TB_TENANT_USER_FK_USER_ID",
                table: "TB_TENANT_USER",
                newName: "IX_TB_TENANT_USER_COL_USER_ID");

            migrationBuilder.RenameIndex(
                name: "IX_TB_USER_COL_NORMALIZED_LASTNAME",
                table: "TB_USERS",
                newName: "IX_TB_USERS_COL_NORMALIZED_LASTNAME");

            migrationBuilder.RenameIndex(
                name: "IX_TB_USER_COL_NORMALIZED_FIRSTNAME",
                table: "TB_USERS",
                newName: "IX_TB_USERS_COL_NORMALIZED_FIRSTNAME");

            migrationBuilder.RenameIndex(
                name: "IX_TB_USER_COL_NORMALIZED_EMAIL",
                table: "TB_USERS",
                newName: "IX_TB_USERS_COL_NORMALIZED_EMAIL");

            migrationBuilder.RenameIndex(
                name: "IX_TB_USER_COL_COMPLETE_NAME",
                table: "TB_USERS",
                newName: "IX_TB_USERS_COL_COMPLETE_NAME");

            migrationBuilder.RenameIndex(
                name: "IX_TB_SALE_FK_USER_ID",
                table: "TB_SALES",
                newName: "IX_TB_SALES_FK_USER_ID");

            migrationBuilder.RenameIndex(
                name: "IX_TB_SALE_FK_TENANT_ID",
                table: "TB_SALES",
                newName: "IX_TB_SALES_FK_TENANT_ID");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_TB_USERS_COL_EMAIL",
                table: "TB_USERS",
                column: "COL_EMAIL");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TB_USERS",
                table: "TB_USERS",
                column: "COL_ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TB_SALES",
                table: "TB_SALES",
                column: "COL_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_TB_SALE_ITEM_TB_SALES_FK_SALE_ID",
                table: "TB_SALE_ITEM",
                column: "FK_SALE_ID",
                principalTable: "TB_SALES",
                principalColumn: "COL_ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TB_SALES_TB_TENANT_FK_TENANT_ID",
                table: "TB_SALES",
                column: "FK_TENANT_ID",
                principalTable: "TB_TENANT",
                principalColumn: "COL_ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TB_SALES_TB_USERS_FK_USER_ID",
                table: "TB_SALES",
                column: "FK_USER_ID",
                principalTable: "TB_USERS",
                principalColumn: "COL_ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TB_TENANT_USER_TB_TENANT_COL_TENANT_ID",
                table: "TB_TENANT_USER",
                column: "COL_TENANT_ID",
                principalTable: "TB_TENANT",
                principalColumn: "COL_ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TB_TENANT_USER_TB_USERS_COL_USER_ID",
                table: "TB_TENANT_USER",
                column: "COL_USER_ID",
                principalTable: "TB_USERS",
                principalColumn: "COL_ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TB_USER_ROLE_TB_ROLE_COL_ROLE_ID",
                table: "TB_USER_ROLE",
                column: "COL_ROLE_ID",
                principalTable: "TB_ROLE",
                principalColumn: "COL_ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TB_USER_ROLE_TB_USERS_COL_USER_ID",
                table: "TB_USER_ROLE",
                column: "COL_USER_ID",
                principalTable: "TB_USERS",
                principalColumn: "COL_ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
