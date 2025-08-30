using MeuPetShop.Domain.Entities;

namespace MeuPetShop.Domain.Interfaces.IPets;

public interface IPetRepository
{
    Task AddAsync(Pet pet);
    Task<Pet?> GetByIdAsync(int id);
    Task<IEnumerable<Pet>> GetAllByClientIdAsync(int clientId);
    Task UpdateAsync(Pet pet);
    Task DeleteAsync(Pet pet);
}