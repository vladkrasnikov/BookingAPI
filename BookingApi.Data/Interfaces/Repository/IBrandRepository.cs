using BookingApi.Data.Models;
using FluentResults;

namespace BookingApi.Data.Interfaces.Repository;

public interface IBrandRepository
{
    Task<IEnumerable<Brand>> GetAsync();
    Task<Brand> GetByIdAsync(Guid brandId);
    Task<Brand> CreateAsync(Brand brand);
    Task<Brand> UpdateAsync(Brand brand);
    Task<Result> DeleteAsync(Guid brandId);
}