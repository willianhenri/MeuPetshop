using MeuPetShop.Domain.Dtos.ClientDtos;
using MeuPetShop.Domain.Interfaces.IClients;
using MeuPetShop.Domain.Interfaces.IProducts;
using Microsoft.AspNetCore.Mvc;

namespace MeuPetshop.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientController : ControllerBase
{
    private readonly IClientService _clientService;

    public ClientController(IClientService clientService)
    {
        _clientService = clientService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ClientDto>>> GetAllClients()
    {
        var clients = await _clientService.GetAllClientsAsync();
        return Ok(clients);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ClientDto>> GetClientById(int id)
    {
        var client = await _clientService.GetClientByIdAsync(id);
        if (client == null) return NotFound("Client not found");
        return Ok(client);
    }

    [HttpPost]
    public async Task<ActionResult<ClientDto>> CreateClient([FromBody] CreateClientDto clientDto)
    {
        try
        {
            var newClient = await _clientService.CreateClientAsync(clientDto);
            return CreatedAtAction(nameof(GetClientById), new { id = newClient.Id }, newClient);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(ex.Message);
        }
    }

    [HttpPut]
    public async Task<ActionResult<ClientDto>> UpdateClient(int id, [FromBody] UpdateClientDto clientDto)
    {
        try
        {
            var updatedClient = await _clientService.UpdateClientAsync(id, clientDto);
            if(updatedClient == null) return NotFound("Client not found");
            return Ok(updatedClient);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(ex.Message);
        }
    }

    [HttpDelete]
    public async Task<ActionResult<ClientDto>> DeleteClient(int id)
    {
        var success = await _clientService.DeleteClientAsync(id);
        if (!success) return NotFound("Client not found");
        return NoContent();
    }
}