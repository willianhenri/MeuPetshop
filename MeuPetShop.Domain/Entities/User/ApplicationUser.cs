using System.Security.Principal;
using Microsoft.AspNetCore.Identity;

namespace MeuPetShop.Domain.Entities.User;

public class ApplicationUser : IdentityUser
{
    public string FullName { get; set; }
    public DateTime CreatedAt { get; set; }
    
    public bool IsActive { get; set; } = true;
    
    public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}