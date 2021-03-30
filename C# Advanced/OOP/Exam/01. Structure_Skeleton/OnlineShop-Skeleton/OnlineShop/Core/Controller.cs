using OnlineShop.Common.Constants;
using OnlineShop.Models.Products.Components;
using OnlineShop.Models.Products.Computers;
using OnlineShop.Models.Products.Peripherals;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineShop.Core
{
    public class Controller : IController
    {
        private List<IComputer> computers;
        private List<IComponent> components;
        private List<IPeripheral> peripherals;

        public Controller()
        {
            this.computers = new List<IComputer>();
            this.components = new List<IComponent>();
            this.peripherals = new List<IPeripheral>();
        }
        public string AddComputer(string computerType, int id, string manufacturer, string model, decimal price)
        {
            var computer = computerType switch
            {
                nameof(DesktopComputer) => (IComputer)new DesktopComputer(id, manufacturer, model, price),
                nameof(Laptop) => new Laptop(id, manufacturer, model, price),
                _ => throw new ArgumentException(ExceptionMessages.InvalidComputerType)
            };

            if (this.computers.Any(c => c.Id == id))
            {
                throw new ArgumentException(ExceptionMessages.ExistingComputerId);
            }

            this.computers.Add(computer);
            return string.Format(SuccessMessages.AddedComputer, id);
        }

        public string AddPeripheral(int computerId, int id, string peripheralType, string manufacturer, string model, decimal price,
            double overallPerformance, string connectionType)
        {
            var computer = CheckExistence(computerId);
            var peripheral = peripheralType switch
            {
                nameof(Headset) => (IPeripheral)new Headset(id, manufacturer, model, price, overallPerformance, connectionType),
                nameof(Keyboard) => new Keyboard(id, manufacturer, model, price, overallPerformance, connectionType),
                nameof(Monitor) => new Monitor(id, manufacturer, model, price, overallPerformance, connectionType),
                nameof(Mouse) => new Mouse(id, manufacturer, model, price, overallPerformance, connectionType),
                _ => throw new ArgumentException(ExceptionMessages.InvalidPeripheralType)
            };

            if (computer.Peripherals.Any(c => c.Id == id))
            {
                throw new ArgumentException(ExceptionMessages.ExistingPeripheralId);
            }

            this.peripherals.Add(peripheral);
            computer.AddPeripheral(peripheral);

            return string.Format(SuccessMessages.AddedPeripheral, peripheralType, id, computerId);
        }

        public string RemovePeripheral(string peripheralType, int computerId)
        {
            var computer = CheckExistence(computerId);
            computer.RemovePeripheral(peripheralType);

            var peripheral = peripherals.First(c => c.GetType().Name == peripheralType);
            this.peripherals.Remove(peripheral);

            return string.Format(SuccessMessages.RemovedPeripheral, peripheralType, peripheral.Id);
        }

        public string AddComponent(int computerId, int id, string componentType, string manufacturer, string model, decimal price,
            double overallPerformance, int generation)
        {
            var computer = CheckExistence(computerId);
            var component = componentType switch
            {
                nameof(CentralProcessingUnit) => (IComponent)new CentralProcessingUnit(id, manufacturer, model, price, overallPerformance, generation),
                nameof(Motherboard) => new Motherboard(id, manufacturer, model, price, overallPerformance, generation),
                nameof(PowerSupply) => new PowerSupply(id, manufacturer, model, price, overallPerformance, generation),
                nameof(RandomAccessMemory) => new RandomAccessMemory(id, manufacturer, model, price, overallPerformance, generation),
                nameof(SolidStateDrive) => new SolidStateDrive(id, manufacturer, model, price, overallPerformance, generation),
                nameof(VideoCard) => new VideoCard(id, manufacturer, model, price, overallPerformance, generation),
                _ => throw new ArgumentException(ExceptionMessages.InvalidComponentType)
            };

            if (computer.Components.Any(c=>c.Id==id))
            {
                throw new ArgumentException(ExceptionMessages.ExistingComponentId);
            }

            this.components.Add(component);
            computer.AddComponent(component);

            return string.Format(SuccessMessages.AddedComponent, componentType, id, computerId);
        }

        public string RemoveComponent(string componentType, int computerId)
        {
            var computer = CheckExistence(computerId);
            computer.RemoveComponent(componentType);

            var component = components.First(c => c.GetType().Name == componentType);
            this.components.Remove(component);

            return string.Format(SuccessMessages.RemovedComponent, componentType, component.Id);
        }

        public string BuyComputer(int id)
        {
            var computer = CheckExistence(id);
            this.computers.Remove(computer);

            return computer.ToString();
        }

        public string BuyBest(decimal budget)
        {
            var computer = this.computers
                .Where(c => c.Price <= budget)
                .OrderByDescending(c => c.OverallPerformance)
                .FirstOrDefault();

            if (computer==null)
            {
                throw new ArgumentException(string.Format(ExceptionMessages.CanNotBuyComputer, budget));
            }

            this.computers.Remove(computer);

            return computer.ToString();
        }

        public string GetComputerData(int id)
        {
            var computer = CheckExistence(id);

            return computer.ToString();
        }

        private IComputer CheckExistence(int id)
        {
            var computer = this.computers.FirstOrDefault(c => c.Id == id);

            if (computer==null)
            {
                throw new ArgumentException(ExceptionMessages.NotExistingComputerId);
            }

            return computer;
        }
    }
}