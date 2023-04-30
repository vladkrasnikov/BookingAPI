using BookingApi.Data.Models;

namespace BookingApi.Data.Interfaces.Repository;

public interface IReservationRepository
{
    public Task<Reservation> CreateAsync(Reservation reservation);
    
    public Task<Reservation> GetAsync(Guid id);
    
    public Task<IEnumerable<Reservation>> GetByPerformerIdAsync(Guid performerId);
}