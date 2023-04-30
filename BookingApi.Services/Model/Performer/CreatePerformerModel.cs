namespace BookingApi.Services.Model.Performer;

public class CreatePerformerModel
{
    public string Name { get; set; }

    public string Description { get; set; }

    public short WorkingHourStart { get; set; }

    public short WorkingHourEnd { get; set; }

    public Guid BrandId { get; set; }
}