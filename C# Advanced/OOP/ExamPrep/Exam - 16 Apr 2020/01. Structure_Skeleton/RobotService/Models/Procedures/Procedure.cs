using RobotService.Models.Procedures.Contracts;
using RobotService.Models.Robots.Contracts;
using RobotService.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace RobotService.Models.Procedures
{
    public abstract class Procedure : IProcedure
    {
        protected ICollection<IRobot> robots;
        protected Procedure()
        {
            this.robots = new List<IRobot>();
        }
        public ICollection<IRobot> Robots => this.robots;
        public string History()
        {
            var result = new StringBuilder();
            result.AppendLine($"{this.GetType().Name}");

            foreach (var robot in this.robots)
            {
                result.AppendLine(robot.ToString());
            }

            return result.ToString().TrimEnd();
        }

        public virtual void DoService(IRobot robot, int procedureTime)
        {
            if(robot.ProcedureTime < procedureTime)
            {
                throw new ArgumentException(ExceptionMessages.InsufficientProcedureTime);
            }

            robot.ProcedureTime -= procedureTime;
        }
    }
}
