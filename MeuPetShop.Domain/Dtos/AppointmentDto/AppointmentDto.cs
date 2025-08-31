using MeuPetShop.Domain.Dtos.ClientDtos;
using MeuPetShop.Domain.Dtos.PetDtos;
using MeuPetShop.Domain.Dtos.ServiceDtos;
using MeuPetShop.Domain.Entities;

namespace MeuPetShop.Domain.Dtos.AppointmentDto;

public record AppointmentDto(
    int Id,
    DateTime AppointmentDateTime,
    AppointmentStatus Status,
    string? Notes,
    ClientDto Client,
    PetDto Pet,
    ServiceDto Service);