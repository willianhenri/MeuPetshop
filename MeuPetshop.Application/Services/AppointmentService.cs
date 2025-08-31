using MeuPetShop.Domain.Dtos.AppointmentDto;
using MeuPetShop.Domain.Dtos.ClientDtos;
using MeuPetShop.Domain.Dtos.PetDtos;
using MeuPetShop.Domain.Dtos.ServiceDtos;
using MeuPetShop.Domain.Entities;
using MeuPetShop.Domain.Interfaces.IAppointment;
using MeuPetShop.Domain.Interfaces.IClients;
using MeuPetShop.Domain.Interfaces.IPets;
using MeuPetShop.Domain.Interfaces.IService;

namespace MeuPetshop.Application.Services;

public class AppointmentService : IAppointmentService
{
    private readonly IAppointmentRepository _appointmentRepository;
    private readonly IClientRepository _clientRepository;
    private readonly IPetRepository _petRepository;
    private readonly IServiceRepository _serviceRepository;

    public AppointmentService(
        IAppointmentRepository appointmentRepository,
        IClientRepository clientRepository,
        IPetRepository petRepository,
        IServiceRepository serviceRepository
    )
    {
        _appointmentRepository = appointmentRepository;
        _clientRepository = clientRepository;
        _petRepository = petRepository;
        _serviceRepository = serviceRepository;
    }
    public async Task<AppointmentDto?> CreateAppointmentAsync(CreateAppointmentDto appointmentDto)
    {
        var client = await _clientRepository.GetByIdAsync(appointmentDto.ClientId);
        var pet = await _petRepository.GetByIdAsync(appointmentDto.PetId);
        var service = await _serviceRepository.GetByIdAsync(appointmentDto.ServiceId);

        if (client == null || pet == null || service == null)
        {
            throw new InvalidOperationException("Invalid client, pet or service");
        }
        
        if (pet.ClientId != client.Id)
        {
            throw new InvalidOperationException("O pet informado não pertence ao cliente informado.");
        }

        var newAppointment = new Appointment
        {
            ClientId = client.Id,
            PetId = pet.Id,
            ServiceId = service.Id,
            AppointmentDateTime = appointmentDto.AppointmentDateTime,
            Notes = appointmentDto.Notes,
            AppointmentStatus = AppointmentStatus.Scheduled
        };
        
        await _appointmentRepository.AddAsync(newAppointment);
        var createdAppointment = await _appointmentRepository.GetByIdAsync(newAppointment.Id);
        return createdAppointment == null ? null : MapAppointmentToDto(createdAppointment);
    }

    public async Task<AppointmentDto?> GetAppointmentByIdAsync(int id)
    {
        var appointment = await _appointmentRepository.GetByIdAsync(id);
        return appointment == null ? null : MapAppointmentToDto(appointment);
    }

    public async Task<IEnumerable<AppointmentDto>> FindAppointmentsByDateRangeAsync(DateTime startDate, DateTime endDate)
    {
        var appointments = await _appointmentRepository.FindByDateRangeAsync(startDate, endDate);
        return appointments.Select(MapAppointmentToDto);
    }

    public async Task<AppointmentDto?> UpdateAppointmentAsync(int id, UpdateAppointmentDto appointmentDto)
    {
        var appointment = await _appointmentRepository.GetByIdAsync(id);
        if (appointment == null) return null;
        
        
        appointment.AppointmentDateTime = appointmentDto.AppointmentDateTime.ToUniversalTime();
        appointment.AppointmentStatus = appointmentDto.Status;
        appointment.Notes = appointmentDto.Notes;
        
        await _appointmentRepository.UpdateAsync(appointment);
        return MapAppointmentToDto(appointment);
    }

    public async Task<AppointmentDto?> CancelAppointmentAsync(int id)
    {
        var appointment = await _appointmentRepository.GetByIdAsync(id);
        if (appointment == null) return null;
        appointment.AppointmentStatus = AppointmentStatus.Canceled;
        await _appointmentRepository.UpdateAsync(appointment);
        return MapAppointmentToDto(appointment);
    }
    
    private AppointmentDto MapAppointmentToDto(Appointment appointment)
    {
        var clientDto = new ClientDto(appointment.Client.Id, appointment.Client.Name, appointment.Client.Phone, appointment.Client.Email, appointment.Client.Address, new List<PetDto>());
        var petDto = new PetDto(appointment.Pet.Id, appointment.Pet.Name, appointment.Pet.Breed, appointment.Pet.Specie, appointment.Pet.ClientId);
        var serviceDto = new ServiceDto(appointment.Service.Id, appointment.Service.Name, appointment.Service.Description, appointment.Service.Price, appointment.Service.DurationInMinutes);

        return new AppointmentDto(appointment.Id, appointment.AppointmentDateTime, appointment.AppointmentStatus, appointment.Notes, clientDto, petDto, serviceDto);
    }
}