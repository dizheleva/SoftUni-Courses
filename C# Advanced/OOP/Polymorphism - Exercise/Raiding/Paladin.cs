﻿namespace Raiding
{
    public class Paladin : BaseHero
    {
        private const int DefaultPower = 100;
        public Paladin(string name) : base(name)
        {
        }
        public override int Power => DefaultPower;
        public override string CastAbility()
        {
            return $"{this.GetType().Name} - {this.Name} healed for {this.Power}";
        }
    }
}
