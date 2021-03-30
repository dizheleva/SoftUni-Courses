using CounterStrike.Core.Contracts;
using CounterStrike.Models.Guns;
using CounterStrike.Models.Guns.Contracts;
using CounterStrike.Models.Maps.Contracts;
using CounterStrike.Models.Players;
using CounterStrike.Models.Players.Contracts;
using CounterStrike.Repositories;
using CounterStrike.Utilities.Messages;
using System;
using System.Linq;
using System.Text;
using CounterStrike.Models.Maps;

namespace CounterStrike.Core
{
    public class Controller : IController
    {
        private GunRepository guns;
        private PlayerRepository players;
        private IMap map;
        public Controller()
        {
            this.guns = new GunRepository();
            this.players = new PlayerRepository();
            this.map = new Map();
        }

        public string AddGun(string type, string name, int bulletsCount)
        {
            var gun = type switch
            {
                nameof(Pistol) => (IGun) new Pistol(name, bulletsCount),
                nameof(Rifle) => new Rifle(name, bulletsCount),
                _ => throw new ArgumentException(ExceptionMessages.InvalidGunType)
            };

            this.guns.Add(gun);
            return string.Format(OutputMessages.SuccessfullyAddedGun, name);
        }

        public string AddPlayer(string type, string username, int health, int armor, string gunName)
        {
            var gun = guns.FindByName(gunName);
            if (gun==null)
            {
                throw new ArgumentException(ExceptionMessages.GunCannotBeFound);
            }

            var player = type switch
            {
                nameof(Terrorist) => (IPlayer)new Terrorist(username, health, armor, gun),
                nameof(CounterTerrorist) => new CounterTerrorist(username, health, armor, gun),
                _ => throw new ArgumentException(ExceptionMessages.InvalidPlayerType)
            };

            this.players.Add(player);
            return string.Format(OutputMessages.SuccessfullyAddedPlayer, player.Username);
        }

        public string StartGame()
        {
            return this.map.Start(players.Models.ToHashSet());
        }

        public string Report()
        {
            var played = players
                .Models
                .OrderBy(p => p.GetType().Name)
                .ThenByDescending(p => p.Health)
                .ThenBy(p => p.Username)
                .ToHashSet();

            var result = new StringBuilder();

            foreach (var player in played)
            {
                result.AppendLine(player.ToString());
            }
            return result.ToString().TrimEnd();
        }
    }
}