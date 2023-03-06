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
        return brandEntity.Adapt<BrandModel>();
    }

    public async Task<Result<BrandModel>> GetAsync(string brandName)
    {
        var brandEntity = await _brandRepository.GetAsync(brandName);
        return Result.Ok(brandEntity.Adapt<BrandModel>());
    }

    public async Task<Result<IEnumerable<BrandModel>>> GetAsync()
    {
        var brands = await _brandRepository.GetAsync();
        return brands.EntityToModel<IEnumerable<Brand>, IEnumerable<BrandModel>>();
    }

    public async Task<Result<BrandModel>> CreateAsync(CreateBrandModel brand)
    {
        var brandEntity = await GetAsync(brand.Name);
        // Check if brand with the same name already exists, return 400 not 500
        if(brandEntity.IsSuccess)
        {
            return Result.Fail("Brand with the same name already exists");
        }
        await _brandRepository.CreateAsync(brand.Adapt<Brand>());
        return brand.Adapt<BrandModel>();
    }

    public Task<Result<BrandModel>> UpdateAsync(BrandModel brand)
    {
        throw new NotImplementedException();
    }

    public Task<Result> DeleteAsync(Guid brandId)
    {
        throw new NotImplementedException();
    }
}