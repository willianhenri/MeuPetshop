namespace MeuPetShop.Domain.Dtos.ServiceDtos;

public record CreateServiceDto( string Name,
    string Description,
    decimal Price,
    int DurationInMinutes);