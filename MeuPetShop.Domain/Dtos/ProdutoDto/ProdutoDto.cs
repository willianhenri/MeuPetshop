using MeuPetShop.Domain.Entities;

namespace MeuPetshop.Domain.Dtos.ProdutoDto;

public record ProdutoDto(int Id, string Name, string Description, decimal Preco, int StockQuantity, DateTime DateAdded)
{
    private static readonly TimeZoneInfo _fusoHorarioBrasil = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");
    
    public ProdutoDto(Produto produto) : this(
        produto.Id,
        produto.Name,
        produto.Description,
        produto.Price,
        produto.StockQuantity,
        TimeZoneInfo.ConvertTimeFromUtc(produto.DateAdded, _fusoHorarioBrasil)
    ){}
}