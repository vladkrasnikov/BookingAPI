﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace BookingApi.Data.Models;

public partial class Brand
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public Guid UserId { get; set; }

    public virtual ICollection<Company> Company { get; } = new List<Company>();

    public virtual User User { get; set; }
}