using MeuPetshop.Domain.Dtos;
using MeuPetshop.Domain.Dtos.ProductDtos;
using MeuPetShop.Domain.Entities;
using MeuPetShop.Domain.Interfaces;
using MeuPetShop.Domain.Interfaces.IProducts;
using MeuPetShop.Domain.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MeuPetshop.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<ActionResult<PagedApiResponse<ProductDto>>> GetAllPagedAsync([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        var response = await _productService.GetAllProductsAsync(pageNumber, pageSize);
        return Ok(response);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ProductDto>> GetProdutoByIdAsync(int id)
    {
        var produto = await _productService.GetProductByIdAsync(id);
        if(produto == null) return NotFound();
        return Ok(produto);
    }

    [HttpGet("search")]
    public async Task<ActionResult<PagedApiResponse<ProductDto>>> SearchProducts(
        [FromQuery] string term, 
        [FromQuery] int pageNumber = 1, 
        [FromQuery] int pageSize = 10)
    {
        if (string.IsNullOrWhiteSpace(term))
        {
            return Ok(new PagedApiResponse<ProductDto>
            {
                Data = new List<ProductDto>(), // Lista vazia
                Pagination = new PaginationData { TotalCount = 0, TotalPages = 0, CurrentPage = 1, PageSize = pageSize }
            });
        }

        var response = await _productService.SearchProductsByNameAsync(term, pageNumber, pageSize);
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<ProductDto>> PostProdutos(CreateProductDto product)
    {
        try
        {
            var novoProduto = await _productService.CreateProductAsync(product);
            return Ok(novoProduto);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<ProductDto>> PutProdutoAsync(int id, UpdateProductDto productDto)
    {
        var produtoAtualizado = await _productService.UpdateProductAsync(id, productDto);
        if(produtoAtualizado == null) return NotFound();
        return Ok(produtoAtualizado);
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<bool>> DeleteProdutoAsync(int id)
    {
        var produtoDeletado = await _productService.DeleteProductAsync(id);
        if(!produtoDeletado) return NotFound();
        return NoContent();
    }
}