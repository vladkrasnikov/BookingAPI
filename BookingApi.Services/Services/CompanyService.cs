using BookingApi.Data.Interfaces.Repository;
using BookingApi.Data.Models;
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
        return ToResult<IEnumerable<Company>, List<CompanyModel>>(result);
    }

    public async Task<Result<CompanyModel>> GetAsync(Guid id)
    {
        var result = await _companyRepository.GetAsync(id);
        return ToResult<Company, CompanyModel>(result);
    }

    public async Task<Result<CompanyModel>> CreateAsync(CreateCompanyModel createCompanyModel)
    {
        var result = await _companyRepository.CreateAsync(createCompanyModel.Adapt<Company>());
        return ToResult<Company, CompanyModel>(result);
    }

    private static Result<TO> ToResult<TFrom, TO>(Result<TFrom> result)
    {
        return result.IsFailed ? result.ToResult() : Result.Ok(result.Value.Adapt<TO>());
    }
}