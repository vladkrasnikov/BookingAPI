using BookingApi.Services.Model.Company;
using BookingApi.Services.Model.Reservation;

namespace BookingApi.Services.Model.Performer;

public class PerformerModel
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public int ReservationTime { get; set; }

    public Guid CompanyId { get; set; }

    public CompanyModel Company { get; set; }

    public IEnumerable<ReservationModel> Reservations { get; set; }
}