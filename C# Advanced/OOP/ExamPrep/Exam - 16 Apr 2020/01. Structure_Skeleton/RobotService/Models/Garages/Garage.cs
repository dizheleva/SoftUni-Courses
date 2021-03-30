using RobotService.Models.Garages.Contracts;
using RobotService.Models.Robots.Contracts;
using RobotService.Utilities.Messages;
using System;
using System.Collections.Generic;

namespace RobotService.Models.Garages
{
    public class Garage : IGarage
    {
        private const int MaxCapacity = 10;
        private readonly Dictionary<string, IRobot> _robots;
        public Garage()
        {
            this._robots = new Dictionary<string, IRobot>();
        }
        public IReadOnlyDictionary<string, IRobot> Robots => this._robots;
        public int Capacity => MaxCapacity;
        public void Manufacture(IRobot robot)
        {
            if (this._robots.Count==this.Capacity)
            {
                throw new InvalidOperationException(ExceptionMessages.NotEnoughCapacity);
            }

            if (this._robots.ContainsKey(robot.Name))
            {
                throw new ArgumentException(string.Format(ExceptionMessages.ExistingRobot, robot.Name));
            }

            this._robots.Add(robot.Name, robot);
        }

        public void Sell(string robotName, string ownerName)
        {
            if (!this._robots.ContainsKey(robotName))
            {
                throw new ArgumentException(string.Format(ExceptionMessages.InexistingRobot, robotName));
            }

            IRobot robot = this._robots[robotName];
            robot.IsBought = true;
            robot.Owner = ownerName;
            this._robots.Remove(robotName);
        }
    }
}
