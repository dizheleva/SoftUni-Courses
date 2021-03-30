using System;
using System.Collections.Generic;
using System.Linq;

namespace Raiding
{
    class Program
    {
        static void Main(string[] args)
        {
            var heroes = new List<BaseHero>();
            var n = int.Parse(Console.ReadLine());
            for (var i = 0; i < n; i++)
            {
                var name = Console.ReadLine();
                var type = Console.ReadLine();

                HeroFactory factory = null;
                switch (type)
                {
                    case "Druid":
                        factory = new DruidFactory(name);
                        break;
                    case "Paladin":
                        factory = new PaladinFactory(name);
                        break;
                    case "Rogue":
                        factory = new RogueFactory(name);
                        break;
                    case "Warrior":
                        factory = new WarriorFactory(name);
                        break;
                    default:
                        Console.WriteLine("Invalid hero!");
                        i--;
                        continue;
                }

                var hero = factory.MakeHero();
                heroes.Add(hero);
            }

            var bossPower = int.Parse(Console.ReadLine());
            foreach (var hero in heroes)
            {
                Console.WriteLine(hero.CastAbility());
            }
            var totalPower = heroes.Sum(hero => hero.Power);

            Console.WriteLine(totalPower >= bossPower ? "Victory!" : "Defeat...");
        }
    }
}
