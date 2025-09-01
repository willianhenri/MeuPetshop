namespace MeuPetShop.Domain.Dtos.Auth;

public record LoginDto(
    string UserName,
    string Password
);