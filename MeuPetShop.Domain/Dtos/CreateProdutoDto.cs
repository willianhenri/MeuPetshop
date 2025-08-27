namespace MeuPetshop.Domain.Dtos;

public record CreateProdutoDto(string Name, string Description, decimal Price, int StockQuantity);