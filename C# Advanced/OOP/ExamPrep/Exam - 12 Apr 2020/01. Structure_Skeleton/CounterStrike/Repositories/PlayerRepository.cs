using CounterStrike.Models.Players.Contracts;
using CounterStrike.Repositories.Contracts;
using CounterStrike.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CounterStrike.Repositories
{
    public class PlayerRepository : IRepository<IPlayer>
    {
        private readonly HashSet<IPlayer> players;
        public PlayerRepository()
        {
            this.players = new HashSet<IPlayer>();
        }
        public IReadOnlyCollection<IPlayer> Models => players;
        public void Add(IPlayer model)
        {
            if (model == null)
            {
                throw new ArgumentException(ExceptionMessages.InvalidPlayerRepository);
            }

            players.Add(model);
        }

        public bool Remove(IPlayer model)
        {
            if (!players.Contains(model)) return false;

            players.Remove(model);
            return true;
        }

        public IPlayer FindByName(string name)
        {
            var player = players.FirstOrDefault(p => p.Username == name);
            return player;
        }
    }
}