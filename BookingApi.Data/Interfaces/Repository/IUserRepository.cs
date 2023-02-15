using BookingApi.Data.Models;
using FluentResults;

namespace BookingApi.Data.Interfaces.Repository;

public interface IUserRepository
{
    public Task CreateAsync(User userModel);
    public Task<User> GetAsync(string emailAddress);
}