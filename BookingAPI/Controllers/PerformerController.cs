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
    public async Task<IActionResult> Create(AddOrUpdatePerformerModel addOrUpdatePerformerRequest)
    {
        var result = await _performerService.CreateAsync(addOrUpdatePerformerRequest);
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
    public async Task<IActionResult> GetByBrandId(Guid id)
    {
        var result = await _performerService.GetByBrandIdAsync(id);
        return Ok(result.Value);
    }
    
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PerformerModel))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update(Guid id, AddOrUpdatePerformerModel updatePerformerRequest)
    {
        var result = await _performerService.UpdateAsync(id, updatePerformerRequest);
        return Ok(result.Value);
    }
    
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(Guid id)
    {
        return Ok(await _performerService.DeleteAsync(id));
    }
}