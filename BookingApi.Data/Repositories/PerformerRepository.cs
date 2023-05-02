using BookingApi.Data.Data;
using BookingApi.Data.Interfaces.Repository;
using BookingApi.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace BookingApi.Data.Repositories;

public class PerformerRepository : IPerformerRepository
{
    private readonly ReservationContext _context;

    public PerformerRepository(ReservationContext context)
    {
        _context = context;
    }

    public async Task<Performer> GetAsync(Guid id)
    {
        return await _context.Performer.FirstOrDefaultAsync(x => x.Id == id);
    }
    
    public async Task<IEnumerable<Performer>> GetByBrandIdAsync(Guid brandId)
    {
        return await _context.Performer.Where(x => x.BrandId == brandId).ToListAsync();
    }

    public async Task<Performer> CreateAsync(Performer performer)
    {
        performer.Id = Guid.NewGuid();
        
        await _context.Performer.AddAsync(performer);
        await _context.SaveChangesAsync();
        
        return performer;
    }
    
    public async Task<Performer> UpdateAsync(Guid id, Performer performer)
    {
        performer.Id = id;
        
        _context.Performer.Update(performer);
        await _context.SaveChangesAsync();
        
        return performer;
    }
    
    public async Task DeleteAsync(Guid id)
    {
        var performer = await _context.Performer.FirstOrDefaultAsync(x => x.Id == id);
        if (performer == null)
        {
            return;
        }
        
        _context.Performer.Remove(performer);
        await _context.SaveChangesAsync();
    }
}