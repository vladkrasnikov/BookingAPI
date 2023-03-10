using BookingAPI.Models.Brand;
using BookingApi.Services.Interfaces;
using BookingApi.Services.Model.Brand;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace BookingAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BrandController : ControllerBase
{
    private readonly IBrandService _brandService;
    public BrandController(IBrandService brandService)
    {
        _brandService = brandService;
    }
    
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<GetBrandResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetBrands()
    {
        var result = await _brandService.GetAsync();
        var mappedResult = result.Value.Adapt<IEnumerable<GetBrandResponse>>();
        return Ok(mappedResult);
    }
    
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(GetBrandResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetBrand([FromRoute] Guid id)
    {
        var result = await _brandService.GetAsync(id);
        var mappedResult = result.Value.Adapt<GetBrandResponse>();
        return Ok(mappedResult);
    }
    
    [HttpPost]
    [ProducesResponseType(typeof(GetBrandResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateBrand([FromBody] CreateBrandRequest request)
    {
        var result = await _brandService.CreateAsync(request.Adapt<CreateBrandModel>());
        var mappedResult = result.Value.Adapt<GetBrandResponse>();
        return Ok(mappedResult);
    }
}