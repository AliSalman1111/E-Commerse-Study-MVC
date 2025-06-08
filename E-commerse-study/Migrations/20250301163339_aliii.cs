using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_commerse_study.Migrations
{
    /// <inheritdoc />
    public partial class aliii : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_products_categories_CategoryId",
                table: "products");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "products");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "products",
                newName: "categoryId");

            migrationBuilder.RenameIndex(
                name: "IX_products_CategoryId",
                table: "products",
                newName: "IX_products_categoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_products_categories_categoryId",
                table: "products",
                column: "categoryId",
                principalTable: "categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_products_categories_categoryId",
                table: "products");

            migrationBuilder.RenameColumn(
                name: "categoryId",
                table: "products",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_products_categoryId",
                table: "products",
                newName: "IX_products_CategoryId");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_products_categories_CategoryId",
                table: "products",
                column: "CategoryId",
                principalTable: "categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
