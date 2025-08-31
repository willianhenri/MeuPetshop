using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeuPetshop.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSpecieColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Specie",
                table: "Pets",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Specie",
                table: "Pets");
        }
    }
}
