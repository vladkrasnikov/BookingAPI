using BookingApi.Data.Interfaces.Repository;
using BookingApi.Data.Models;
using BookingApi.Services.Interfaces;
using BookingApi.Services.Model.Brand;
using BookingApi.Services.Extensions;
using FluentResults;
using Mapster;

namespace BookingApi.Services.Services;

public class BrandService : IBrandService
{
    private readonly IBrandRepository _brandRepository;

    public BrandService(IBrandRepository brandRepository)
    {
        _brandRepository = brandRepository;
    }

    public async Task<Result<BrandModel>> GetAsync(Guid brandId)
    {
        var brandEntity = await _brandRepository.GetAsync(brandId);
        
        if (brandEntity.IsFailed)
            return brandEntity.ToResult();
        
        return brandEntity.Value.Adapt<BrandModel>();
    }

    public async Task<Result<IEnumerable<BrandModel>>> GetAsync()
    {
        var brands = await _brandRepository.GetAsync();
        return brands.EntityToModel<IEnumerable<Brand>, IEnumerable<BrandModel>>();
    }

    public async Task<Result<BrandModel>> CreateAsync(AddOrUpdateBrandModel brand)
    {
        var brandEntity = await _brandRepository.GetBrandInCompanyAsync(brand.Adapt<Brand>());
        // Check if brand with the same name already exists, return 400 not 500
        if(brandEntity.IsSuccess)
        {
            return Result.Fail("Brand with the same name already exists");
        }
        var brandResult = await _brandRepository.CreateAsync(brand.Adapt<Brand>());
        return brandResult.Value.Adapt<BrandModel>();
    }

    public async Task<Result<BrandModel>> UpdateAsync(Guid id, AddOrUpdateBrandModel brand)
    {
        var brandEntity = await _brandRepository.GetAsync(id);
        if (brandEntity.IsFailed)
            return brandEntity.ToResult();
        
        var brandResult = await _brandRepository.UpdateAsync(brand.Adapt<Brand>());
        return brandResult.Value.Adapt<BrandModel>();
    }

    public async Task<Result> DeleteAsync(Guid brandId)
    {
        var brandEntity = await _brandRepository.GetAsync(brandId);
        if (brandEntity.IsFailed)
            return brandEntity.ToResult();
        
        return await _brandRepository.DeleteAsync(brandId);
    }
}