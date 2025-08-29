using MeuPetShop.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MeuPetshop.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }
    
    public DbSet<Product> Produtos { get; set; }
}