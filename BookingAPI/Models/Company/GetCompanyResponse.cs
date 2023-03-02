using BookingApi.Services.Model.Brand;

namespace BookingAPI.Models.Company;

public class GetCompanyResponse
{
    public string Id { get; set; }

    public string Name { get; set; }

    public string ShortName { get; set; }

    public string Description { get; set; }
    
    public IEnumerable<BrandModel> Brand { get; set; }
}