using MeuPetshop.Domain.Dtos;
using MeuPetshop.Domain.Dtos.ProdutoDto;
using MeuPetShop.Domain.Entities;
using MeuPetShop.Domain.Interfaces;
using MeuPetShop.Domain.Interfaces.IProdutos;
using MeuPetShop.Domain.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MeuPetshop.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProdutosController : ControllerBase
{
    private readonly IProdutoService _produtoService;

    public ProdutosController(IProdutoService produtoService)
    {
        _produtoService = produtoService;
    }

    [HttpGet]
    public async Task<ActionResult<PagedApiResponse<ProdutoDto>>> GetAllPagedAsync([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        var response = await _produtoService.GetAllProductsAsync(pageNumber, pageSize);
        return Ok(response);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ProdutoDto>> GetProdutoByIdAsync(int id)
    {
        var produto = await _produtoService.GetProductByIdAsync(id);
        if(produto == null) return NotFound();
        return Ok(produto);
    }

    [HttpPost]
    public async Task<ActionResult<ProdutoDto>> PostProdutos(CreateProdutoDto produto)
    {
        try
        {
            var novoProduto = await _produtoService.CreateProductAsync(produto);
            return Ok(novoProduto);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<ProdutoDto>> PutProdutoAsync(int id, UpdateProdutoDto produtoDto)
    {
        var produtoAtualizado = await _produtoService.UpdateProductAsync(id, produtoDto);
        if(produtoAtualizado == null) return NotFound();
        return Ok(produtoAtualizado);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<bool>> DeleteProdutoAsync(int id)
    {
        var produtoDeletado = await _produtoService.DeleteProductAsync(id);
        if(!produtoDeletado) return NotFound();
        return NoContent();
    }
}