using BookingApi.Services.Model.Brand;
using FluentResults;

namespace BookingApi.Services.Interfaces;

public interface IBrandService
{
    Task<Result<BrandModel>> GetAsync(Guid brandId);
    Task<Result<IEnumerable<BrandModel>>> GetAsync();
    Task<Result<BrandModel>> CreateAsync(AddOrUpdateBrandModel brand);
    Task<Result<BrandModel>> UpdateAsync(Guid id, AddOrUpdateBrandModel brand);
    Task<Result> DeleteAsync(Guid brandId);
}