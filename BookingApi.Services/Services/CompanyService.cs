using BookingApi.Data.Helpers.Interfaces;
using BookingApi.Data.Models;
using BookingApi.Services.Extensions;
using BookingApi.Services.Interfaces;
using BookingApi.Services.Model.Company;
using FluentResults;
using Mapster;

namespace BookingApi.Services.Services;

public class CompanyService : ICompanyService
{
    private readonly IUnitOfWork _unitOfWork;

    public CompanyService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<List<CompanyModel>>> GetListAsync()
    {
        var result = await _unitOfWork.Company.GetListAsync();
        return result.EntityToModel<IEnumerable<Company>, List<CompanyModel>>();
    }

    public async Task<Result<CompanyModel>> GetAsync(Guid id)
    {
        var result = await _unitOfWork.Company.GetAsync(id);
        return result.EntityToModel<Company, CompanyModel>();
    }

    public async Task<Result<CompanyModel>> CreateAsync(AddOrUpdateCompanyModel addOrUpdateCompanyModel)
    {
        var companyResult = await _unitOfWork.Company.GetAsync(addOrUpdateCompanyModel.Name);
        if(companyResult.IsSuccess)
        {
            return Result.Fail<CompanyModel>("Company with the same name already exists");
        }
        var result = await _unitOfWork.Company.CreateAsync(addOrUpdateCompanyModel.Adapt<Company>());
        return result.EntityToModel<Company, CompanyModel>();
    }
    
    public async Task<Result<CompanyModel>> UpdateAsync(Guid id, AddOrUpdateCompanyModel addOrUpdateCompanyModel)
    {
        var companyResult = await _unitOfWork.Company.GetAsync(id);
        if(companyResult.IsFailed)
        {
            return companyResult.ToResult<CompanyModel>();
        }
        var result = await _unitOfWork.Company.UpdateAsync(id, addOrUpdateCompanyModel.Adapt<Company>());
        return result.EntityToModel<Company, CompanyModel>();
    }
    
    public async Task<Result> DeleteAsync(Guid id)
    {
        var companyResult = await _unitOfWork.Company.GetAsync(id);
        if(companyResult.IsFailed)
        {
            return companyResult.ToResult();
        }
        var result = await _unitOfWork.Company.DeleteAsync(id);
        return result;
    }
    
    public async Task<Result<List<CompanyModel>>> GetByUserIdAsync(Guid userId)
    {
        var result = await _unitOfWork.Company.GetByUserIdAsync(userId);
        return result.EntityToModel<IEnumerable<Company>, List<CompanyModel>>();
    }
}