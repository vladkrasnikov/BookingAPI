namespace BookingApi.Services.Model.Reservation;

public class GetReservationModel
{
    public string UserEmailAddress { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }
}