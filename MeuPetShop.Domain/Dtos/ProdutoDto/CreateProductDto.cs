namespace MeuPetshop.Domain.Dtos.ProdutoDto;

public record CreateProductDto(string Name, string Description, decimal Price, int StockQuantity);