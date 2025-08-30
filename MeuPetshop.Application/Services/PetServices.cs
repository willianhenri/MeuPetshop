using MeuPetShop.Domain.Dtos.PetDtos;
using MeuPetShop.Domain.Entities;
using MeuPetShop.Domain.Interfaces.IPets;

namespace MeuPetshop.Application.Services;

public class PetServices : IPetService
{
    private readonly IPetRepository _petRepository;

    public PetServices(IPetRepository petRepository)
    {
        _petRepository = petRepository;
    }
    public async Task<PetDto> CreatePetForClientAsync(int clientId, CreatePetDto petDto)
    {
        var client = await _petRepository.GetByIdAsync(clientId);
        if (client == null)
            throw new InvalidOperationException(
                $"Cliente com ID {clientId} não encontrado. Não é possível criar o pet.");
        var newPet = new Pet
        {
            Name = petDto.Name,
            Breed = petDto.Breed,
            ClientId = clientId,
        };
        
        await _petRepository.AddAsync(newPet);
        return MapPetToDto(newPet);
    }

    public async Task<PetDto?> GetPetByIdAsync(int id)
    {
        var pet = await _petRepository.GetByIdAsync(id);
        return pet == null ? null : MapPetToDto(pet);
    }

    public async Task<IEnumerable<PetDto>> GetAllPetsForClientAsync(int clientId)
    {
        var client = await _petRepository.GetByIdAsync(clientId);
        if (client == null)
            throw new InvalidOperationException($"Cliente com ID {clientId} não encontrado.");
        var pets = await _petRepository.GetAllByClientIdAsync(clientId);
        return pets.Select(MapPetToDto);
    }

    public async Task<PetDto?> UpdatePetAsync(int id, UpdatePetDto petDto)
    {
        var existingPet = await _petRepository.GetByIdAsync(id);
        if (existingPet == null) return null;
        
        existingPet.Name = petDto.Name;
        existingPet.Breed = petDto.Breed;
        
        await _petRepository.UpdateAsync(existingPet);
        return MapPetToDto(existingPet);
    }

    public async Task<bool> DeletePetAsync(int id)
    {
        var pet = await _petRepository.GetByIdAsync(id);
        if (pet == null) return false;
        await _petRepository.DeleteAsync(pet);
        return true;
    }
    
    private PetDto MapPetToDto(Pet pet)
    {
        return new PetDto(pet.Id, pet.Name, pet.Breed, pet.ClientId);
    }
}