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

    public async Task<Result<PerformerModel>> CreateAsync(CreatePerformerModel createPerformerModel)
    {
        var performer = createPerformerModel.Adapt<Performer>();
        var createdPerformer = await _performerRepository.CreateAsync(performer);
        return Result.Ok(createdPerformer.Adapt<PerformerModel>());
    }
}