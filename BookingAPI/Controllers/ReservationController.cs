﻿using BookingApi.Services.Interfaces;
using BookingApi.Services.Model.Reservation;
using Microsoft.AspNetCore.Mvc;

namespace BookingAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ReservationController : Controller
{
    private readonly IReservationService _reservationService;

    public ReservationController(IReservationService reservationService)
    {
        _reservationService = reservationService;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ReservationModel))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create(CreateReservationModel createReservationRequest)
    {
        var result = await _reservationService.CreateAsync(createReservationRequest);
        return Ok(result.Value);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ReservationModel))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Get(Guid id)
    {
        var result = await _reservationService.GetAsync(id);
        return Ok(result.Value);
    }
    
    [HttpGet("performer/{performerId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ReservationModel>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetByPerformerId(Guid performerId)
    {
        var result = await _reservationService.GetByPerformerIdAsync(performerId);
        return Ok(result.Value);
    }
    
    // [HttpGet("brand/{id}")]
    // [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ReservationModel>))]
    // [ProducesResponseType(StatusCodes.Status404NotFound)]
    // [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    // public async Task<IActionResult> GetByBrandId(Guid brandId)
    // {
    //     var result = await _reservationService.GetByBrandIdAsync(brandId);
    //     return Ok(result.Value);
    // }
}