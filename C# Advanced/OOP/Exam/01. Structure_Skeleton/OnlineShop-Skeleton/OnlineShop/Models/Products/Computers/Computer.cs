using OnlineShop.Common.Constants;
using OnlineShop.Models.Products.Components;
using OnlineShop.Models.Products.Peripherals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OnlineShop.Models.Products.Computers
{
    public abstract class Computer : Product, IComputer
    {
        private readonly List<IComponent> components;
        private readonly List<IPeripheral> peripherals;
        protected Computer(int id, string manufacturer, string model, decimal price, double overallPerformance) 
            : base(id, manufacturer, model, price, overallPerformance: overallPerformance)
        {
            this.components = new List<IComponent>();
            this.peripherals = new List<IPeripheral>();
        }
        public override double OverallPerformance
        {
            get
            {
                if (!this.Components.Any())
                {
                    return base.OverallPerformance;
                }

                var componentsAveragePerformance = this.Components.Any() ? this.Components.Average(c => c.OverallPerformance) : 0;

                return base.OverallPerformance + componentsAveragePerformance;
            }
        }

        public override decimal Price
        {
            get
            {
                var componentsSum = this.Components.Any() ? this.Components.Sum(c => c.Price) : 0;
                var peripheralsSum = this.Peripherals.Any() ? this.Peripherals.Sum(p => p.Price) : 0;

                return base.Price + componentsSum + peripheralsSum;
            }
        }
        public IReadOnlyCollection<IComponent> Components => this.components.AsReadOnly();
        public IReadOnlyCollection<IPeripheral> Peripherals => this.peripherals.AsReadOnly();
        public void AddComponent(IComponent component)
        {
            var componentType = component.GetType().Name;
            if (this.components.Any(c => c.GetType().Name == componentType))
            {
                throw new ArgumentException(string.Format(ExceptionMessages.ExistingComponent, componentType, this.GetType().Name, this.Id));
            }

            this.components.Add(component);
        }

        public IComponent RemoveComponent(string componentType)
        {
            if (this.components.All(c => c.GetType().Name != componentType))
            {
                throw new ArgumentException(string.Format(ExceptionMessages.NotExistingComponent, componentType, this.GetType().Name, this.Id));
            }

            var component = this.components.First(c => c.GetType().Name == componentType);
            this.components.Remove(component);
            return component;
        }

        public void AddPeripheral(IPeripheral peripheral)
        {
            var peripheralType = peripheral.GetType().Name;
            if (this.peripherals.Any(c => c.GetType().Name == peripheralType))
            {
                throw new ArgumentException(string.Format(ExceptionMessages.ExistingPeripheral, peripheralType, this.GetType().Name, this.Id));
            }

            this.peripherals.Add(peripheral);
        }

        public IPeripheral RemovePeripheral(string peripheralType)
        {
            if (this.peripherals.All(c => c.GetType().Name != peripheralType))
            {
                throw new ArgumentException(string.Format(ExceptionMessages.NotExistingPeripheral, peripheralType, this.GetType().Name, this.Id));
            }

            var peripheral = this.peripherals.First(c => c.GetType().Name == peripheralType);
            this.peripherals.Remove(peripheral);
            return peripheral;
        }
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine(base.ToString());
            sb.AppendLine(" " + String.Format(SuccessMessages.ComputerComponentsToString, this.Components.Count));
            foreach (var component in this.Components)
            {
                sb.AppendLine("  " + component.ToString());
            }
            sb.AppendLine(" " + String.Format(SuccessMessages.ComputerPeripheralsToString, this.Peripherals.Count,
                this.Peripherals.Any() ? this.Peripherals.Average(p => p.OverallPerformance) : 0));
            foreach (var peripheral in this.Peripherals)
            {
                sb.AppendLine("  " + peripheral.ToString());
            }

            return sb.ToString().TrimEnd();
        }
    }
}