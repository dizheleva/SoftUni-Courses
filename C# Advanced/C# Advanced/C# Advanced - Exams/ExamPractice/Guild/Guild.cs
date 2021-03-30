using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Guild
{
    public class Guild : Player
    {
        private HashSet<Player> roster;
        public string Name { get; set; }
        public int Capacity { get; set; }
        public int Count => roster.Count;

        private Guild()
        {
            roster = new HashSet<Player>();
        }
        public Guild(string name, int capacity) : this()
        {
            Name = name;
            Capacity = capacity;
        }

        public void AddPlayer(Player player)
        {
            if (Capacity > roster.Count)
            {
                roster.Add(player);
            }
        }

        public bool RemovePlayer(string name)
        {
            roster.
            var playerIsRemoved = roster.Any(p => p.Name == name);
            if (playerIsRemoved)
            {
                roster.Remove(roster.First(p => p.Name == name));
            }
            return playerIsRemoved;
        }

        public void PromotePlayer(string name)
        {
            var player = roster.First(p => p.Name == name);
            if (player.Rank != "Member")
            {
                player.Rank = "Member";
            }
        }

        public void DemotePlayer(string name)
        {
            var player = roster.First(p => p.Name == name);
            if (player.Rank != "Trial")
            {
                player.Rank = "Trial";
            }
        }
        public Player[] KickPlayersByClass(string Class)
        { 
            var kickedPlayers = roster.Where(p => p.Class == Class).ToArray();
            roster = roster.Where(p => p.Class != Class).ToHashSet();
            return kickedPlayers;
        }

        public string Report()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Players in the guild: {Name}");
            sb.AppendLine($"{ string.Join(Environment.NewLine, roster)}");

            return sb.ToString().TrimEnd();
            //return $"Players in the guild: {Name}Environment.NewLine{string.Join('\n', roster)}";
        }
    }
}
