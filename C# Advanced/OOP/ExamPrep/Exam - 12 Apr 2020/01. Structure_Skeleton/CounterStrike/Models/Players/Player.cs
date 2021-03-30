using System;
using System.Text;
using CounterStrike.Models.Guns.Contracts;
using CounterStrike.Models.Players.Contracts;
using CounterStrike.Utilities.Messages;

namespace CounterStrike.Models.Players
{
    public abstract class Player : IPlayer
    {
        private string _username;
        private int _health;
        private int _armor;
        private IGun _gun;

        protected Player(string username, int health, int armor, IGun gun)
        {
            this.Username = username;
            this.Health = health;
            this.Armor = armor;
            this.Gun = gun;
        }
        public string Username 
        {
            get => this._username;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    var message = ExceptionMessages.InvalidPlayerName;
                    throw new ArgumentException(message);
                }

                this._username = value;
            }
        }

        public int Health
        {
            get => this._health;
            private set
            {
                if (value <= 0)
                {
                    var message = ExceptionMessages.InvalidPlayerHealth;
                    throw new ArgumentException(message);
                }

                this._health = value;
            }
        }

        public int Armor
        {
            get => this._armor;
            private set
            {
                if (value < 0)
                {
                    var message = ExceptionMessages.InvalidPlayerArmor;
                    throw new ArgumentException(message);
                }

                this._armor = value;
            }
        }
        public IGun Gun
        {
            get => this._gun;
            private set
            {
                if (value == null)
                {
                    var message = ExceptionMessages.InvalidGun;
                    throw new ArgumentException(message);
                }

                this._gun = value;
            }
        }
        public bool IsAlive => this.Health > 0;

        public void TakeDamage(int points)
        {
            if (this._armor>=points)
            {
                this._armor -= points;
            }
            else
            {
                if (this._armor+this._health>points)
                {
                    this._health = this._health + this._armor - points; 
                    this._armor = 0;
                }
                else
                {
                    this._health = 0;
                }
            }
        }

        public override string ToString()
        {
            var result = new StringBuilder();
            result.AppendLine($"{this.GetType().Name}: { this.Username}")
                .AppendLine($"--Health: {this.Health}")
                .AppendLine($"--Armor: {this.Armor}")
                .AppendLine($"--Gun: {this.Gun.Name}");
            return result.ToString().TrimEnd();
        }
    }
}