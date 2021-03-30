namespace Raiding
{
    public class DruidFactory : HeroFactory
    {
        private string _name;

        public DruidFactory(string name)
        {
            _name = name;
        }
        public override BaseHero MakeHero()
        {
            return new Druid(_name);
        }
    }
}
