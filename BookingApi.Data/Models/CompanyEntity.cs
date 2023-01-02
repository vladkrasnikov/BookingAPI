using System;

namespace BookingApi.Data.Models
{
    public class CompanyEntity
    {
        public Guid Id { get; set; }
        public Guid BrandId { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
    }
}