namespace BookingAPI.Models.User;

public class UpdateUserRequest
{
    public Guid Id { get; set; }
    
    public string? FirstName { get; set; }
    
    public string? LastName { get; set; }
    
    public string? Image { get; set; }
}