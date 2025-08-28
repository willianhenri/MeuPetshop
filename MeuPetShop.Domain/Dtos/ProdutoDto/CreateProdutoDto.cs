namespace MeuPetshop.Domain.Dtos.ProdutoDto;

public record CreateProdutoDto(string Name, string Description, decimal Price, int StockQuantity);