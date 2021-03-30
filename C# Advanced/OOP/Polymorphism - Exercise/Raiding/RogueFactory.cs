namespace Raiding
{
    public class RogueFactory : HeroFactory
    {
        private string _name;

        public RogueFactory(string name)
        {
            _name = name;
        }
        public override BaseHero MakeHero()
        {
            return new Rogue(_name);
        }
    }
}
