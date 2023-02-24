﻿namespace BookingAPI.Models.Company;

public class CreateCompanyRequest
{
    public Guid BrandId { get; set; }

    public string Name { get; set; }

    public string ShortName { get; set; }

    public string Description { get; set; }

    public string Address { get; set; }
}