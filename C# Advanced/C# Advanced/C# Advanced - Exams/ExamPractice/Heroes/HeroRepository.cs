using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Heroes
{
    public class HeroRepository
    {
        private readonly HashSet<Hero> data;
        public int Count => data.Count;
        public HeroRepository()
        {
            data = new HashSet<Hero>();
        }
        public void Add(Hero hero)
        {
            data.Add(hero);
        }

        public void Remove(string name)
        {
            data.Remove(data.First(p => p.Name == name));
        }
        public Hero GetHeroWithHighestStrength()
        {
            return data.First(p => p.Item.Strength == data.Select(h => h.Item.Strength).Max());
        }
        public Hero GetHeroWithHighestAbility()
        {
            return data.First(p => p.Item.Ability == data.Select(h => h.Item.Ability).Max());
        }
        public Hero GetHeroWithHighestIntelligence()
        {
            return data.First(p => p.Item.Intelligence == data.Select(h => h.Item.Intelligence).Max());
        }
        public override string ToString()
        {
            var sb = new StringBuilder();
            foreach (var hero in data)
            {
                sb.AppendLine(hero.ToString());
            }

            return sb.ToString().TrimEnd();
        }
    }
}
