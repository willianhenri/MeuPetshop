using MeuPetShop.Domain.Entities;

namespace MeuPetshop.Domain.Dtos.ProductDtos;

public record ProductDto(int Id, string Name, string Description, decimal Preco, int StockQuantity, DateTime DateAdded)
{
    private static readonly TimeZoneInfo _fusoHorarioBrasil = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");
    
    public ProductDto(Product product) : this(
        product.Id,
        product.Name,
        product.Description,
        product.Price,
        product.StockQuantity,
        TimeZoneInfo.ConvertTimeFromUtc(product.DateAdded, _fusoHorarioBrasil)
    ){}
}