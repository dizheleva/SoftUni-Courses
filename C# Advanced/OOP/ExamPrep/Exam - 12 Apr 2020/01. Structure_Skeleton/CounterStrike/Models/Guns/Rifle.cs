namespace CounterStrike.Models.Guns
{
    public class Rifle : Gun
    {
        public Rifle(string name, int bulletCount) : base(name, bulletCount)
        {
        }

        public override int Fire()
        {
            if (this.BulletsCount< 10)
            {
                return 0;
            }

            this.BulletsCount -= 10;
            return 10;
        }
    }
}