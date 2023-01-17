using BookingApi.Services.Model.Company;
using FluentResults;

namespace BookingApi.Services.Interfaces;

public interface ICompanyService
{ 
    Task<Result<List<CompanyModel>>> GetListAsync();
}