using MeuPetshop.Domain.Dtos;
using MeuPetshop.Domain.Dtos.ProdutoDto;
using MeuPetShop.Domain.Entities;
using MeuPetShop.Domain.Interfaces;
using MeuPetShop.Domain.Interfaces.IProdutos;
using MeuPetShop.Domain.Shared;

namespace MeuPetshop.Application.Services;

public class ProdutoServices : IProdutoService
{
    private readonly IProdutoRepository _produtoRepository;

    public ProdutoServices(IProdutoRepository produtoRepository)
    {
        _produtoRepository = produtoRepository;
    }
    
    public async Task<ProdutoDto> CreateProductAsync(CreateProdutoDto produtoDto)
    {
        if(produtoDto == null) throw new ArgumentNullException(nameof(produtoDto));
        if (string.IsNullOrWhiteSpace(produtoDto.Name)) throw new ArgumentException("O nome do produto não pode ser vazio");
        
        var existingProduct = await _produtoRepository.GetByNameAsync(produtoDto.Name);
        if(existingProduct != null) throw new InvalidOperationException($"Produto {produtoDto.Name} já existe");

        var newProduct = new Produto
        {
            Name = produtoDto.Name,
            Description = produtoDto.Description,
            Price = produtoDto.Price,
            StockQuantity = produtoDto.StockQuantity,
            DateAdded = DateTime.UtcNow
        };
        
        await _produtoRepository.AddAsync(newProduct);
        
        return new ProdutoDto(newProduct);
    }

    public async Task<ProdutoDto?> GetProductByIdAsync(int id)
    {
        var product = await _produtoRepository.GetByIdAsync(id);
        if(product == null) return null;
        return new ProdutoDto(product.Id, product.Name, product.Description, product.Price, product.StockQuantity, product.DateAdded);
    }

    public async Task<PagedApiResponse<ProdutoDto>> GetAllProductsAsync(int pageNumber, int pageSize)
    {
        var totalCount = await _produtoRepository.CountAsync();
        var products = await _produtoRepository.GetAllPagedAsync(pageNumber, pageSize);
        
        var productDtos = products.Select(p => new ProdutoDto(p.Id, p.Name, p.Description, p.Price, p.StockQuantity, p.DateAdded));
        var response = new PagedApiResponse<ProdutoDto>
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

    public async Task<ProdutoDto?> UpdateProductAsync(int id, UpdateProdutoDto productDto)
    {
        if(productDto == null) throw new ArgumentNullException(nameof(productDto));
        
        var productToUpdate = await _produtoRepository.GetByIdAsync(id);
        if(productToUpdate == null) return null;
        
        productToUpdate.Name = productDto.Name;
        productToUpdate.Description = productDto.Description;
        productToUpdate.Price = productDto.Price;
        productToUpdate.StockQuantity = productDto.StockQuantity;
        
        await _produtoRepository.UpdateAsync(productToUpdate);
        return new ProdutoDto(productToUpdate.Id, productToUpdate.Name, productToUpdate.Description, productToUpdate.Price, productToUpdate.StockQuantity, productToUpdate.DateAdded);
        
        
    }

    public async Task<bool> DeleteProductAsync(int id)
    {
        var productToDelete = await _produtoRepository.GetByIdAsync(id);
        if(productToDelete == null) return false;
        await _produtoRepository.DeleteAsync(productToDelete);
        return true;
    }
}