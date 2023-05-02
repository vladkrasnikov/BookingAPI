using BookingApi.Data.Interfaces.Repository;
using BookingApi.Data.Models;
using BookingApi.Services.Interfaces;
using BookingApi.Services.Model.Performer;
using FluentResults;
using Mapster;

namespace BookingApi.Services.Services;

public class PerformerService : IPerformerService
{
    private readonly IPerformerRepository _performerRepository;

    public PerformerService(IPerformerRepository performerRepository)
    {
        _performerRepository = performerRepository;
    }

    public async Task<Result<List<PerformerModel>>> GetByBrandIdAsync(Guid brandId)
    {
        var performers = await _performerRepository.GetByBrandIdAsync(brandId);
        return Result.Ok(performers.Adapt<List<PerformerModel>>());
    }

    public async Task<Result<PerformerModel>> GetAsync(Guid id)
    {
        var performer = await _performerRepository.GetAsync(id);
        if (performer == null)
        {
            return Result.Fail<PerformerModel>($"Performer with id {id} not found");
        }

        return Result.Ok(performer.Adapt<PerformerModel>());
    }

    public async Task<Result<PerformerModel>> CreateAsync(AddOrUpdatePerformerModel addOrUpdatePerformerModel)
    {
        var performer = addOrUpdatePerformerModel.Adapt<Performer>();
        var createdPerformer = await _performerRepository.CreateAsync(performer);
        return Result.Ok(createdPerformer.Adapt<PerformerModel>());
    }
    
    public async Task<Result<PerformerModel>> UpdateAsync(Guid id, AddOrUpdatePerformerModel addOrUpdatePerformerModel)
    {
        var performer = await _performerRepository.GetAsync(id);
        if (performer == null)
        {
            return Result.Fail<PerformerModel>($"Performer with id {id} not found");
        }
        performer = addOrUpdatePerformerModel.Adapt(performer);
        var updatedPerformer = await _performerRepository.UpdateAsync(id, performer);
        return Result.Ok(updatedPerformer.Adapt<PerformerModel>());
    }
    
    public async Task<Result> DeleteAsync(Guid id)
    {
        var performer = await _performerRepository.GetAsync(id);
        if (performer == null)
        {
            return Result.Fail($"Performer with id {id} not found");
        }
        await _performerRepository.DeleteAsync(id);
        return Result.Ok();
    }
}