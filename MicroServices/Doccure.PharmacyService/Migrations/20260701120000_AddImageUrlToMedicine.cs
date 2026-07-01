using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Doccure.PharmacyService.Migrations
{
    /// <inheritdoc />
    public partial class AddImageUrlToMedicine : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Medicines",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Medicines");
        }
    }
}
