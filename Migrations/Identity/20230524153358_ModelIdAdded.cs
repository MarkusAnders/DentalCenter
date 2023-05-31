using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DentalCenter.Migrations.Identity
{
    /// <inheritdoc />
    public partial class ModelIdAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ModelId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ModelId",
                table: "AspNetUsers");
        }
    }
}
