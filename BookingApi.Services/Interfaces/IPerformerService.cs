using BookingApi.Services.Model.Performer;
using FluentResults;

namespace BookingApi.Services.Interfaces;

public interface IPerformerService
{
    public Task<Result<List<PerformerModel>>> GetByBrandIdAsync(Guid brandId);

    public Task<Result<PerformerModel>> GetAsync(Guid id);
    
    public Task<Result<PerformerModel>> CreateAsync(AddOrUpdatePerformerModel addOrUpdatePerformerModel);
    
    public Task<Result<PerformerModel>> UpdateAsync(Guid id, AddOrUpdatePerformerModel addOrUpdatePerformerModel);
    
    public Task<Result> DeleteAsync(Guid id);
}