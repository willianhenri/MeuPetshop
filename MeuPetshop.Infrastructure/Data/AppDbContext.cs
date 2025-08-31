using MeuPetShop.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MeuPetshop.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }
    
    public DbSet<Product> Products { get; set; }
    public DbSet<Pet> Pets { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<Service> Services { get; set; }
    public DbSet<Appointment> Appointments { get; set; }
}