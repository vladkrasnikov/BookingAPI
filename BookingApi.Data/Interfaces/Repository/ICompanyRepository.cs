using FluentResults;
using BookingApi.Data.Models;

namespace BookingApi.Data.Interfaces.Repository;

public interface ICompanyRepository
{
    public Task<Result<IEnumerable<Company>>> GetListAsync();
    public Task<Result<Company>> GetAsync(Guid id);
    public Task<Result<Company>> CreateAsync(Company company);
}