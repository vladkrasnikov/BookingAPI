using BookingApi.Data.Models;

namespace BookingApi.Data.Interfaces.Repository;

public interface IReservationRepository
{
    public Task<Reservation> CreateAsync(Reservation reservation);
    
    public Task<Reservation> GetAsync(Guid id);
    
    public Task<IEnumerable<Reservation>> GetByPerformerIdAsync(Guid performerId);
    
    public Task<IEnumerable<Reservation>> GetByUserIdAsync(Guid userId);
    
    public Task<IEnumerable<Reservation>> GetByBrandIdAsync(Guid brandId);
    
    public Task<Reservation> UpdateAsync(Reservation reservation);
    
    public Task DeleteAsync(Guid id);
}