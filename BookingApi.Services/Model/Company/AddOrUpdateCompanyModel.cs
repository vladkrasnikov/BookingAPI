namespace BookingApi.Services.Model.Company;

public class AddOrUpdateCompanyModel
{
    public Guid UserId { get; set; }
    
    public string Name { get; set; }

    public string ShortName { get; set; }

    public string Description { get; set; }
}