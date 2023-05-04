using BookingApi.Data.Data;
using BookingApi.Data.Interfaces.Repository;
using BookingApi.Data.Models;
using FluentResults;
using Microsoft.EntityFrameworkCore;

namespace BookingApi.Data.Repositories;

public class BrandRepository : IBrandRepository
{
    private readonly ReservationContext _context;

    public BrandRepository(ReservationContext context)
    {
        _context = context;
    }

    public async Task<Result<IEnumerable<Brand>>> GetAsync()
    {
        return await _context.Brand.ToListAsync();
    }

    public async Task<Result<Brand>> GetAsync(Guid brandId)
    {
        var brandEntity = await _context.Brand.FirstOrDefaultAsync(x => x.Id == brandId);
        
        if (brandEntity is null)
        {
            return Result.Fail("Entity not found");
        }

        return brandEntity;
    }

    public async Task<Result<Brand>> GetBrandInCompanyAsync(Brand brand)
    {
        var brandEntity = await _context.Brand.Where(x => x.CompanyId == brand.CompanyId).FirstOrDefaultAsync(x => x.Name.Equals(brand.Name));
        
        if (brandEntity is null)
        {
            return Result.Fail("Entity not found");
        }

        return brandEntity;
    }

    public async Task<Result<Brand>> CreateAsync(Brand brand)
    {
        brand.Id = Guid.NewGuid();
        await _context.Brand.AddAsync(brand);
        await _context.SaveChangesAsync();
        return brand;
    }

    public async Task<Result<Brand>> UpdateAsync(Brand brand)
    {
        _context.Brand.Update(brand);
        await _context.SaveChangesAsync();
        return brand;
    }

    public async Task<Result> DeleteAsync(Guid brandId)
    {
        var brandEntity = await _context.Brand.FirstOrDefaultAsync(x => x.Id == brandId);
        if (brandEntity == null)
        {
            Result.Fail("Entity not found");
        }
        _context.Brand.Remove(brandEntity);
        await _context.SaveChangesAsync();
        return Result.Ok();
    }
}