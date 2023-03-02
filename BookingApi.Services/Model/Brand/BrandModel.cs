using BookingApi.Services.Model.Company;
using BookingApi.Services.Model.Performer;

namespace BookingApi.Services.Model.Brand;

public class BrandModel
{
    public Guid Id { get; set; }

    public Guid CompanyId { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public string Address { get; set; }

    public CompanyModel Company { get; set; }

    public IEnumerable<PerformerModel> Performers { get; set; }
}