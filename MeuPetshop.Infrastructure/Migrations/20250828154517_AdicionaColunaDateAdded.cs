using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeuPetshop.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AdicionaColunaDateAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Quantidade",
                table: "Produtos",
                newName: "StockQuantity");

            migrationBuilder.RenameColumn(
                name: "Preco",
                table: "Produtos",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "Nome",
                table: "Produtos",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Descricao",
                table: "Produtos",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "DataCadastro",
                table: "Produtos",
                newName: "DateAdded");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StockQuantity",
                table: "Produtos",
                newName: "Quantidade");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Produtos",
                newName: "Preco");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Produtos",
                newName: "Nome");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Produtos",
                newName: "Descricao");

            migrationBuilder.RenameColumn(
                name: "DateAdded",
                table: "Produtos",
                newName: "DataCadastro");
        }
    }
}
