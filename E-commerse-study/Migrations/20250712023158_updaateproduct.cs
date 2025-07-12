using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_commerse_study.Migrations
{
    /// <inheritdoc />
    public partial class updaateproduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "countaty",
                table: "products",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "countaty",
                table: "products");
        }
    }
}
