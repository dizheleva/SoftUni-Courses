using RobotService.Models.Garages;
using RobotService.Models.Garages.Contracts;
using RobotService.Models.Procedures;
using RobotService.Models.Robots;
using RobotService.Models.Robots.Contracts;
using RobotService.Utilities.Messages;
using System;

namespace RobotService.Core.Contracts
{
    public class Controller : IController
    {
        private readonly IGarage _garage;
        private readonly Chip _chip;
        private readonly TechCheck _techCheck;
        private readonly Work _work;
        private readonly Rest _rest;
        private readonly Charge _charge;
        private readonly Polish _polish;
        private IRobot _currentRobot;
        public Controller()
        {
            this._garage = new Garage();
            this._chip = new Chip();
            this._techCheck = new TechCheck();
            this._rest = new Rest();
            this._work = new Work();
            this._charge = new Charge();
            this._polish = new Polish();
        }
        public string Manufacture
            (string robotType, string name, int energy, int happiness, int procedureTime)
        {
            this._currentRobot = robotType switch
            {
                nameof(HouseholdRobot) => new HouseholdRobot(name, energy, happiness, procedureTime),
                nameof(PetRobot) => new PetRobot(name, energy, happiness, procedureTime),
                nameof(WalkerRobot) => new WalkerRobot(name, energy, happiness, procedureTime),
                _ => throw new ArgumentException(string.Format(ExceptionMessages.InvalidRobotType, robotType))
            };

            this._garage.Manufacture(this._currentRobot);

            return string.Format(OutputMessages.RobotManufactured, _currentRobot.Name);
        }

        public string Chip(string robotName, int procedureTime)
        {
            ExecuteProcedure(robotName, procedureTime, this._chip);

            return string.Format(OutputMessages.ChipProcedure, robotName);
        }

        public string TechCheck(string robotName, int procedureTime)
        {
            ExecuteProcedure(robotName, procedureTime, this._techCheck);

            return string.Format(OutputMessages.TechCheckProcedure, robotName);
        }

        public string Rest(string robotName, int procedureTime)
        {
            ExecuteProcedure(robotName, procedureTime, this._rest);

            return string.Format(OutputMessages.RestProcedure, robotName);
        }

       

        public string Work(string robotName, int procedureTime)
        {
            ExecuteProcedure(robotName, procedureTime, this._work);

            return string.Format(OutputMessages.WorkProcedure, robotName, procedureTime);
        }

        public string Charge(string robotName, int procedureTime)
        {
            ExecuteProcedure(robotName, procedureTime, this._charge);

            return string.Format(OutputMessages.ChargeProcedure, robotName);
        }

        public string Polish(string robotName, int procedureTime)
        {
            ExecuteProcedure(robotName, procedureTime, this._polish);

            return string.Format(OutputMessages.PolishProcedure, robotName);
        }

        public string Sell(string robotName, string ownerName)
        {
            if (!this._garage.Robots.ContainsKey(robotName))
            {
                throw new ArgumentException(string.Format(ExceptionMessages.InexistingRobot, robotName));
            }

            this._currentRobot = this._garage.Robots[robotName];
            this._garage.Sell(robotName, ownerName);

            return string.Format(this._currentRobot.IsChipped ? OutputMessages.SellChippedRobot : OutputMessages.SellNotChippedRobot, ownerName);
        }

        public string History(string procedureType)
        {
            Procedure procedure = procedureType switch
            {
                nameof(Chip) => this._chip,
                nameof(Charge) => this._charge,
                nameof(Rest) => this._rest,
                nameof(Polish) => this._polish,
                nameof(Work) => this._work,
                nameof(TechCheck) => this._techCheck
            };
            return procedure.History().TrimEnd();
        }

        private void ExecuteProcedure(string robotName, int procedureTime, Procedure procedure)
        {
            if (!this._garage.Robots.ContainsKey(robotName))
            {
                throw new ArgumentException(string.Format(ExceptionMessages.InexistingRobot, robotName));
            }

            this._currentRobot = this._garage.Robots[robotName];
            procedure.Robots.Add(_currentRobot);

            procedure.DoService(this._currentRobot, procedureTime);
        }
    }
}
