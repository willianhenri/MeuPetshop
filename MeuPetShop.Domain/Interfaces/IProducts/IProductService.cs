
using MeuPetshop.Domain.Dtos;
using MeuPetshop.Domain.Dtos.ProductDtos;
using MeuPetShop.Domain.Shared;

namespace MeuPetShop.Domain.Interfaces.IProducts;

public interface IProductService
{
    Task<ProductDto> CreateProductAsync(CreateProductDto productDto);
    Task<ProductDto?> GetProductByIdAsync(int id);
    Task<PagedApiResponse<ProductDto>> GetAllProductsAsync(int pageNumber, int pageSize);
    Task<ProductDto?> UpdateProductAsync(int id, UpdateProductDto productDto);
    Task<bool> DeleteProductAsync(int id);
}