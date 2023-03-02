namespace BookingApi.Services.Model.Company;

public class CreateCompanyModel
{
    public Guid UserId { get; set; }
    
    public string Name { get; set; }

    public string ShortName { get; set; }

    public string Description { get; set; }
}