using BookingApi.Services.Model.Brand;

namespace BookingApi.Services.Model.Company;

public class CompanyModel
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public string Name { get; set; }

    public string ShortName { get; set; }

    public string Description { get; set; }
    
    public IEnumerable<BrandModel> Brand { get; set; }
}