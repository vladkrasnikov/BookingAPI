namespace BookingAPI.Models.User;

public class SignInRequest
{
    public string EmailAddress { get; set; }
    public string Password { get; set; }
}