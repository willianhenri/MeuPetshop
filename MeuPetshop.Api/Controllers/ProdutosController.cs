using MeuPetShop.Domain.Entities;
using MeuPetshop.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MeuPetshop.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProdutosController : ControllerBase
{
    private readonly AppDbContext _context;

    public ProdutosController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<Produto>>> GetProdutos()
    {
        var produtos = await _context.Produtos.ToListAsync();
        return  Ok(produtos);
    }
}