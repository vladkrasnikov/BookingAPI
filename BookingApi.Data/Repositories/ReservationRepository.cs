using BookingApi.Data.Data;
using BookingApi.Data.Interfaces.Repository;
using BookingApi.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace BookingApi.Data.Repositories;

public class ReservationRepository : IReservationRepository
{
    private readonly ReservationContext _context;

    public ReservationRepository(ReservationContext context)
    {
        _context = context;
    }

    public async Task<Reservation> CreateAsync(Reservation reservation)
    {
        reservation.Id = Guid.NewGuid();
        
        await _context.Reservation.AddAsync(reservation);
        await _context.SaveChangesAsync();
        
        return reservation;
    }

    public async Task<Reservation> GetAsync(Guid id)
    {
        return await _context.Reservation.FirstOrDefaultAsync(x => x.Id == id);
    }
    
    public async Task<IEnumerable<Reservation>> GetByPerformerIdAsync(Guid performerId)
    {
        return await _context.Reservation.Where(x => x.PerformerId == performerId).ToListAsync();
    }
    
    public async Task<IEnumerable<Reservation>> GetByUserIdAsync(Guid userId)
    {
        return await _context.Reservation.Where(x => x.UserId == userId).ToListAsync();
    }
}