using BookingApi.Services.Model.Brand;
using FluentResults;

namespace BookingApi.Services.Interfaces;

public interface IBrandService
{
    Task<Result<BrandModel>> GetAsync(Guid brandId);
    Task<Result<BrandModel>> GetAsync(string brandName);
    Task<Result<IEnumerable<BrandModel>>> GetAsync();
    Task<Result<BrandModel>> CreateAsync(CreateBrandModel brand);
    Task<Result<BrandModel>> UpdateAsync(BrandModel brand);
    Task<Result> DeleteAsync(Guid brandId);
}