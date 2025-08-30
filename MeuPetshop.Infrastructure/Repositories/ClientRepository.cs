using MeuPetShop.Domain.Entities;
using MeuPetShop.Domain.Interfaces.IClients;
using MeuPetshop.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MeuPetshop.Infrastructure.Repositories;

public class ClientRepository : IClientRepository
{
    private readonly AppDbContext _context;

    public ClientRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task AddAsync(Client client)
    {
        await _context.Clients.AddAsync(client);
        await _context.SaveChangesAsync();
    }

    public async Task<Client?> GetByIdAsync(int id)
    {
        return await _context.Clients.Include(c => c.Pets).FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<IEnumerable<Client>> GetAllAsync()
    {
        return await _context.Clients.Include(c => c.Pets).ToListAsync();
    }

    public async Task<Client?> GetByEmailAsync(string email)
    {
        return await _context.Clients.Include(c => c.Pets).FirstOrDefaultAsync(c => c.Email == email);
    }

    public async Task UpdateAsync(Client client)
    {
        _context.Clients.Update(client);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Client client)
    {
        _context.Clients.Remove(client);
        await _context.SaveChangesAsync();
    }
}