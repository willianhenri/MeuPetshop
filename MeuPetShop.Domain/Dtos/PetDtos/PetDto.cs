namespace MeuPetShop.Domain.Dtos.PetDtos;

public record PetDto(int Id, string Name, string Breed, string specie, int ClientId);