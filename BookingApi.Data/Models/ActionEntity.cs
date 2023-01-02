using System;

namespace BookingApi.Data.Models
{
    public class ActionEntity
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public int BookTime { get; set; }
    }
}