using BookingApi.Services.Interfaces;
using BookingApi.Services.Model.Performer;
using Microsoft.AspNetCore.Mvc;

namespace BookingAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PerformerController : Controller
{
    private readonly IPerformerService _performerService;

    public PerformerController(IPerformerService performerService)
    {
        _performerService = performerService;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(PerformerModel))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create(CreatePerformerModel createPerformerRequest)
    {
        var result = await _performerService.CreateAsync(createPerformerRequest);
        return Ok(result.Value);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PerformerModel))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Get(Guid id)
    {
        var result = await _performerService.GetAsync(id);
        return Ok(result.Value);
    }
    
    [HttpGet("brand/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<PerformerModel>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetByBrandId(Guid brandId)
    {
        var result = await _performerService.GetByBrandIdAsync(brandId);
        return Ok(result.Value);
    }
}