using MeuPetShop.Domain.Entities;

namespace MeuPetShop.Domain.Interfaces.IProdutos;

public interface IProdutoRepository
{
    Task AddAsync(Produto product);
    Task <Produto?> GetByIdAsync(int id);
    Task<IEnumerable<Produto>> GetAllAsync();
    Task<IEnumerable<Produto>> GetByStockQuantityAsync(int stockQuantity);
    Task<Produto?> GetByNameAsync(string name);
    Task UpdateAsync(Produto product);
    Task DeleteAsync(Produto product);
    Task<IEnumerable<Produto>> GetAllPagedAsync(int pageNumber, int pageSize);
    Task<int> CountAsync();
}