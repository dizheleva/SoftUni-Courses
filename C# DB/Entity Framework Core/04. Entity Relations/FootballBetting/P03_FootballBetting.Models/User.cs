using System.Collections.Generic;

namespace P03_FootballBetting.Models
{
    public class User
    {
        public int UserId { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        //[EmailAddress]
        public string Email { get; set; }

        public string Name { get; set; }

        public decimal Balance { get; set; }

        public ICollection<Bet> Bets { get; set; }
    }
}
