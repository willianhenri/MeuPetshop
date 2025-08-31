using MeuPetShop.Domain.Entities;

namespace MeuPetShop.Domain.Dtos.AppointmentDto;

public record UpdateAppointmentDto(
    DateTime AppointmentDateTime,
    AppointmentStatus Status,
    string? Notes);