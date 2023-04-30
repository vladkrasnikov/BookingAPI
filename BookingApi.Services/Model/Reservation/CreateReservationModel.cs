namespace BookingApi.Services.Model.Reservation;

public class CreateReservationModel
{
    public Guid PerformerId { get; set; }
    
    public Guid UserId { get; set; }
    
    public DateTime Start { get; set; }
    
    public DateTime End { get; set; }
    
    public string UserEmailAddress { get; set; }
    
    public string? Comment { get; set; }
}