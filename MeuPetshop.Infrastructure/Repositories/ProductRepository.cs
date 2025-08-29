using MeuPetShop.Domain.Entities;
using MeuPetShop.Domain.Interfaces;
using MeuPetShop.Domain.Interfaces.IProducts;
using MeuPetshop.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MeuPetshop.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task AddAsync(Product product)
    {
        await _context.Produtos.AddAsync(product);
        await _context.SaveChangesAsync();
    }

    public async Task<Product?> GetByIdAsync(int id)
    {
        return await _context.Produtos.FindAsync(id);
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await _context.Produtos.ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetByStockQuantityAsync(int stockQuantity)
    {
        return await _context.Produtos
            .Where(p => p.StockQuantity == stockQuantity)
            .ToListAsync();
    }

    public async Task<Product?> GetByNameAsync(string name)
    {
        return await _context.Produtos.FirstOrDefaultAsync(p => p.Name == name);
    }

    public async Task UpdateAsync(Product product)
    {
        _context.Produtos.Update(product);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Product product)
    {
        _context.Produtos.Remove(product);
        await _context.SaveChangesAsync();
    }
    
    public async Task<int> CountAsync()
    {
        return await _context.Produtos.CountAsync();
    }

    public async Task<IEnumerable<Product>> GetAllPagedAsync(int pageNumber, int pageSize)
    {
        return await _context.Produtos
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }
    
}