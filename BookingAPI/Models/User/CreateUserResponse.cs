namespace BookingAPI.Models.User;

public class CreateUserResponse
{
    public Guid Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string EmailAddress { get; set; }
    public string Password { get; set; }

    public bool Blocked { get; set; }
}