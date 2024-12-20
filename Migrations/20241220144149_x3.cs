using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Marknadsplatsen.Migrations
{
    /// <inheritdoc />
    public partial class x3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Listings_AspNetUsers_OwnerId",
                table: "Listings");

            migrationBuilder.AlterColumn<string>(
                name: "OwnerId",
                table: "Listings",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddForeignKey(
                name: "FK_Listings_AspNetUsers_OwnerId",
                table: "Listings",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Listings_AspNetUsers_OwnerId",
                table: "Listings");

            migrationBuilder.AlterColumn<string>(
                name: "OwnerId",
                table: "Listings",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Listings_AspNetUsers_OwnerId",
                table: "Listings",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
