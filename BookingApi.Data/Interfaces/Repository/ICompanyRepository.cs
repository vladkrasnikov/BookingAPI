using FluentResults;
using BookingApi.Data.Models;

namespace BookingApi.Data.Interfaces.Repository;

public interface ICompanyRepository
{
    public Task<Result<IEnumerable<Company>>> GetListAsync();
}