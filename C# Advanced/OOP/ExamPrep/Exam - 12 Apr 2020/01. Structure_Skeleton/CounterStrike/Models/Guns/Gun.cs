using System;
using CounterStrike.Models.Guns.Contracts;
using CounterStrike.Utilities.Messages;

namespace CounterStrike.Models.Guns
{
    public abstract class Gun : IGun
    {
        private string name;
        private int bulletsCount;

        protected Gun(string name, int bulletCount)
        {
            this.Name = name;
            this.BulletsCount = bulletCount;
        }

        public string Name //⦁	All names are unique 
        {
            get => this.name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    var message = ExceptionMessages.InvalidGunName;
                    throw new ArgumentException(message);
                }

                this.name = value;
            }
        }
        public int BulletsCount
        {
            get => this.bulletsCount;
            protected set
            {
                if (value < 0)
                {
                    var message = ExceptionMessages.InvalidGunBulletsCount;
                    throw new ArgumentException(message);
                }

                this.bulletsCount = value;
            }
        }

        public abstract int Fire();
    }
}