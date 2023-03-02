using BookingApi.Services.Model.Performer;

namespace BookingAPI.Models.Brand;

public abstract class GetBrandResponse
{    
    public Guid Id { get; set; }

    public Guid CompanyId { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public string Address { get; set; }

    public IEnumerable<PerformerModel> Performer { get; set; }
}