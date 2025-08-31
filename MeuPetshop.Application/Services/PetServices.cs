using MeuPetShop.Domain.Dtos.ClientDtos;
using MeuPetShop.Domain.Dtos.PetDtos;
using MeuPetShop.Domain.Entities;
using MeuPetShop.Domain.Interfaces.IClients;
using MeuPetShop.Domain.Interfaces.IPets;
using MeuPetShop.Domain.Shared;

namespace MeuPetshop.Application.Services;

public class PetServices : IPetService
{
    private readonly IPetRepository _petRepository;
    private readonly IClientRepository _clientRepository;

    public PetServices(IPetRepository petRepository, IClientRepository clientRepository)
    {
        _petRepository = petRepository;
        _clientRepository = clientRepository;
    }
    
    public async Task<PagedApiResponse<PetDto>> GetAllPetsAsync(int pageNumber, int pageSize)
    {
        var (pets, totalCount) = await _petRepository.GetAllAsync(pageNumber, pageSize);

        var petDtos = pets.Select(p => new PetDto(p.Id, p.Name, p.Breed, p.Specie, p.ClientId ));

        var response = new PagedApiResponse<PetDto>
        {
            Data = petDtos,
            Pagination = new PaginationData
            {
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
            }
        };

        return response;
    }
    public async Task<PetDto> CreatePetForClientAsync(int clientId, CreatePetDto petDto)
    {
        var client = await _clientRepository.GetByIdAsync(clientId);
        if (client == null)
        {
            throw new InvalidOperationException($"Cliente com ID {clientId} não encontrado. Não é possível criar o pet.");
        }

        var newPet = new Pet
        {
            Name = petDto.Name,
            Breed = petDto.Breed,
            ClientId = clientId,
            Specie = petDto.Specie,
        };

        await _petRepository.AddAsync(newPet);

        return MapPetToDto(newPet);
    }

    public async Task<IEnumerable<PetDto>> GetAllPetsForClientAsync(int clientId)
    {
        var client = await _clientRepository.GetByIdAsync(clientId);
        if (client == null)
        {
            throw new InvalidOperationException($"Cliente com ID {clientId} não encontrado.");
        }

        var pets = await _petRepository.GetAllByClientIdAsync(clientId);
        return pets.Select(MapPetToDto);
    }

    public async Task<PetDto?> GetPetByIdAsync(int id)
    {
        var pet = await _petRepository.GetByIdAsync(id);
        return pet == null ? null : MapPetToDto(pet);
    }

    public async Task<PetDto?> UpdatePetAsync(int id, UpdatePetDto petDto)
    {
        var petToUpdate = await _petRepository.GetByIdAsync(id);
        if (petToUpdate == null)
        {
            return null;
        }

        petToUpdate.Name = petDto.Name;
        petToUpdate.Breed = petDto.Breed;

        await _petRepository.UpdateAsync(petToUpdate);

        return MapPetToDto(petToUpdate);
    }

    public async Task<bool> DeletePetAsync(int id)
    {
        var petToDelete = await _petRepository.GetByIdAsync(id);
        if (petToDelete == null)
        {
            return false;
        }

        await _petRepository.DeleteAsync(petToDelete);
        return true;
    }

    private PetDto MapPetToDto(Pet pet)
    {
        return new PetDto(pet.Id, pet.Name, pet.Breed, pet.Specie, pet.ClientId);
    }
    
}