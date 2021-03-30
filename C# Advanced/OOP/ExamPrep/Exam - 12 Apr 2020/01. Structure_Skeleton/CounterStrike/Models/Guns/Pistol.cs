namespace CounterStrike.Models.Guns
{
    public class Pistol : Gun
    {
        public Pistol(string name, int bulletCount) : base(name, bulletCount)
        {
        }

        public override int Fire()
        {
            if (this.BulletsCount < 1)
            {
                return 0;
            }

            this.BulletsCount -= 1;
            return 1;
        }
    }
}