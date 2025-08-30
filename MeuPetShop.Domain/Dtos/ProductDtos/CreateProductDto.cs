namespace MeuPetshop.Domain.Dtos.ProductDtos;

public record CreateProductDto(string Name, string Description, decimal Price, int StockQuantity);