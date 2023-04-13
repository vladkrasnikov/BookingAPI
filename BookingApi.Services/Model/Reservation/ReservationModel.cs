using BookingApi.Services.Model.Performer;
using BookingApi.Services.Model.User;

namespace BookingApi.Services.Model.Reservation;

public class ReservationModel
{
    
    public Guid Id { get; set; }

    public string UserEmailAddress { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public Guid PerformerId { get; set; }

    [System.Text.Json.Serialization.JsonIgnore]
    public Guid UserId { get; set; }
}