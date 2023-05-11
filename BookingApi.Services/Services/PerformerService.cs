using BookingApi.Data.Helpers.Interfaces;
using BookingApi.Data.Models;
using BookingApi.Services.Interfaces;
using BookingApi.Services.Model.Performer;
using FluentResults;
using Mapster;

namespace BookingApi.Services.Services;

public class PerformerService : IPerformerService
{
    private readonly IUnitOfWork _unitOfWork;

    public PerformerService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<List<PerformerModel>>> GetByBrandIdAsync(Guid brandId)
    {
        var performers = await _unitOfWork.Performer.GetByBrandIdAsync(brandId);
        return Result.Ok(performers.Adapt<List<PerformerModel>>());
    }

    public async Task<Result<PerformerModel>> GetAsync(Guid id)
    {
        var performer = await _unitOfWork.Performer.GetAsync(id);
        if (performer == null)
        {
            return Result.Fail<PerformerModel>($"Performer with id {id} not found");
        }

        return Result.Ok(performer.Adapt<PerformerModel>());
    }

    public async Task<Result<PerformerModel>> CreateAsync(AddOrUpdatePerformerModel addOrUpdatePerformerModel)
    {
        var performer = addOrUpdatePerformerModel.Adapt<Performer>();
        var createdPerformer = await _unitOfWork.Performer.CreateAsync(performer);
        return Result.Ok(createdPerformer.Adapt<PerformerModel>());
    }
    
    public async Task<Result<PerformerModel>> UpdateAsync(Guid id, AddOrUpdatePerformerModel addOrUpdatePerformerModel)
    {
        var performer = await _unitOfWork.Performer.GetAsync(id);
        if (performer == null)
        {
            return Result.Fail<PerformerModel>($"Performer with id {id} not found");
        }
        performer = addOrUpdatePerformerModel.Adapt(performer);
        var updatedPerformer = await _unitOfWork.Performer.UpdateAsync(id, performer);
        return Result.Ok(updatedPerformer.Adapt<PerformerModel>());
    }
    
    public async Task<Result> DeleteAsync(Guid id)
    {
        var performer = await _unitOfWork.Performer.GetAsync(id);
        if (performer == null)
        {
            return Result.Fail($"Performer with id {id} not found");
        }
        await _unitOfWork.Performer.DeleteAsync(id);
        return Result.Ok();
    }
}