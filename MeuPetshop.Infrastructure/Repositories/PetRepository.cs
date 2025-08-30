using MeuPetShop.Domain.Entities;
using MeuPetShop.Domain.Interfaces.IPets;
using MeuPetshop.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MeuPetshop.Infrastructure.Repositories;

public class PetRepository : IPetRepository
{
    private readonly AppDbContext _context;

    public PetRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task AddAsync(Pet pet)
    {
        await _context.Pets.AddAsync(pet);
        await _context.SaveChangesAsync();
    }

    public async Task<Pet?> GetByIdAsync(int id)
    {
        return await _context.Pets.FindAsync(id);
    }

    public async Task<IEnumerable<Pet>> GetAllByClientIdAsync(int clientId)
    {
        return await _context.Pets
            .Where(p => p.ClientId == clientId)
            .ToListAsync();
    }

    public async Task UpdateAsync(Pet pet)
    {
        _context.Pets.Update(pet);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Pet pet)
    {
        _context.Pets.Remove(pet);
        await _context.SaveChangesAsync();
    }
}