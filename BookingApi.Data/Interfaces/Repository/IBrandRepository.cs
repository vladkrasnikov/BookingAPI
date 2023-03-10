using BookingApi.Data.Models;
using FluentResults;

namespace BookingApi.Data.Interfaces.Repository;

public interface IBrandRepository
{
    Task<Result<IEnumerable<Brand>>> GetAsync();
    Task<Result<Brand>> GetAsync(Guid brandId);
    Task<Result<Brand>> GetAsync(string brandName);
    Task<Result<Brand>> CreateAsync(Brand brand);
    Task<Result<Brand>> UpdateAsync(Brand brand);
    Task<Result> DeleteAsync(Guid brandId);
}