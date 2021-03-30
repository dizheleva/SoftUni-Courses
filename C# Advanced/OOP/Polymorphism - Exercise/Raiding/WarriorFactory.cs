namespace Raiding
{
    public class WarriorFactory : HeroFactory
    {
        private string _name;

        public WarriorFactory(string name)
        {
            _name = name;
        }
        public override BaseHero MakeHero()
        {
            return new Warrior(_name);
        }
    }
}
