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
        return await _context.Company.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Result<Company>> CreateAsync(Company company)
    {
        company.Id = Guid.NewGuid();

        await _context.Company.AddAsync(company);
        await _context.SaveChangesAsync();

        return company;
    }
}