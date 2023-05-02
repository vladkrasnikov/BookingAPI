using BookingAPI.Models.Brand;
using BookingApi.Services.Interfaces;
using BookingApi.Services.Model.Brand;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace BookingAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BrandController : Controller
{
    private readonly IBrandService _brandService;
    public BrandController(IBrandService brandService)
    {
        _brandService = brandService;
    }
    
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<GetBrandResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetBrands()
    {
        var result = await _brandService.GetAsync();
        var mappedResult = result.Value.Adapt<IEnumerable<GetBrandResponse>>();
        return Ok(mappedResult);
    }
    
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(GetBrandResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetBrand([FromRoute] Guid id)
    {
        var result = await _brandService.GetAsync(id);
        var mappedResult = result.Value.Adapt<GetBrandResponse>();
        return Ok(mappedResult);
    }
    
    [HttpPost]
    [ProducesResponseType(typeof(GetBrandResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateBrand([FromBody] AddOrUpdateBrandRequest request)
    {
        var result = await _brandService.CreateAsync(request.Adapt<AddOrUpdateBrandModel>());
        var mappedResult = result.Value.Adapt<GetBrandResponse>();
        return Ok(mappedResult);
    }
    
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(GetBrandResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateBrand([FromRoute] Guid id, [FromBody] AddOrUpdateBrandModel request)
    {
        var result = await _brandService.UpdateAsync(id, request);
        var mappedResult = result.Value.Adapt<GetBrandResponse>();
        return Ok(mappedResult);
    }
    
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteBrand([FromRoute] Guid id)
    {
        var result = await _brandService.DeleteAsync(id);
        return Ok(result);
    }
}