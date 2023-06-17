namespace BookingApi.Services.Model.User;

public class UpdateUserRequestModel
{
    public Guid Id { get; set; }
    
    public string? FirstName { get; set; } = String.Empty;
    
    public string? LastName { get; set; } = String.Empty;
    
    public string? Image { get; set; } = String.Empty;
}