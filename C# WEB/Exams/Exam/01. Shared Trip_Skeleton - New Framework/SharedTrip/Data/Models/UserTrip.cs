using System;
using System.ComponentModel.DataAnnotations;

namespace SharedTrip.Data.Models
{
    public class UserTrip
    {
        public UserTrip()
        {
            this.UserId = Guid.NewGuid().ToString();
            this.TripId = Guid.NewGuid().ToString();
        }

        [Required]
        public string UserId { get; set; }

        public User User { get; set; }

        [Required]
        public string TripId { get; set; }

        public Trip Trip { get; set; }
    }
}