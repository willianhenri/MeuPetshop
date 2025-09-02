using Microsoft.AspNetCore.Identity;

namespace MeuPetshop.Infrastructure.Data;

public class DbSeeder
{
    public static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
    {

        if (!await roleManager.RoleExistsAsync("Admin"))
        {
            await roleManager.CreateAsync(new IdentityRole("Admin"));
        }
        
        if (!await roleManager.RoleExistsAsync("Funcionario"))
        {
            await roleManager.CreateAsync(new IdentityRole("Funcionario"));
        }
    }
}