using BookingApi.Services.Model.Reservation;

namespace BookingApi.Services.Model.Performer;

public class PerformerModel
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public short WorkingHourStart { get; set; }
    
    public short WorkingHourEnd { get; set; }

    [System.Text.Json.Serialization.JsonIgnore]
    public Guid BrandId { get; set; }

    public IEnumerable<ReservationModel> Reservations { get; set; }
}