using BeerSender.Domain.Projections;
using Marten;
using Microsoft.AspNetCore.Mvc;

namespace BeerSender.Web.Controllers;

[ApiController]
[Route("api/command/[controller]")]
public class BrandController(IDocumentStore store) : ControllerBase
{
    [HttpGet("{name}")]
    public async Task<IActionResult> GetBrand([FromRoute]string name)
    {
        await using var session = store.QuerySession();
        var brand = await session
            .Query<Brand>()
            .FirstOrDefaultAsync(b => b.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        
        if (brand is null)
            return NotFound();
        
        return Ok(brand);
    }
}
