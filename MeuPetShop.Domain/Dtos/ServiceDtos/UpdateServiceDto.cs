namespace MeuPetShop.Domain.Dtos.ServiceDtos;

public record UpdateServiceDto( string Name,
    string Description,
    decimal Price,
    int DurationInMinutes);