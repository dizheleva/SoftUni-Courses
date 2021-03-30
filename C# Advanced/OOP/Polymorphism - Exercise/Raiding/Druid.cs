﻿namespace Raiding
{
    public class Druid : BaseHero
    {
        private const int DefaultPower = 80;
        public Druid(string name) : base(name)
        {
        }
        public override int Power => DefaultPower;
        public override string CastAbility()
        {
            return $"{this.GetType().Name} - {this.Name} healed for {this.Power}";
        }
    }
}
