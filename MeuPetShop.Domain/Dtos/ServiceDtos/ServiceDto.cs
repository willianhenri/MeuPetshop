namespace MeuPetShop.Domain.Dtos.ServiceDtos;

public record ServiceDto( int Id,
    string Name,
    string Description,
    decimal Price,
    int DurationInMinutes);