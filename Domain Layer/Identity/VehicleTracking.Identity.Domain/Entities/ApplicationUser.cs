using Microsoft.AspNetCore.Identity;
using System;

namespace VehicleTracking.Identity.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public long ApplicationUserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int UsernameChangeLimit { get; set; } = 10;
        public byte[] ProfilePicture { get; set; }
        public DateTime DateRegistered { get; set; } = DateTime.Now;
    }
}
