using CounterStrike.Models.Maps.Contracts;
using CounterStrike.Models.Players;
using CounterStrike.Models.Players.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace CounterStrike.Models.Maps
{
    public class Map : IMap
    {
        public string Start(ICollection<IPlayer> players)
        {
            var terrorists = players.Where(p => p.GetType().Name == nameof(Terrorist) && p.IsAlive).ToHashSet();
            var counterTerrorists = players.Where(p => p.GetType().Name == nameof(CounterTerrorist) && p.IsAlive).ToHashSet();

            while (terrorists.Any(t=>t.IsAlive && counterTerrorists.Any(c=>c.IsAlive)))
            {
                foreach (var terrorist in terrorists.Where(t => t.IsAlive))
                {
                    foreach (var counterTerrorist in counterTerrorists.Where(c => c.IsAlive))
                    {
                        counterTerrorist.TakeDamage(terrorist.Gun.Fire());
                    }
                }

                foreach (var counterTerrorist in counterTerrorists.Where(c => c.IsAlive))
                {
                    foreach (var terrorist in terrorists.Where(t => t.IsAlive))
                    {
                        terrorist.TakeDamage(counterTerrorist.Gun.Fire());
                    }
                }
            }

            return terrorists.Any(t => t.IsAlive) ? "Terrorist wins!" : "Counter Terrorist wins!";
        }
    }
}