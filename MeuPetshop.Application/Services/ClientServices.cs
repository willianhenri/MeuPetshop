using MeuPetShop.Domain.Dtos.ClientDtos;
using MeuPetShop.Domain.Dtos.PetDtos;
using MeuPetShop.Domain.Entities;
using MeuPetShop.Domain.Interfaces.IClients;

namespace MeuPetshop.Application.Services;

public class ClientServices : IClientService
{
    private readonly IClientRepository _clientRepository;

    public ClientServices(IClientRepository clientRepository)
    {
        _clientRepository = clientRepository;
    }
    public async Task<ClientDto> CreateClientAsync(CreateClientDto clientDto)
    {
        var existingClient = await _clientRepository.GetByEmailAsync(clientDto.Email);
        if (existingClient != null)
        {
            throw new InvalidOperationException("Já existe um cliente cadastrado com este e-mail.");
        }

        var newClient = new Client
        {
            Name = clientDto.Name,
            Email = clientDto.Email,
            Phone = clientDto.Phone,
            Address = clientDto.Adress
        };
        
        await _clientRepository.AddAsync(newClient);
        return MapClientToDto(newClient);
    }

    public async Task<ClientDto?> GetClientByIdAsync(int id)
    {
        var client = await _clientRepository.GetByIdAsync(id);
        if (client == null) throw new KeyNotFoundException($"Client {id} not found.");
        
        return MapClientToDto(client);
    }

    public async Task<IEnumerable<ClientDto>> GetAllClientsAsync()
    {
        var clients = await _clientRepository.GetAllAsync();
        return clients.Select(MapClientToDto);
    }

    public async Task<ClientDto?> UpdateClientAsync(int id, UpdateClientDto clientDto)
    {
        var clientToUpdate = await _clientRepository.GetByIdAsync(id);
        if (clientToUpdate == null) throw new KeyNotFoundException($"Client {id} not found.");
        
        var existingClientWithEmail = await _clientRepository.GetByEmailAsync(clientDto.Email);
        if (existingClientWithEmail != null && existingClientWithEmail.Id != id)
            throw new InvalidOperationException("O email informado ja está em uso");
        clientToUpdate.Name = clientDto.Name;
        clientToUpdate.Email = clientDto.Email;
        clientToUpdate.Phone = clientDto.Phone;
        clientToUpdate.Address = clientDto.Adress;
        await _clientRepository.UpdateAsync(clientToUpdate);
        return MapClientToDto(clientToUpdate);
    }

    public async Task<bool> DeleteClientAsync(int id)
    {
        var clientToDelete = await _clientRepository.GetByIdAsync(id);
        if (clientToDelete == null) throw new KeyNotFoundException($"Client {id} not found.");
        await _clientRepository.DeleteAsync(clientToDelete );
        return true;
    }
    
    
    private ClientDto MapClientToDto(Client client)
    {
        var petsDto = client.Pets?.Select(pet => new PetDto(pet.Id, pet.Name, pet.Breed, pet.ClientId)).ToList() 
                      ?? new List<PetDto>();

        return new ClientDto(client.Id, client.Name, client.Phone, client.Email, client.Address, petsDto);
    }
}