using BookingApi.Data.Models;
using FluentResults;

namespace BookingApi.Data.Interfaces.Repository;

public interface IBrandRepository
{
    Task<Result<IEnumerable<Brand>>> GetAsync();
    Task<Result<Brand>> GetAsync(Guid brandId);
    Task<Result<Brand>> GetBrandInCompanyAsync(Brand brand);
    Task<Result<Brand>> CreateAsync(Brand brand);
    Result<Brand> UpdateAsync(Brand brand);
    Task<Result> DeleteAsync(Guid brandId);
}