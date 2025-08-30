namespace MeuPetshop.Domain.Dtos.ProductDtos;

public record UpdateProductDto(string Name, string Description, decimal Price, int StockQuantity);