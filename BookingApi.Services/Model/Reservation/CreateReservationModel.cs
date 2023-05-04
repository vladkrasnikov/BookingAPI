namespace BookingApi.Services.Model.Reservation;

public class CreateReservationModel
{
    public Guid PerformerId { get; set; }
    
    public Guid UserId { get; set; }
    
    public DateTime StartDate { get; set; }
    
    public DateTime EndDate { get; set; }
    
    public string UserEmailAddress { get; set; }
}