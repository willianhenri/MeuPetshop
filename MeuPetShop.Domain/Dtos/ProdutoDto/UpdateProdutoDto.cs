namespace MeuPetshop.Domain.Dtos.ProdutoDto;

public record UpdateProdutoDto(string Name, string Description, decimal Price, int StockQuantity);