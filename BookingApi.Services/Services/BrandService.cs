﻿using BookingApi.Data.Interfaces.Repository;
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

    public Task<IEnumerable<BrandModel>> GetAsync()
    {
        throw new NotImplementedException();
    }

    public Task<BrandModel> CreateAsync(BrandModel brand)
    {
        throw new NotImplementedException();
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