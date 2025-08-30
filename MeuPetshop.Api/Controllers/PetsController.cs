using MeuPetShop.Domain.Dtos.ClientDtos;
using MeuPetShop.Domain.Dtos.PetDtos;
using MeuPetShop.Domain.Interfaces.IPets;
using Microsoft.AspNetCore.Mvc;

namespace MeuPetshop.Api.Controllers;
[ApiController]
[Route("api/[controller]")]
public class PetsController : ControllerBase
{
    private readonly IPetService _petService;

    public PetsController(IPetService petService)
    {
        _petService = petService;
    }

    [HttpGet("/api/clients/{clientId}/pets")]
    public async Task<ActionResult<IEnumerable<ClientDto>>> GetAllClientsForPet(int clientId)
    {
        try
        {
            var pets = await _petService.GetAllPetsForClientAsync(clientId);
            return Ok(pets);
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPost("/api/clients/{clientId}/pets")]
    public async Task<ActionResult<PetDto>> CreatePetForClient(int clientId, [FromBody] CreatePetDto petDto)
    {
        try
        {
            var newPet = await _petService.CreatePetForClientAsync(clientId, petDto);
            return CreatedAtAction(nameof(GetPetById), new { clientId = clientId, petId = newPet.Id }, newPet);
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PetDto>> GetPetById(int id)
    {
        var pet = await _petService.GetPetByIdAsync(id);
        if (pet == null) return NotFound();
        return Ok(pet);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePet(int id, [FromBody] UpdatePetDto petDto)
    {
        var petUpdate = await _petService.UpdatePetAsync(id, petDto);
        if (petUpdate == null) return NotFound();
        return Ok(petUpdate);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePet(int id)
    {
        var success = await _petService.DeletePetAsync(id);
        if (!success) return NotFound("Pet not found");
        return NoContent();
    }
}