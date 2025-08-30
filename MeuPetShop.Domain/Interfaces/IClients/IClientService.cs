using MeuPetShop.Domain.Dtos.ClientDtos;

namespace MeuPetShop.Domain.Interfaces.IClients;

public interface IClientService
{
    Task<ClientDto> CreateClientAsync(CreateClientDto clientDto);
    Task<ClientDto?> GetClientByIdAsync(int id);
    Task<IEnumerable<ClientDto>> GetAllClientsAsync();
    Task<ClientDto?> UpdateClientAsync(int id, UpdateClientDto clientDto);
    Task<bool> DeleteClientAsync(int id);
}