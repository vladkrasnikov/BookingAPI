using BookingApi.Services.Model.Company;
using BookingApi.Services.Model.Reservation;

namespace BookingApi.Services.Model.User;

public class UserModel
{
    public Guid Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string EmailAddress { get; set; }
    
    public string Password { get; set; }

    public bool Blocked { get; set; }

    public IEnumerable<CompanyModel> Companies { get; set; }

    public IEnumerable<ReservationModel> Reservations { get; set; }
}