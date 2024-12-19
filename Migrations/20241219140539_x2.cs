using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Marknadsplatsen.Migrations
{
    /// <inheritdoc />
    public partial class x2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Listings",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Listings_CategoryId",
                table: "Listings",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Listings_Categories_CategoryId",
                table: "Listings",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Listings_Categories_CategoryId",
                table: "Listings");

            migrationBuilder.DropIndex(
                name: "IX_Listings_CategoryId",
                table: "Listings");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Listings");
        }
    }
}
