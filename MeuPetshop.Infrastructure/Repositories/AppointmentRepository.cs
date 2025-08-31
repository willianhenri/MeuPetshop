using MeuPetShop.Domain.Entities;
using MeuPetShop.Domain.Interfaces.IAppointment;
using MeuPetshop.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MeuPetshop.Infrastructure.Repositories;

public class AppointmentRepository : IAppointmentRepository
{
    private readonly AppDbContext _context;

    public AppointmentRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task AddAsync(Appointment appointment)
    {
        await _context.Appointments.AddAsync(appointment);
        await _context.SaveChangesAsync();
    }

    public async Task<Appointment?> GetByIdAsync(int id)
    {
        return await _context.Appointments
            .Include(a => a.Client)
            .Include(a => a.Pet)
            .Include(a => a.Service)
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<IEnumerable<Appointment>> FindByDateRangeAsync(DateTime startDate, DateTime endDate)
    {
        return await _context.Appointments
            .Include(a => a.Client)
            .Include(a => a.Pet)
            .Include(a => a.Service)
            .Where(a => a.AppointmentDateTime >= startDate && a.AppointmentDateTime <= endDate)
            .ToListAsync();
    }

    public async Task UpdateAsync(Appointment appointment)
    {
        _context.Appointments.Update(appointment);
        await _context.SaveChangesAsync();
    }
}