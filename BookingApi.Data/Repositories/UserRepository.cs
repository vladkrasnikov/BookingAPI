using BookingApi.Data.Data;
using BookingApi.Data.Interfaces.Repository;
using BookingApi.Data.Models;
using FluentResults;
using Microsoft.EntityFrameworkCore;

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

    public async Task<User> GetAsync(string emailAddress)
    {
        var user = await _context.User.FirstOrDefaultAsync(x => x.EmailAddress == emailAddress);
        return user ?? throw new Exception($"User with email {emailAddress} not found");
    }
}