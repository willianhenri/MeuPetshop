using MeuPetShop.Domain.Dtos.PetDtos;

namespace MeuPetShop.Domain.Dtos.ClientDtos;

public record ClientDto( int Id,
    string Name,
    string Phone,
    string Email,
    string Address, 
    ICollection<PetDto> Pets );