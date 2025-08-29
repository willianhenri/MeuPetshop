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
        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();
    }

    public async Task<Product?> GetByIdAsync(int id)
    {
        return await _context.Products.FindAsync(id);
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await _context.Products.ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetByStockQuantityAsync(int stockQuantity)
    {
        return await _context.Products
            .Where(p => p.StockQuantity == stockQuantity)
            .ToListAsync();
    }

    public async Task<Product?> GetByNameAsync(string name)
    {
        return await _context.Products.FirstOrDefaultAsync(p => p.Name == name);
    }

    public async Task UpdateAsync(Product product)
    {
        _context.Products.Update(product);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Product product)
    {
        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
    }
    
    public async Task<int> CountAsync()
    {
        return await _context.Products.CountAsync();
    }

    public async Task<IEnumerable<Product>> GetAllPagedAsync(int pageNumber, int pageSize)
    {
        return await _context.Products
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }
    
}