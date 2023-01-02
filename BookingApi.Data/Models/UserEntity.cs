using System;

namespace BookingApi.Data.Models
{
    public class UserEntity
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Auth0Id { get; set; }
        public bool Blocked { get; set; }
        public bool Pending { get; set; }
        public bool Deleted { get; set; }
    }
}