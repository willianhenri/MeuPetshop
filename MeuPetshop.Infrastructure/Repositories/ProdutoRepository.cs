using MeuPetShop.Domain.Entities;
using MeuPetShop.Domain.Interfaces;
using MeuPetshop.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MeuPetshop.Infrastructure.Repositories;

public class ProdutoRepository : IProdutoRepository
{
    private readonly AppDbContext _context;

    public ProdutoRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task AddAsync(Produto product)
    {
        await _context.Produtos.AddAsync(product);
        await _context.SaveChangesAsync();
    }

    public async Task<Produto?> GetByIdAsync(int id)
    {
        return await _context.Produtos.FindAsync(id);
    }

    public async Task<IEnumerable<Produto>> GetAllAsync()
    {
        return await _context.Produtos.ToListAsync();
    }

    public async Task<IEnumerable<Produto>> GetByStockQuantityAsync(int stockQuantity)
    {
        return await _context.Produtos
            .Where(p => p.StockQuantity == stockQuantity)
            .ToListAsync();
    }

    public async Task<Produto?> GetByNameAsync(string name)
    {
        return await _context.Produtos.FirstOrDefaultAsync(p => p.Name == name);
    }

    public async Task UpdateAsync(Produto product)
    {
        _context.Produtos.Update(product);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Produto product)
    {
        _context.Produtos.Remove(product);
        await _context.SaveChangesAsync();
    }
}