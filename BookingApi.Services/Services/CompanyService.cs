using BookingApi.Data.Interfaces.Repository;
using BookingApi.Data.Models;
using BookingApi.Services.Extensions;
using BookingApi.Services.Interfaces;
using BookingApi.Services.Model.Company;
using FluentResults;
using Mapster;

namespace BookingApi.Services.Services;

public class CompanyService : ICompanyService
{
    private readonly ICompanyRepository _companyRepository;

    public CompanyService(ICompanyRepository companyRepository)
    {
        _companyRepository = companyRepository;
    }

    public async Task<Result<List<CompanyModel>>> GetListAsync()
    {
        var result = await _companyRepository.GetListAsync();
        return result.EntityToModel<IEnumerable<Company>, List<CompanyModel>>();
    }

    public async Task<Result<CompanyModel>> GetAsync(Guid id)
    {
        var result = await _companyRepository.GetAsync(id);
        return result.EntityToModel<Company, CompanyModel>();
    }

    public async Task<Result<CompanyModel>> CreateAsync(CreateCompanyModel createCompanyModel)
    {
        var companyResult = await _companyRepository.GetAsync(createCompanyModel.Name);
        if(companyResult.IsSuccess)
        {
            return Result.Fail<CompanyModel>("Company with the same name already exists");
        }
        var result = await _companyRepository.CreateAsync(createCompanyModel.Adapt<Company>());
        return result.EntityToModel<Company, CompanyModel>();
    }
}