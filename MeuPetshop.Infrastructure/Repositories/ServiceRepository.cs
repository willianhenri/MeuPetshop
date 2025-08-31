using MeuPetShop.Domain.Entities;
using MeuPetShop.Domain.Interfaces.IService;
using MeuPetshop.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MeuPetshop.Infrastructure.Repositories;

public class ServiceRepository : IServiceRepository
{
    private readonly AppDbContext _context;

    public ServiceRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task AddAsync(Service service)
    {
        await _context.Services.AddAsync(service);
        await _context.SaveChangesAsync();
    }

    public async Task<Service?> GetByIdAsync(int id)
    {
        return await _context.Services.FindAsync(id);
        
    }

    public async Task<IEnumerable<Service>> GetAllAsync()
    {
        return await _context.Services.ToListAsync();
    }

    public async Task UpdateAsync(Service service)
    {
        _context.Update(service);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Service service)
    {
        _context.Services.Remove(service);
        await _context.SaveChangesAsync();
    }
}