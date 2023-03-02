namespace BookingAPI.Models.Company;

public class CreateCompanyRequest
{
    public Guid UserId { get; set; }
    
    public string Name { get; set; }

    public string ShortName { get; set; }

    public string Description { get; set; }
}