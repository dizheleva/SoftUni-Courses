using System;
using System.Text;

namespace ClassBoxData
{
    public class Box
    {
        private double length;
        private double width;
        private double height;

        public Box(double length, double width, double height)
        {
            this.Length = length;
            this.Width = width;
            this.Height = height;
        }

        public double Length
        {
            get => length;
            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Length cannot be zero or negative.");
                }

                length = value;
            }
        }

        public double Width
        {
            get => width;
            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Width cannot be zero or negative.");
                }

                width = value;
            }
        }

        public double Height
        {
            get => height;
            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Height cannot be zero or negative.");
                }

                height = value;
            }
        }

        public double GetSurfaceArea()
        {
            var surfaceArea = this.GetLateralSurfaceArea() + 2 * length * width;
            return surfaceArea;
        }

        public double GetLateralSurfaceArea()
        {
            var lateralSurfaceArea = 2 * this.length * this.height + 2 * this.width * this.height;
            return lateralSurfaceArea;
        }

        public double GetVolume()
        {
            var volume = this.length * this.width * this.height;
            return volume;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Surface Area - {this.GetSurfaceArea():F2}");
            sb.AppendLine($"Lateral Surface Area - {this.GetLateralSurfaceArea():F2}");
            sb.Append($"Volume - {this.GetVolume():F2}");
            return sb.ToString();
        }
    }
}
