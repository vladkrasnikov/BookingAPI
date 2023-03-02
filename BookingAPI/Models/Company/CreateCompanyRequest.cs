namespace BookingAPI.Models.Company;

public class CreateCompanyRequest
{
    public string Name { get; set; }

    public string ShortName { get; set; }

    public string Description { get; set; }
}