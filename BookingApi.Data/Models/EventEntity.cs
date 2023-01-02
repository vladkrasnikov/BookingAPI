using System;

namespace BookingApi.Data.Models
{
    public class EventEntity
    {
        public Guid Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Guid CompanyId { get; set; }
        public bool Approved { get; set; }
    }
}