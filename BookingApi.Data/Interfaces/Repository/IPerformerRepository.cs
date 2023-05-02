using BookingApi.Data.Models;

namespace BookingApi.Data.Interfaces.Repository;

public interface IPerformerRepository
{
    public Task<Performer> GetAsync(Guid id);
    
    public Task<IEnumerable<Performer>> GetByBrandIdAsync(Guid brandId);

    public Task<Performer> CreateAsync(Performer performer);
    
    public Task<Performer> UpdateAsync(Guid id, Performer performer);
    
    public Task DeleteAsync(Guid id);
}