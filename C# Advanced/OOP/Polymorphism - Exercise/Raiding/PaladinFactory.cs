namespace Raiding
{
    public class PaladinFactory : HeroFactory
    {
        private string _name;

        public PaladinFactory(string name)
        {
            _name = name;
        }
        public override BaseHero MakeHero()
        {
            return new Paladin(_name);
        }
    }
}
