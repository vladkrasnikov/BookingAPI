using BookingApi.Services.Model.Company;
using BookingApi.Services.Model.User;

namespace BookingApi.Services.Model.Brand;

public class BrandModel
{
    
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public Guid UserId { get; set; }

    public IEnumerable<CompanyModel> Companies { get; set; }

    public UserModel User { get; set; }
}