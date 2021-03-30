using RobotService.Models.Robots.Contracts;
using RobotService.Utilities.Messages;
using System;

namespace RobotService.Models.Robots
{
    public abstract class Robot : IRobot
    {
        private const string DefaultOwner = "Service";
        private const int MinValue = 0;
        private const int MaxValue = 100;

        private int _happiness;
        private int _energy;

        protected Robot(string name, int energy, int happiness, int procedureTime)
        {
            this.Name = name;
            this.Energy = energy;
            this.Happiness = happiness;
            this.ProcedureTime = procedureTime;
            this.Owner = DefaultOwner;
            this.IsBought = false;
            this.IsChecked = false;
            this.IsChipped = false;
        }
        public string Name { get; }
        public int Happiness 
        {
            get => this._happiness;
            set
            {
                if (value < MinValue || value > MaxValue)
                {
                    throw new ArgumentException(ExceptionMessages.InvalidHappiness);
                }

                this._happiness = value;
            }
        }
        public int Energy 
        {
            get => this._energy;
            set
            {
                if (value < MinValue || value > MaxValue)
                {
                    throw new ArgumentException(ExceptionMessages.InvalidEnergy);
                }

                this._energy = value;
            }
        }
        public int ProcedureTime { get; set; }
        public string Owner { get; set; }
        public bool IsBought { get; set; }
        public bool IsChipped { get; set; }
        public bool IsChecked { get; set; }

        public override string ToString()
        {
            return string.Format(OutputMessages.RobotInfo, this.GetType().Name, this.Name, this.Happiness, this.Energy);
        }
    }
}
