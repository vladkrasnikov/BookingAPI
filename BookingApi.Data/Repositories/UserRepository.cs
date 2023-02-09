using BookingApi.Data.Data;
using BookingApi.Data.Interfaces.Repository;
using BookingApi.Data.Models;
using FluentResults;

namespace BookingApi.Data.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ReservationContext _context;

    public UserRepository(ReservationContext context)
    {
        _context = context;
    }

    public async Task CreateAsync(User userEntity)
    {
        userEntity.Id = Guid.NewGuid();

        await _context.User.AddAsync(userEntity);
        await _context.SaveChangesAsync();
    }
}