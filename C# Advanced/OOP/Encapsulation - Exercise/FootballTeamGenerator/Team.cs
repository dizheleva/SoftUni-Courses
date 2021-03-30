using System;
using System.Collections.Generic;
using System.Text;

namespace FootballTeamGenerator
{
    public class Team
    {
        private Dictionary<string, Player> players = new Dictionary<string, Player>();

        private string name;

        public Team(string name)
        {
            this.Name = name;
        }

        public string Name
        {
            get => name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("A name should not be empty.");
                }
                name = value;
            }
        }

        public void AddPlayer(Player player)
        {
            if (!players.ContainsKey(player.Name))
            {
                players.Add(player.Name, player);
            }
        }

        public void RemovePlayer(string player)
        {
            if (!players.ContainsKey(player))
            {
                throw new ArgumentException($"Player {player} is not in {this.name} team.");
            }

            players.Remove(player);
        }

        public void PrintRating()
        {
            double sum = 0;

            foreach (var (name, player) in players)
            {
                sum += player.SkillLevel;
            }

            var rating = (int)Math.Round(sum / players.Count);

            if (rating < 0)
            {
                rating = 0;
            }

            Console.WriteLine($"{this.name} - {rating}");
        }
    }
}
