using Microsoft.AspNetCore.Mvc;
using IslamicPOS.Infrastructure.Repositories;
using IslamicPOS.Core.Models.Products;

namespace IslamicPOS.API.Controllers;

public class ProductController : BaseApiController
{
    private readonly IProductRepository _productRepository;

    public ProductController(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetAll()
    {
        var products = await _productRepository.GetAllAsync();
        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetById(Guid id)
    {
        var product = await _productRepository.GetByIdAsync(id);
        if (product == null)
            return NotFound();
            
        return Ok(product);
    }

    [HttpPost]
    public async Task<ActionResult<Product>> Create([FromBody] Product product)
    {
        var created = await _productRepository.AddAsync(product);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(Guid id, [FromBody] Product product)
    {
        if (id != product.Id)
            return BadRequest();

        await _productRepository.UpdateAsync(product);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        await _productRepository.DeleteAsync(id);
        return NoContent();
    }

    [HttpGet("halal")]
    public async Task<ActionResult<IEnumerable<Product>>> GetHalalProducts()
    {
        var products = await _productRepository.GetHalalProductsAsync();
        return Ok(products);
    }

    [HttpGet("category/{category}")]
    public async Task<ActionResult<IEnumerable<Product>>> GetByCategory(string category)
    {
        var products = await _productRepository.GetByCategory(category);
        return Ok(products);
    }

    [HttpGet("lowstock")]
    public async Task<ActionResult<IEnumerable<Product>>> GetLowStockProducts([FromQuery] int threshold = 10)
    {
        var products = await _productRepository.GetLowStockProducts(threshold);
        return Ok(products);
    }
}