using MeuPetShop.Domain.Entities;
using MeuPetShop.Domain.Interfaces;

namespace MeuPetshop.Infrastructure.Repositories;

public class ProdutoRepository : IProdutoRepository
{
    public Task AddAsync(Produto product)
    {
        throw new NotImplementedException();
    }

    public Task<Produto?> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Produto>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Produto>> GetByStockQuantityAsync(int stockQuantity)
    {
        throw new NotImplementedException();
    }

    public Task<Produto?> GetByNameAsync(string name)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Produto product)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Produto product)
    {
        throw new NotImplementedException();
    }
}