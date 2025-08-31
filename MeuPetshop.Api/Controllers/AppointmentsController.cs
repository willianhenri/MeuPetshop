using MeuPetShop.Domain.Dtos.AppointmentDto;
using MeuPetShop.Domain.Interfaces.IAppointment;
using MeuPetShop.Domain.Interfaces.IService;
using Microsoft.AspNetCore.Mvc;

namespace MeuPetshop.Api.Controllers;

[ApiController]
[Route("api/appointments")]
public class AppointmentsController : Controller
{
    private readonly IAppointmentService _appointmentService;

    public AppointmentsController(IAppointmentService appointmentService)
    {
        _appointmentService = appointmentService;
    }
    
      [HttpGet("{id}", Name = "GetAppointmentById")]
    public async Task<ActionResult<AppointmentDto>> GetAppointmentById(int id)
    {
        var appointment = await _appointmentService.GetAppointmentByIdAsync(id);
        if (appointment == null)
        {
            return NotFound();
        }
        return Ok(appointment);
    }

    [HttpGet("search")]
    public async Task<ActionResult<IEnumerable<AppointmentDto>>> FindAppointmentsByDate([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
    {
        var appointments = await _appointmentService.FindAppointmentsByDateRangeAsync(startDate, endDate);
        return Ok(appointments);
    }
    
    [HttpPost]
    public async Task<ActionResult<AppointmentDto>> CreateAppointment([FromBody] CreateAppointmentDto appointmentDto)
    {
        try
        {
            var newAppointment = await _appointmentService.CreateAppointmentAsync(appointmentDto);
            if (newAppointment == null)
            {
                return BadRequest("Não foi possível criar o agendamento.");
            }
            return CreatedAtRoute("GetAppointmentById", new { id = newAppointment.Id }, newAppointment);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<AppointmentDto>> UpdateAppointment(int id, [FromBody] UpdateAppointmentDto appointmentDto)
    {
        var updatedAppointment = await _appointmentService.UpdateAppointmentAsync(id, appointmentDto);
        if (updatedAppointment == null)
        {
            return NotFound();
        }
        return Ok(updatedAppointment);
    }

    [HttpPost("{id}/cancel")]
    public async Task<ActionResult<AppointmentDto>> CancelAppointment(int id)
    {
        var canceledAppointment = await _appointmentService.CancelAppointmentAsync(id);
        if (canceledAppointment == null)
        {
            return NotFound();
        }
        return Ok(canceledAppointment);
    }
}