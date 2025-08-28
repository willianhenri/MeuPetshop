
using MeuPetshop.Domain.Dtos;
using MeuPetshop.Domain.Dtos.ProdutoDto;
using MeuPetShop.Domain.Shared;

namespace MeuPetShop.Domain.Interfaces.IProdutos;

public interface IProdutoService
{
    Task<ProdutoDto> CreateProductAsync(CreateProdutoDto productDto);
    Task<ProdutoDto?> GetProductByIdAsync(int id);
    Task<PagedApiResponse<ProdutoDto>> GetAllProductsAsync(int pageNumber, int pageSize);
    Task<ProdutoDto?> UpdateProductAsync(int id, UpdateProdutoDto productDto);
    Task<bool> DeleteProductAsync(int id);
}