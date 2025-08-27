namespace MeuPetshop.Domain.Dtos;

public record UpdateProdutoDto(string Name, string Description, decimal Price, int StockQuantity);