using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration; 
using System.IO; 


namespace MeuPetshop.Infrastructure.Data
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../MeuPetshop.Api")) 
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            optionsBuilder.UseNpgsql(connectionString); 

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}