using BookingApi.Data.Interfaces.Repository;
using BookingApi.Data.Models;
using BookingApi.Services.Interfaces;
using BookingApi.Services.Model.Brand;
using Mapster;

namespace BookingApi.Services.Services;

public class BrandService : IBrandService
{
    private readonly IBrandRepository _brandRepository;

    public BrandService(IBrandRepository brandRepository)
    {
        _brandRepository = brandRepository;
    }
    
    public async Task<BrandModel> GetByIdAsync(Guid brandId)
    {
        var brandEntity = await _brandRepository.GetByIdAsync(brandId);
        return brandEntity.Adapt<BrandModel>();
    }

    public async Task<IEnumerable<BrandModel>> GetAsync()
    {
        var brands = await _brandRepository.GetAsync();
        return brands.Adapt<IEnumerable<BrandModel>>();
    }

    public async Task<BrandModel> CreateAsync(CreateBrandModel brand)
    {
        await _brandRepository.CreateAsync(brand.Adapt<Brand>());
        return brand.Adapt<BrandModel>();
    }

    public Task<BrandModel> UpdateAsync(BrandModel brand)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Guid brandId)
    {
        throw new NotImplementedException();
    }
}