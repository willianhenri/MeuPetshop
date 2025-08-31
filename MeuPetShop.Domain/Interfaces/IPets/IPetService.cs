using MeuPetShop.Domain.Dtos.ClientDtos;
using MeuPetShop.Domain.Dtos.PetDtos;
using MeuPetShop.Domain.Shared;

namespace MeuPetShop.Domain.Interfaces.IPets;

public interface IPetService
{
    Task<PetDto> CreatePetForClientAsync(int clientId, CreatePetDto petDto);
    Task<PetDto?> GetPetByIdAsync(int id);
    Task<IEnumerable<PetDto>> GetAllPetsForClientAsync(int clientId);
    Task<PetDto?> UpdatePetAsync(int id, UpdatePetDto petDto);
    Task<bool> DeletePetAsync(int id);
    Task<PagedApiResponse<PetDto>> GetAllPetsAsync(int pageNumber, int pageSize);
}