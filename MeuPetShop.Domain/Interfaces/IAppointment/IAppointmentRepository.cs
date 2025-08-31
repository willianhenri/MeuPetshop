using MeuPetShop.Domain.Entities;

namespace MeuPetShop.Domain.Interfaces.IAppointment;

public interface IAppointmentRepository
{
    Task AddAsync(Appointment appointment);
    Task<Appointment?> GetByIdAsync(int id);
    Task<IEnumerable<Appointment>> FindByDateRangeAsync(DateTime startDate, DateTime endDate);
    Task UpdateAsync(Appointment appointment);
}