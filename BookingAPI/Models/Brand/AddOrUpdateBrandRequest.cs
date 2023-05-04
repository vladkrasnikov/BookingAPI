namespace BookingAPI.Models.Brand;

public class AddOrUpdateBrandRequest
{
    public Guid CompanyId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Address { get; set; }
    public string Image { get; set; }
}