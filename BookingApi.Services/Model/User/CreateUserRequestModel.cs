namespace BookingApi.Services.Model.User;

public class CreateUserRequestModel
{
    public string EmailAddress { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Password { get; set; }
    public short Role { get; set; }
}