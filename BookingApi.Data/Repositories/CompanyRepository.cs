using BookingApi.Data.Data;
using BookingApi.Data.Interfaces.Repository;
using BookingApi.Data.Models;
using FluentResults;
using Microsoft.EntityFrameworkCore;

namespace BookingApi.Data.Repositories;

public class CompanyRepository : ICompanyRepository
{
    private readonly ReservationContext _context;

    public CompanyRepository(ReservationContext context)
    {
        _context = context;
    }
    public async Task<Result<IEnumerable<Company>>> GetListAsync()
    {
        return await _context.Company.ToListAsync();
    }

    public async Task<Result<Company>> GetAsync(Guid id)
    {
        var companyEntity = await _context.Company.Include(x => x.Brand).FirstOrDefaultAsync(x => x.Id == id);
        
        if (companyEntity == null)
        {
            return Result.Fail<Company>("Company not found");
        }
        
        return Result.Ok(companyEntity);
    }

    public async Task<Result<Company>> GetAsync(string name)
    {
        var companyEntity = await _context.Company.FirstOrDefaultAsync(x => x.Name.Equals(name));
        
        if (companyEntity == null)
        {
            return Result.Fail<Company>("Company not found");
        }
        
        return Result.Ok(companyEntity);
    }

    public async Task<Result<Company>> CreateAsync(Company company)
    {
        company.Id = Guid.NewGuid();

        await _context.Company.AddAsync(company);
        await _context.SaveChangesAsync();

        return company;
    }
    
    public async Task<Result<Company>> UpdateAsync(Guid id, Company company)
    {
        _context.Company.Update(company);
        await _context.SaveChangesAsync();
        
        return company;
    }
    
    public async Task<Result> DeleteAsync(Guid id)
    {
        var companyEntity = await _context.Company.Include(x => x.Brand).FirstOrDefaultAsync(x => x.Id == id);
        
        if (companyEntity == null)
        {
            return Result.Fail("Company not found");
        }
        
        _context.Company.Remove(companyEntity);
        await _context.SaveChangesAsync();

        return Result.Ok();
    }
    
    public async Task<Result<IEnumerable<Company>>> GetByUserIdAsync(Guid userId)
    {
        var companyEntities = await _context.Company.Include(x => x.Brand).Where(x => x.UserId == userId).ToListAsync();
        
        if (companyEntities == null)
        {
            return Result.Fail<IEnumerable<Company>>("Company not found");
        }
        
        return companyEntities;
    }
}