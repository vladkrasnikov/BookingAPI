using BookingApi.Services.Model.Reservation;
using FluentResults;

namespace BookingApi.Services.Interfaces;

public interface IReservationService
{
    public Task<Result<ReservationModel>> CreateAsync(CreateReservationModel createReservationModel);
    
    public Task<Result<ReservationModel>> GetAsync(Guid id);
    
    public Task<Result<List<ReservationModel>>> GetByPerformerIdAsync(Guid performerId);
}