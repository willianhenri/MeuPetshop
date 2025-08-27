
using MeuPetshop.Domain.Dtos;

namespace MeuPetShop.Domain.Interfaces;

public interface IProdutoService
{
    Task<ProdutoDto> CreateProductAsync(CreateProdutoDto productDto);
    Task<ProdutoDto?> GetProductByIdAsync(int id);
    Task<IEnumerable<ProdutoDto>> GetAllProductsAsync();
    Task<ProdutoDto?> UpdateProductAsync(int id, UpdateProdutoDto productDto);
    Task<bool> DeleteProductAsync(int id);
}