using BookingApi.Services.Model.Company;
using FluentResults;

namespace BookingApi.Services.Interfaces;

public interface ICompanyService
{ 
    Task<Result<List<CompanyModel>>> GetListAsync();
    Task<Result<CompanyModel>> GetAsync(Guid id);
    Task<Result<CompanyModel>> CreateAsync(AddOrUpdateCompanyModel addOrUpdateCompanyModel);
    Task<Result<CompanyModel>> UpdateAsync(Guid id, AddOrUpdateCompanyModel addOrUpdateCompanyModel);
    Task<Result> DeleteAsync(Guid id);
    Task<Result<List<CompanyModel>>> GetByUserIdAsync(Guid userId);
}