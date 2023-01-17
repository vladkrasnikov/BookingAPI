using BookingApi.Data.Interfaces.Repository;
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
        var mappedModel = result.Value.Adapt<List<CompanyModel>>();
        return result.IsFailed ? result.ToResult() : Result.Ok(mappedModel);
    }
}