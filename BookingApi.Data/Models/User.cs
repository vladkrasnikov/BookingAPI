﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace BookingApi.Data.Models;

public partial class User
{
    public Guid Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string EmailAddress { get; set; }

    public bool Blocked { get; set; }

    public string Password { get; set; }

    public virtual ICollection<Company> Company { get; } = new List<Company>();

    public virtual ICollection<Reservation> Reservation { get; } = new List<Reservation>();
}