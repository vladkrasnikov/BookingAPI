﻿using BookingApi.Services.Model.Reservation;

namespace BookingApi.Services.Model.Performer;

public class PerformerModel
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public short WorkingHourStart { get; set; }
    
    public short WorkingHourEnd { get; set; }

    public short Rating { get; set; }

    [System.Text.Json.Serialization.JsonIgnore]
    public Guid BrandId { get; set; }

    public IEnumerable<ReservationModel> Reservation { get; set; }
}