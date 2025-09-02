using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Linq;
using MeuPetShop.Domain.Dtos.Auth;
using MeuPetShop.Domain.Entities.User;

namespace MeuPetshop.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IConfiguration _configuration;
    private readonly RoleManager<IdentityRole> _roleManager;

    public AuthController(
        UserManager<ApplicationUser> userManager,
        IConfiguration configuration,
        RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _configuration = configuration;
        _roleManager = roleManager;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
        var userExists = await _userManager.FindByNameAsync(registerDto.UserName);
        if (userExists != null)
        {
            return StatusCode(StatusCodes.Status409Conflict, new { Message = "Username já existe." });
        }

        var emailExists = await _userManager.FindByEmailAsync(registerDto.Email);
        if (emailExists != null)
        {
            return StatusCode(StatusCodes.Status409Conflict, new { Message = "Email já cadastrado." });
        }

        ApplicationUser user = new()
        {
            Email = registerDto.Email,
            SecurityStamp = Guid.NewGuid().ToString(),
            UserName = registerDto.UserName,
            FullName = registerDto.FullName,
            CreatedAt = DateTime.UtcNow,
            IsActive = true
        };

        var result = await _userManager.CreateAsync(user, registerDto.Password);

        if (!result.Succeeded)
        {
            var errors = result.Errors.Select(e => e.Description).ToList();
            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Falha ao criar usuário.", Errors = errors });
        }

        string roleToAssign;
    
        if (registerDto.Email == "admin@petshop.com")
        {
            roleToAssign = "Admin";
        }
        else
        {
            roleToAssign = "Funcionario";
        }

        if (!await _roleManager.RoleExistsAsync(roleToAssign))
        {
            await _roleManager.CreateAsync(new IdentityRole(roleToAssign));
        }
    
        await _userManager.AddToRoleAsync(user, roleToAssign);

        return Ok(new { Message = "Usuário criado com sucesso!" });
    }
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        var user = await _userManager.FindByNameAsync(loginDto.UserName);

        if (user != null && user.IsActive && await _userManager.CheckPasswordAsync(user, loginDto.Password))
        {
            var token = await GenerateJwtToken(user);
            return Ok(new LoginResponseDto(token));
        }
        
        return Unauthorized(new { Message = "Usuário ou senha inválidos."});
    }

    private async Task<string> GenerateJwtToken(ApplicationUser user)
    {
        var userRoles = await _userManager.GetRolesAsync(user);

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email)
        };
        
        foreach (var userRole in userRoles)
        {
            claims.Add(new Claim(ClaimTypes.Role, userRole));
        }

        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["ApiSettings:Secret"]));

        var token = new JwtSecurityToken(
            issuer: null,
            audience: null,
            expires: DateTime.UtcNow.AddHours(8),
            claims: claims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}