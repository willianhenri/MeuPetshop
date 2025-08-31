namespace MeuPetShop.Domain.Dtos.AppointmentDto;

public record CreateAppointmentDto(
    int ClientId,
    int PetId,
    int ServiceId,
    DateTime AppointmentDateTime,
    string? Notes);