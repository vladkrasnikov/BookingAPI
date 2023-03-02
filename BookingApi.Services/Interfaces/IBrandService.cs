using BookingApi.Services.Model.Brand;

namespace BookingApi.Services.Interfaces;

public interface IBrandService
{
    Task<BrandModel> GetByIdAsync(Guid brandId);
    Task<IEnumerable<BrandModel>> GetAsync();
    Task<BrandModel> CreateAsync(CreateBrandModel brand);
    Task<BrandModel> UpdateAsync(BrandModel brand);
    Task DeleteAsync(Guid brandId);
}