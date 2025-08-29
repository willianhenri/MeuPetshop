using MeuPetshop.Domain.Dtos;
using MeuPetshop.Domain.Dtos.ProdutoDto;
using MeuPetShop.Domain.Entities;
using MeuPetShop.Domain.Interfaces;
using MeuPetShop.Domain.Interfaces.IProducts;
using MeuPetShop.Domain.Shared;

namespace MeuPetshop.Application.Services;

public class ProductServices : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductServices(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    
    public async Task<ProductDto> CreateProductAsync(CreateProductDto productDto)
    {
        if(productDto == null) throw new ArgumentNullException(nameof(productDto));
        if (string.IsNullOrWhiteSpace(productDto.Name)) throw new ArgumentException("O nome do produto não pode ser vazio");
        
        var existingProduct = await _productRepository.GetByNameAsync(productDto.Name);
        if(existingProduct != null) throw new InvalidOperationException($"Produto {productDto.Name} já existe");

        var newProduct = new Product
        {
            Name = productDto.Name,
            Description = productDto.Description,
            Price = productDto.Price,
            StockQuantity = productDto.StockQuantity,
            DateAdded = DateTime.UtcNow
        };
        
        await _productRepository.AddAsync(newProduct);
        
        return new ProductDto(newProduct);
    }

    public async Task<ProductDto?> GetProductByIdAsync(int id)
    {
        var product = await _productRepository.GetByIdAsync(id);
        if(product == null) return null;
        return new ProductDto(product.Id, product.Name, product.Description, product.Price, product.StockQuantity, product.DateAdded);
    }

    public async Task<PagedApiResponse<ProductDto>> GetAllProductsAsync(int pageNumber, int pageSize)
    {
        var totalCount = await _productRepository.CountAsync();
        var products = await _productRepository.GetAllPagedAsync(pageNumber, pageSize);
        
        var productDtos = products.Select(p => new ProductDto(p.Id, p.Name, p.Description, p.Price, p.StockQuantity, p.DateAdded));
        var response = new PagedApiResponse<ProductDto>
        {
            Data = productDtos,
            Pagination = new PaginationData
            {
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
            }
        };
        return response;
    }

    public async Task<ProductDto?> UpdateProductAsync(int id, UpdateProductDto productDto)
    {
        if(productDto == null) throw new ArgumentNullException(nameof(productDto));
        
        var productToUpdate = await _productRepository.GetByIdAsync(id);
        if(productToUpdate == null) return null;
        
        productToUpdate.Name = productDto.Name;
        productToUpdate.Description = productDto.Description;
        productToUpdate.Price = productDto.Price;
        productToUpdate.StockQuantity = productDto.StockQuantity;
        
        await _productRepository.UpdateAsync(productToUpdate);
        return new ProductDto(productToUpdate.Id, productToUpdate.Name, productToUpdate.Description, productToUpdate.Price, productToUpdate.StockQuantity, productToUpdate.DateAdded);
        
        
    }

    public async Task<bool> DeleteProductAsync(int id)
    {
        var productToDelete = await _productRepository.GetByIdAsync(id);
        if(productToDelete == null) return false;
        await _productRepository.DeleteAsync(productToDelete);
        return true;
    }
}