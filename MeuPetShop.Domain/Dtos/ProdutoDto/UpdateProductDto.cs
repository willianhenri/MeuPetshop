namespace MeuPetshop.Domain.Dtos.ProdutoDto;

public record UpdateProductDto(string Name, string Description, decimal Price, int StockQuantity);