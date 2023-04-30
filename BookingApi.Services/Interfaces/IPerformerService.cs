using BookingApi.Services.Model.Performer;
using FluentResults;

namespace BookingApi.Services.Interfaces;

public interface IPerformerService
{
    public Task<Result<List<PerformerModel>>> GetByBrandIdAsync(Guid brandId);

    public Task<Result<PerformerModel>> GetAsync(Guid id);
    
    public Task<Result<PerformerModel>> CreateAsync(CreatePerformerModel createPerformerModel);
}