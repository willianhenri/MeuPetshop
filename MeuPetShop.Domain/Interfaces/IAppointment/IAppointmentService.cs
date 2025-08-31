using MeuPetShop.Domain.Dtos.AppointmentDto;

namespace MeuPetShop.Domain.Interfaces.IAppointment;

public interface IAppointmentService
{
    Task<AppointmentDto?> CreateAppointmentAsync(CreateAppointmentDto appointmentDto);
    Task<AppointmentDto?> GetAppointmentByIdAsync(int id);
    Task<IEnumerable<AppointmentDto>> FindAppointmentsByDateRangeAsync(DateTime startDate, DateTime endDate);
    Task<AppointmentDto?> UpdateAppointmentAsync(int id, UpdateAppointmentDto appointmentDto);
    Task<AppointmentDto?> CancelAppointmentAsync(int id);
}