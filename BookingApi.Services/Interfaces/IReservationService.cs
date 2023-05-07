using BookingApi.Services.Model.Reservation;
using FluentResults;

namespace BookingApi.Services.Interfaces;

public interface IReservationService
{
    public Task<Result<ReservationModel>> CreateAsync(CreateReservationModel createReservationModel);
    
    public Task<Result<ReservationModel>> GetAsync(Guid id);
    
    public Task<Result<List<ReservationModel>>> GetByPerformerIdAsync(Guid performerId);
    
    public Task<Result<List<ReservationModel>>> GetByUserIdAsync(Guid userId);
    
    public Task<Result<List<ReservationModel>>> GetByBrandIdAsync(Guid brandId);
}