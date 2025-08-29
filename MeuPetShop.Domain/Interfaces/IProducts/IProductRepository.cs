using MeuPetShop.Domain.Entities;

namespace MeuPetShop.Domain.Interfaces.IProducts;

public interface IProductRepository
{
    Task AddAsync(Product product);
    Task <Product?> GetByIdAsync(int id);
    Task<IEnumerable<Product>> GetAllAsync();
    Task<IEnumerable<Product>> GetByStockQuantityAsync(int stockQuantity);
    Task<Product?> GetByNameAsync(string name);
    Task UpdateAsync(Product product);
    Task DeleteAsync(Product product);
    Task<IEnumerable<Product>> GetAllPagedAsync(int pageNumber, int pageSize);
    Task<int> CountAsync();
}