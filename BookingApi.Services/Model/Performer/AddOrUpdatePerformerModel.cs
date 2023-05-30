namespace BookingApi.Services.Model.Performer;

public class AddOrUpdatePerformerModel
{
    public string Name { get; set; }

    public string Description { get; set; }

    public short WorkingHourStart { get; set; }

    public short WorkingHourEnd { get; set; }

    public short Rating { get; set; }

    public Guid BrandId { get; set; }
}