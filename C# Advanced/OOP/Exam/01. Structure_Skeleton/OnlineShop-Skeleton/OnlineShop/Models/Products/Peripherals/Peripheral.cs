using System;
using System.Text;
using OnlineShop.Common.Constants;

namespace OnlineShop.Models.Products.Peripherals
{
    public abstract class Peripheral : Product, IPeripheral
    {
        protected Peripheral(int id, string manufacturer, string model, decimal price, double overallPerformance, string connectionType) 
            : base(id, manufacturer, model, price, overallPerformance)
        {
            this.ConnectionType = connectionType;
        }

        public string ConnectionType { get; }
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine(base.ToString()
                          + String.Format(SuccessMessages.PeripheralToString, this.ConnectionType));
            return sb.ToString().TrimEnd();
        }
    }
}