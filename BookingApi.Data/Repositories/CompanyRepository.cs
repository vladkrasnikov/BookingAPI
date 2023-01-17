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
}