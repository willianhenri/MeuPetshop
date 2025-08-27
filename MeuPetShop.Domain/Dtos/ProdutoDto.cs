namespace MeuPetshop.Domain.Dtos;

public record ProdutoDto(int Id, string Name, string Description, decimal Preco, int StockQuantity, DateTime DateAdded);