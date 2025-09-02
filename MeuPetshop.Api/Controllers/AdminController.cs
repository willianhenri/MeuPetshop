using MeuPetShop.Domain.Dtos.Admin;
using MeuPetShop.Domain.Entities.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MeuPetshop.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
public class AdminController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public AdminController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    [HttpPost("assign-role")]
    public async Task<IActionResult> AssignRole([FromBody] AssignRoleDto assignRoleDto)
    {
        var user = await _userManager.FindByEmailAsync(assignRoleDto.Email);
        if (user == null)
        {
            return NotFound(new { Message = $"Usuário com email '{assignRoleDto.Email}' não encontrado." });
        }

        if (!await _roleManager.RoleExistsAsync(assignRoleDto.RoleName))
        {
            return BadRequest(new { Message = $"O perfil '{assignRoleDto.RoleName}' não existe." });
        }

        var result = await _userManager.AddToRoleAsync(user, assignRoleDto.RoleName);

        if (result.Succeeded)
        {
            return Ok(new { Message = $"Perfil '{assignRoleDto.RoleName}' atribuído com sucesso ao usuário '{user.UserName}'." });
        }

        return BadRequest(result.Errors);
    }
}